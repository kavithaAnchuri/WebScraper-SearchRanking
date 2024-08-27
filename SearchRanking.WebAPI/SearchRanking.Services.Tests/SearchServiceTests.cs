using Moq;
using Moq.Protected;
using SearchRanking.Data.Models;
using SearchRanking.Data.Repositories;
using SearchRanking.ScraperService;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SearchRanking.Services.Tests
{
    public class SearchServiceTests
    {
        private readonly Mock<ISearchRepository> _searchHistoryRepositoryMock;
        private readonly SearchService _searchService;

        public SearchServiceTests()
        {
            _searchHistoryRepositoryMock = new Mock<ISearchRepository>();
            _searchService = new SearchService(_searchHistoryRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUrlSearchRankings_ValidInput_ReturnsRankings()
        {
            // Arrange
            var keyword = "test";
            var url = "https://test.com";
            var topsearchs = 10;
            var responseBody = "<a href=\"https://test.com\">register</a>";

            var httpClientHandlerMock = new Mock<HttpMessageHandler>();
            httpClientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseBody)
                });

            var httpClient = new HttpClient(httpClientHandlerMock.Object);
            
            // Act
            var result = await _searchService.GetUrlSearchRankings(keyword, url, topsearchs);

            // Assert
            Assert.Equal("0", result); // Assuming the URL is in the first position
            _searchHistoryRepositoryMock.Verify(r => r.AddSearchHistory(It.IsAny<SearchHistory>()), Times.Once);
        }

        [Fact]
        public async Task GetUrlSearchRankings_InvalidKeyword_ThrowsArgumentException()
        {
            // Arrange
            var keyword = "";
            var url = "test.com";
            var topsearchs = 100;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _searchService.GetUrlSearchRankings(keyword, url, topsearchs));
            Assert.Equal("Keyword cannot be empty. (Parameter 'keyword')", exception.Message);
        }

        [Fact]
        public async Task GetUrlSearchRankings_InvalidUrl_ThrowsArgumentException()
        {
            // Arrange
            var keyword = "test";
            var url = "";
            var topsearchs = 100;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _searchService.GetUrlSearchRankings(keyword, url, topsearchs));
            Assert.Equal("URL cannot be empty. (Parameter 'url')", exception.Message);
        }

        [Fact]
        public async Task GetHistory_ReturnsSearchHistories()
        {
            // Arrange
            var expectedHistories = new List<SearchHistory>
            {
                new SearchHistory { Keyword = "land registry searches", Url = "infotrack", Rankings = "12", SearchDate = DateTime.UtcNow },
                new SearchHistory { Keyword = "land registry searches", Url = "infotrack.co.uk", Rankings = "9", SearchDate = DateTime.UtcNow }
            };

            _searchHistoryRepositoryMock.Setup(r => r.GetSearchHistories())
                .ReturnsAsync(expectedHistories);

            // Act
            var result = await _searchService.GetHistory();

            // Assert
            Assert.Equal(expectedHistories, result);
        }


    }
}