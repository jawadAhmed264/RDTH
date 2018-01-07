using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RDTH.Models;

namespace RDTH.Data
{
    public class RDTHDbContext : IdentityDbContext<ApplicationUser>
    {
        public RDTHDbContext(DbContextOptions<RDTHDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
