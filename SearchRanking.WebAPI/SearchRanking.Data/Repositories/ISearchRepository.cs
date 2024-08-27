using SearchRanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRanking.Data.Repositories
{
    public interface ISearchRepository
    {
        Task<IEnumerable<SearchHistory>> GetSearchHistories();

        void AddSearchHistory(SearchHistory searchHistory);

    }
}
