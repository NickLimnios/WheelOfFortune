using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WheelOfFortune.Models
{
    public class WheelOfFortuneContext : DbContext
    {
        public WheelOfFortuneContext (DbContextOptions<WheelOfFortuneContext> options)
            : base(options)
        {
        }

        public DbSet<WheelOfFortune.Models.AdminCoupon> AdminCoupon { get; set; }

        internal void Update(AdminCoupon adminCoupon, Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
