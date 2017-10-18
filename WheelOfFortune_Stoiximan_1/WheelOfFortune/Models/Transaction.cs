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
        public int CouponId { get; set; }//Caution this could be null/0 if we implement a non-coupon transaction or after a spin result?. Could be set to -1 by default.
        public DateTime TransactionDate { get; set; }


        //Use this as Action? Should action be Enum? Is it possible with DBs?
        public string Comment { get; set; }// describe if there is some sort of special reason.

        public float Amount { get; set; }//can be negative.

        //TO-DO Implement below in a new Reason Model.
        //public TobeAdded ReasonID { get; set; }


    }
}
