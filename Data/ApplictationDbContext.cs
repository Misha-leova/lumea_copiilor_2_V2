using lumea_copiilor_2.Models;
using Microsoft.EntityFrameworkCore;

namespace lumea_copiilor_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define DbSets for your models
        public DbSet<AdminUser> AdminUsers { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        //public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<lumea_copiilor_2.Models.UserInformation> UserInformation { get; set; } = default!;
    }
}
