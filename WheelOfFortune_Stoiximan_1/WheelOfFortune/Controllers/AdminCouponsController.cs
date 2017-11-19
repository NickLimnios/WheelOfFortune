using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WheelOfFortune.Models;
using System.Text;
using System.ComponentModel;
using WheelOfFortune.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WheelOfFortune.Controllers
{
    public class AdminCouponsController : Controller
    {
        private readonly WheelOfFortuneContext _context;
        private ApplicationDbContext _applicationDbContext;


        public AdminCouponsController(WheelOfFortuneContext context, ApplicationDbContext applicationDbContext)
        {
            _context = context;
            _applicationDbContext = applicationDbContext;
        }

        // GET: AdminCoupons
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminCoupon.ToListAsync());
        }

        // GET: AdminCoupons/GetCoupon
        public async Task<IActionResult> GetCoupon()
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

        // GET: AdminCoupons/Redeem/5
        public async Task<IActionResult> Redeem(int? id)
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
        // POST: AdminCoupons/Redeem/5
        [HttpPost, ActionName("Redeem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedeemConfirmed(int id)
        {
            var adminCoupon = await _context.AdminCoupon.SingleOrDefaultAsync(m => m.ID == id);
            adminCoupon.Status = "Distributed";
            await _context.SaveChangesAsync();

            //Add Initial Balance after Registration
            _applicationDbContext.GetDBSet<Transaction>().Add(new Transaction { UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), Amount = Convert.ToSingle(adminCoupon.Value), Comment = "Coupon: "+adminCoupon.Code,  TransactionDate = DateTime.Now });
            await _applicationDbContext.SaveChangesAsync();

            //ViewBag.Message = "Congratulations!!Your Code is: " + adminCoupon.Code;
            //return View();
            return RedirectToAction("TransactionList", "Transaction");

        }
    }
}