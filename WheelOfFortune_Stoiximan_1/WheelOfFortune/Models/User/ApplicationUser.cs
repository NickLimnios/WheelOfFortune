using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WheelOfFortune.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public string ImageUrl { get; set; }
        public byte[] Image { get; set; }
        
        public List<Transaction> Transactions { get; set; }
    }
}
