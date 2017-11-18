using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Models;
using WheelOfFortune.Models.Wheels;

namespace WheelOfFortune.Controllers
{
    public class AdminWheelsController : Controller
    {
        private readonly WheelOfFortuneContext _context;

        public AdminWheelsController(WheelOfFortuneContext context)
        {
            _context = context;
        }

        // GET: Admin wheels
        public IActionResult Index()
        {
            //database pull wheels and convert AllWheelSlices to WheelSliceContainer
            return View(_context.AdminWheels.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateWheel(List<WheelSliceContainer> adminCreatedSlices)
        {
            foreach (var slice in adminCreatedSlices)
            {
                Debug.WriteLine(slice.resultText);
            }

            //int Value = adminWheel.Value;

            //_context.Add(adminWheel);
            //DateTime CreationDate = DateTime.Now;
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult AddSlice()
        {
            return PartialView("WheelSlice", new WheelSliceContainer());
        }
    }
}
