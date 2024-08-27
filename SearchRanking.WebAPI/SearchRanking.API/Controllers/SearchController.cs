using Microsoft.AspNetCore.Mvc;
using SearchRanking.Data.Models;
using SearchRanking.ScraperService;
using MediatR;
using SearchRanking.ScraperService.Queries.Search;


namespace SearchRanking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISearchService _searchService;

        public SearchController(IMediator mediator, ISearchService searchService)
        {
            _searchService = searchService;
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> GetSearchResults(SearchRequest request)
        {
            var query = new GetSearchRankingsQuery(request.Keyword, request.Url);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetSearchHistories()
        {
            var histories = await _searchService.GetHistory();
            return Ok(histories);
        }
    }
}
