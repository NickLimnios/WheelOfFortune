using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Controllers
{
    public class SpinTheWheelController : Controller
    {
        public IActionResult PlayWheelOfFortune()
        {
            return View();
        }
    }
}
