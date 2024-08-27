using SearchRanking.Data.Models;
using System;
using System.Threading.Tasks;

namespace SearchRanking.Data.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchHistory>> GetSearchHistories()
        {
            var formattedHistory = _context.SearchHistory
            .Select(record => new SearchHistory
            {
                Id = record.Id,  // Keep other properties
                Keyword = record.Keyword,
                Url = record.Url,
                Rankings = record.Rankings,
                SearchDate = record.SearchDate.AddTicks(-(record.SearchDate.Ticks % TimeSpan.TicksPerSecond))
            })
            .ToList();

            return formattedHistory;
        }

        public void AddSearchHistory(SearchHistory searchResult)
        {
            _context.SearchHistory.Add(searchResult);
            _context.SaveChanges();
        }
    }
}
