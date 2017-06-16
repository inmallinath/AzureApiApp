using Microsoft.EntityFrameworkCore;

namespace AzureApiApp.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<CheckList> CheckListItems { get; set; }
    }
}