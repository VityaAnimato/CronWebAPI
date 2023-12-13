using Microsoft.EntityFrameworkCore;

namespace CronWebApi.Models
{
    public class RequestHistoryDbContext : DbContext
    {
        public RequestHistoryDbContext(DbContextOptions<RequestHistoryDbContext> options) : base(options)
        {
        }

        public DbSet<RequestHistoryItem> RequestHistoryItems => Set<RequestHistoryItem>();
    }
}
