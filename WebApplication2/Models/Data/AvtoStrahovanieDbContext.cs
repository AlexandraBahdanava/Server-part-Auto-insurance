using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Data
{
    public class AvtoStrachovanieDbContext : DbContext
    {
        public AvtoStrachovanieDbContext(DbContextOptions<AvtoStrachovanieDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AuthoriationData> AuthoriationData { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<InsurancePackages> InsurancePackages { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Statements> Statements { get; set; }

    }

}
