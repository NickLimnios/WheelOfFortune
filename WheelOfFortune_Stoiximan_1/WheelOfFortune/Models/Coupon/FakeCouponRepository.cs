using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public class FakeCouponRepository : ICouponsRepository
    {
        public IEnumerable<Coupon> Coupons => new List<Coupon>
        {
            new Coupon{ Id = 1, Code = "00001", Value = 10.0F,CreationDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddYears(1),IsInactive = false},

            new Coupon{ Id = 2, Code = "00002", Value = 20.0F,CreationDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddYears(2),IsInactive = false},

            new Coupon{ Id = 3, Code = "00003", Value = 15.5F,CreationDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddHours(12),IsInactive = false}
        };
    }
}
