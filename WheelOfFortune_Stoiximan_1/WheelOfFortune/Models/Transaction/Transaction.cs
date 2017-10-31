using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? CouponId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
        public float Amount { get; set; }
    }
}
