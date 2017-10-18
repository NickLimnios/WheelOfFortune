using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; } //the code for the user to redeem it.
        public float Value { get; set; } //value the user will receive after redeeming

        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; } //in case we want the coupon to expire

        public bool IsInactive { get; set; } = false; // if the coupon is used. Do we need this?

    }
}
