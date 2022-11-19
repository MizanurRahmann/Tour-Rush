using AssignmentAsp.Net.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAsp.Net.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<TourViewModel> Tours { get; set; }
        public DbSet<AccountViewModel> Accounts { get; set; }
    }
}
