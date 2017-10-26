using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Models;

namespace WheelOfFortune.Controllers
{
    public class CouponController : Controller
    {
        private ICouponsRepository repository;

        public CouponController(ICouponsRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Coupons);
    }
}
