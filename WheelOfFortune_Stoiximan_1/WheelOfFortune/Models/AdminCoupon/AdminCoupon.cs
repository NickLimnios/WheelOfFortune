using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelOfFortune.Models
{
    public class AdminCoupon
    {
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public int ID { get; set; }
        private string Code1;
        public string Code
        {
            get { return Code1 ?? Guid.NewGuid().ToString("N").Substring(0, 6); }

            set { Code1 = value; }
        } //the code for the user to redeem it.

        public int Value { get; set; } //value the user will receive after redeeming

        private DateTime? creationDate;
        public DateTime CreationDate
        {
            get { return creationDate ?? DateTime.Now; }
            set { creationDate = value; }
        }
        private string status;

        public string Status
        {
            get { return status ?? "Active"; }
            set { status = value; }
        }
    }
}
