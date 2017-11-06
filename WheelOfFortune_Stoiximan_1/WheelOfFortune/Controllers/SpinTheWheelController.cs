using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Controllers
{
    public class SpinTheWheelController : Controller
    {
        [Authorize(Roles ="User")]
        public IActionResult PlayWheelOfFortune()
        {
            return View();
        }
    }
}
