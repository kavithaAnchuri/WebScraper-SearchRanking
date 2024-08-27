using SearchRanking.Data.Models;
using SearchRanking.Data.Repositories;
using System.Text.RegularExpressions;

namespace SearchRanking.ScraperService
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchHistoryRepository;

        private const string SearchEngineBaseUrl = "https://www.google.co.uk/";
        private readonly HttpClient _httpClient;

        public SearchService(ISearchRepository searchHistoryRepository)
        {
            _httpClient = new HttpClient();
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<string> GetUrlSearchRankings(string keyword, string url, int topsearchs)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword cannot be empty.", nameof(keyword));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty.", nameof(url));

            //keyword - including spaces and special characters, will be properly formatted with Uri.EscapeDataString
            var searchUrl = $"{SearchEngineBaseUrl}search?num={topsearchs}&q={Uri.EscapeDataString(keyword)}";

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(searchUrl);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Unable to get the results from the search engine. Please check the network and try again.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Search request failed with status code: {response.StatusCode}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var rankings = FindUrlRankings(url, responseBody);

            // Save the search history
            await SaveSearchResult(keyword, url, rankings);

            return rankings;
        }

        private string FindUrlRankings(string url, string response)
        {
            var pattern = @"<a\s+(?:[^>]*?\s+)?href=""(https?://[^\s]+)"""; // Pattern to match URLs in the response
            var matches = Regex.Matches(response, pattern);
            var rankings = new List<int>();

            for (int i = 0; i < matches.Count; i++)
            {
                var matchUrl = matches[i].Groups[1].Value;
                if (matchUrl.Contains(url, StringComparison.OrdinalIgnoreCase))
                {
                    rankings.Add(i + 1); 
                }
            }

            return rankings.Any() ? string.Join(", ", rankings) : "0";
        }

        private async Task SaveSearchResult(string keyword, string url, string rankings)
        {
            var searchHistory = new SearchHistory
            {
                Keyword = keyword,
                Url = url,
                Rankings = rankings,
                SearchDate = DateTime.UtcNow,
            };

            _searchHistoryRepository.AddSearchHistory(searchHistory);
        }

        public async Task<IEnumerable<SearchHistory>> GetHistory()
        {
            return await _searchHistoryRepository.GetSearchHistories();
        }
        
    }
}
