using Microsoft.EntityFrameworkCore;
using SearchRanking.Data.Models;

namespace SearchRanking.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<SearchHistory> SearchHistory { get; set; }
    }
}
