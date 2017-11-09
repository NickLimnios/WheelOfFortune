using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models;

namespace WheelOfFortune.Controllers
{
    public class AdminCouponsController : Controller
    {
        private readonly WheelOfFortuneContext _context;

        public AdminCouponsController(WheelOfFortuneContext context)
        {
            _context = context;
        }

        // GET: AdminCoupons
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminCoupon.ToListAsync());
        }

        // GET: AdminCoupons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCoupon = await _context.AdminCoupon
                .SingleOrDefaultAsync(m => m.ID == id);
            if (adminCoupon == null)
            {
                return NotFound();
            }

            return View(adminCoupon);
        }

        // GET: AdminCoupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCoupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AdminCoupon adminCoupon)
        {      
            int? Value = adminCoupon.Value;

            _context.Add(adminCoupon);
            DateTime CreationDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCoupons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCoupon = await _context.AdminCoupon.SingleOrDefaultAsync(m => m.ID == id);
            if (adminCoupon == null)
            {
                return NotFound();
            }
            return View(adminCoupon);
        }

        // POST: AdminCoupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] AdminCoupon adminCoupon)
        {
            if (id != adminCoupon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    adminCoupon.Status = "Revoked";
                    _context.Update(adminCoupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminCouponExists(adminCoupon.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminCoupon);
        }

        // GET: AdminCoupons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCoupon = await _context.AdminCoupon
                .SingleOrDefaultAsync(m => m.ID == id);
            if (adminCoupon == null)
            {
                return NotFound();
            }

            return View(adminCoupon);
        }

        // POST: AdminCoupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminCoupon = await _context.AdminCoupon.SingleOrDefaultAsync(m => m.ID == id);
            _context.AdminCoupon.Remove(adminCoupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminCouponExists(int id)
        {
            return _context.AdminCoupon.Any(e => e.ID == id);
        }
    }


}