using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;
using WheelOfFortune.Models;
using Microsoft.AspNetCore.Identity;

namespace WheelOfFortune.Controllers
{
    public class SpinTheWheelController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SpinTheWheelController(ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult PlayWheelOfFortune()
        {
            return View();
        }

        //Should this be moved to a service or something similar?
        [HttpPost]
        [Authorize]//is this enough for security purposes?
        public IActionResult UserSpinTheWheel(float spinBetAmount)
        {
            //This will execute if the user CAN'T Spin...
            if (!UserAbleToSpin(spinBetAmount))
            {
                return Json(new
                {
                    spinStatus = "Not valid spin",
                    userPlacedAmount = spinBetAmount
                });
            }
            //else

            DoStuff();

            return Json(new
            {
                spinStatus = "Valid spin",
                userPlacedAmount = spinBetAmount
            });

        }

        private bool UserAbleToSpin(float AmountToSpinFor)
        {
            //TODO Fix this proper
            //Check for NON-LEGIT values as well as if amount <= USER.BALANCE
            return true;
        }
        private  void DoStuff()
        {
            //var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            //if (user == null)
            //{
            //    throw new ApplicationException($"Unable to load two-factor authentication user.");
            //}

            _applicationDbContext.GetDBSet<Transaction>().Add(new Transaction
            {
                UserId =1,
                Amount = /*new Random().Next(-10, 10)*/ 20,
                Comment = "Wheel Spin",
                TransactionDate = DateTime.Now
            });
             _applicationDbContext.SaveChangesAsync();
        }
    }
}
