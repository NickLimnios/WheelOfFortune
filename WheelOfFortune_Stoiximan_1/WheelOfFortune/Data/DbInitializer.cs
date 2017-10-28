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
                    IsAdmin = true,
                    NormalizedName = "Admin"
                },
                new ApplicationRole
                {
                    Name = "SuperAdmin",
                    IsAdmin = true,
                    NormalizedName = "SuperAdmin"
                },
                new ApplicationRole
                {
                    Name = "User",
                    IsAdmin = false,
                    NormalizedName = "User"
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
            var user = new ApplicationUser
            {

                Email = "my@email.com",
                UserName = "TestUser",
                NormalizedUserName = "TESTUSER",
                PhoneNumber = "00000000",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == "me@myemail.com"))
            {
                var _password = new PasswordHasher<ApplicationUser>();
                var hashed = _password.HashPassword(user, "Test!23");
                user.PasswordHash = hashed;

                context.Users.Add(user);
                context.SaveChanges();

            }

            context.Users.AddRange(user);
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
                        UserId = context.Users.Where(p => String.Equals(p.UserName,"TestUser")).Select(p => p.Id).FirstOrDefault()
                     }
                 };

            context.AddRange(userRoles);
            context.SaveChanges();
        }

    }
}
