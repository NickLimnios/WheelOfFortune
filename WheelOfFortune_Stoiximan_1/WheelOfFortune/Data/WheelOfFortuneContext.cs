using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models.Wheels;

namespace WheelOfFortune.Models
{
    public class WheelOfFortuneContext : DbContext
    {
        public WheelOfFortuneContext(DbContextOptions<WheelOfFortuneContext> options)
            : base(options)
        {
        }

        public DbSet<AdminCoupon> AdminCoupon { get; set; }
        public DbSet<Wheel> AdminWheels { get; set; }
    }
}
