using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
        public float Amount { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? CouponId { get; set; }
    }
}
