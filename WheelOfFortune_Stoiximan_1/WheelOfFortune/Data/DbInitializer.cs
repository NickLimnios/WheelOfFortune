using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Models;
using WheelOfFortune.Helpers;

namespace WheelOfFortune.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //Ensure there are no pending changes
            context.Database.Migrate();

            //Seed Initial Data
            SeedRoles(context);
            SeedUsers(context);
            SeedUserRoles(context);
        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            //Check if initial roles exists
            if (context.Roles.Any())
                return;

            //create role initial data
            List<ApplicationRole> roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Name = "Admin",
                    IsAdmin = true
                },
                new ApplicationRole
                {
                    Name = "SuperAdmin",
                    IsAdmin = true
                },
                new ApplicationRole
                {
                    Name = "User",
                    IsAdmin = false
                }

            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            //check if initial users exist
            if (context.Users.Any())
                return;

            Helper helper = new Helper();

            //create initial user
            ApplicationUser[] users = new ApplicationUser[]
                {
                    new ApplicationUser
                    {
                        UserName = "admin@wheel.gr",
                        NormalizedUserName ="ADMIN",
                        PasswordHash = helper.CalculateHash("Admin!23"),
                        Email = "admin@wheel.gr",
                        EmailConfirmed = true
                    }
                };

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void SeedUserRoles(ApplicationDbContext context)
        {
            //chack if initial userroles exist
            if (context.UserRoles.Any())
                return;

            //crate initial userroles
            IdentityUserRole<int>[] userRoles = new IdentityUserRole<int>[]
                 {
                     new IdentityUserRole<int>
                     {
                        RoleId = context.Roles.Where(p => String.Equals(p.Name,"Admin")).Select(p => p.Id).FirstOrDefault(),
                        UserId = context.Users.Where(p => String.Equals(p.UserName,"admin@wheel.gr")).Select(p => p.Id).FirstOrDefault()
                     }
                 };

            context.AddRange(userRoles);
            context.SaveChanges();
        }

    }
}
