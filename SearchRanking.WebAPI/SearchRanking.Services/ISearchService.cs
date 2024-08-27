using SearchRanking.Data.Models;

namespace SearchRanking.ScraperService
{
    public interface ISearchService
    {
        Task<string> GetUrlSearchRankings(string keyword, string url, int topsearchs = 100);

        Task<IEnumerable<SearchHistory>> GetHistory();
    }
}
