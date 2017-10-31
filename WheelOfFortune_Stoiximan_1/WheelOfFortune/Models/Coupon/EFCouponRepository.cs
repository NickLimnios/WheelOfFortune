using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;

namespace WheelOfFortune.Models
{
    public class EFCouponRepository : ICouponsRepository
    {
        private ApplicationDbContext context;

        public EFCouponRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Coupon> Coupons => context.Coupons;
    }
}
