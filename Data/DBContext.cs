using Microsoft.EntityFrameworkCore;
using TravelAgency_Prod.Models;

namespace TravelAgency_Prod.Data
{
    public class TravelAgencyContext : DbContext
    {
        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Administrator> administrators { get; set; }
        public DbSet<TourManager> tourManagers { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Basket> baskets { get; set; }
        public DbSet<Tour> tours { get; set; }
        public DbSet<Favourite> favourites { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
