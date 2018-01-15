using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RDTH.Data.Models;
using RDTH.Models;

namespace RDTH.Data
{
    public class RDTHDbContext : IdentityDbContext<ApplicationUser>
    {
        public RDTHDbContext(DbContextOptions<RDTHDbContext> options)
            : base(options)
        {
        }
         
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Distributer> Distributers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<SetBox> SetBoxes { get; set; }
        public DbSet<CustomerCard> CustomerCards { get; set; }
        public DbSet<NewSubscribe> NewSubscribes { get; set; }
        public DbSet<MovieOnDemand> MoviesOnDemand { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Recharge> RechargeHistory { get; set; }
        public DbSet<CustomerPackage> CustomerPackages { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<NewSetBoxRequest> NewSetBoxRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
