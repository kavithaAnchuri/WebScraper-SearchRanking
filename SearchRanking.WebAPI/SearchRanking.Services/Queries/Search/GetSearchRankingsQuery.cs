using MediatR;

namespace SearchRanking.ScraperService.Queries.Search
{
    public class GetSearchRankingsQuery : IRequest<string>
    {
        public string Keyword { get; }
        public string Url { get; }

        public GetSearchRankingsQuery(string keyword, string url)
        {
            Keyword = keyword;
            Url = url;
        }
    }
}
