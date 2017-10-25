using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models;

namespace WheelOfFortune.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            ModelCustomize(builder);



        }

        private void ModelCustomize(ModelBuilder builder)
        {
            //Customize Application User
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationUser>().Property(typeof(string), "ImageUrl");
            builder.Entity<ApplicationUser>(p => p.Property(c => c.Email).HasColumnName("EmailAddress"));

            //Customize Application Role
            builder.Entity<ApplicationRole>().ToTable("Roles");
        }
    }
}
