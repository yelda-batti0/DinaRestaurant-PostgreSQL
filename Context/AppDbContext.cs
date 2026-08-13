using DatabaseMastery.DinnerMenuPostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMastery.DinnerMenuPostgreSQL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
       
    }
}
