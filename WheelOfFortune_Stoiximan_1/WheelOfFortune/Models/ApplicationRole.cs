using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public bool IsAdmin { get; set; }
    }
}
