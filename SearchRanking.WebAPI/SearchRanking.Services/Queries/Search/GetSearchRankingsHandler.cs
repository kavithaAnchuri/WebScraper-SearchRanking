using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SearchRanking.ScraperService.Queries.Search
{
    public class GetSearchRankingsHandler : IRequestHandler<GetSearchRankingsQuery, string>
    {
        private readonly ISearchService _searchService;

        public GetSearchRankingsHandler(ISearchService searchService)
        {
            _searchService = searchService;
        }
        public async Task<string> Handle(GetSearchRankingsQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.GetUrlSearchRankings(request.Keyword, request.Url);
        }
    }
}
