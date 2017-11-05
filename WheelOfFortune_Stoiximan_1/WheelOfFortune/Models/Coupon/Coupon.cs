using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;
using WheelOfFortune.Interfaces;

namespace WheelOfFortune.Models
{
    public class Coupon : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public float Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsInactive { get; set; } = false;
      
    }
}
