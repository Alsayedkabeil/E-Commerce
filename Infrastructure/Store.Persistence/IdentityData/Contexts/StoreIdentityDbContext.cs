using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.IdentityData.Contexts
{
    // CLR
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        #region Apply Configuration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses"); // Map Address entity to Addresses table
        }
        #endregion
    }
}
