using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models;
using WheelOfFortune.Interfaces;
using WheelOfFortune.Models.Wheels;

namespace WheelOfFortune.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}
        
        public DbSet<T> GetDBSet<T> () where T : class, IEntity
        {
            return this.Set<T>();
        }


        public DbSet<AdminCoupon> AdminCoupon { get; set; }
        public DbSet<Wheel> AdminWheels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            ModelCustomize(builder);
        }

        private void ModelCustomize(ModelBuilder builder)
        {
            //Users
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationUser>(p => p.Property(c => c.Email).HasColumnName("EmailAddress"));

            //Roles
            builder.Entity<ApplicationRole>().ToTable("Roles");

            //Transaction
            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<Transaction>()
                .HasOne(p => p.User)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FKTransactionsUsers");

            //Coupon
            builder.Entity<Coupon>().ToTable("Coupons");

        }
    }
}
