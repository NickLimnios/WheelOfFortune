using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        //public DbSet<Transaction> Transactions { get; set; }
       // public DbSet<Coupon> Coupons { get; set; }


        public DbSet<T> GetDBSet<T> () where T : class, IEntity
        {
            return this.Set<T>();
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
            builder.Entity<ApplicationUser>(p => p.Property(c => c.Email).HasColumnName("EmailAddress"));

            //Customize Application Role
            builder.Entity<ApplicationRole>().ToTable("Roles");

            builder.Entity<Transaction>().ToTable("Transaction");
            builder.Entity<Coupon>().ToTable("Coupon");

            //to do 
            // add Foreign Key like this (Entity<user>hasmany(table).withone(user)
            // add Primary Key



        }
    }
}
