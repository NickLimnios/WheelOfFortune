using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;
using WheelOfFortune.Models;

namespace WheelOfFortune.Controllers
{
    public class UsersController : Controller
    {
        //private ICouponsRepository repository;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = _context.Users;
            var nonAdminUser = from users in _context.Users
                               join userRoles in _context.UserRoles on users.Id equals userRoles.UserId
                               join roles in _context.Roles on userRoles.RoleId equals roles.Id
                               where roles.NormalizedName != "Admin"
                               select users;

            return View(await nonAdminUser.ToListAsync());
        }

        public async Task<IActionResult> Enable(int? id)
        {
            var selectedUser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            selectedUser.UserBanned = false;
            _context.Update(selectedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Disable(int? id)
        {
            var selectedUser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            selectedUser.UserBanned = true;
            _context.Update(selectedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
