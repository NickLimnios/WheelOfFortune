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

            ApplySpinResultToTransaction();

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
        private  void ApplySpinResultToTransaction()
        {
            _applicationDbContext.GetDBSet<Transaction>().Add(new Transaction
            {
                UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), Amount = new Random().Next(-10, 10), Comment = "Wheel Spin",TransactionDate = DateTime.Now
            });
            //TODO SOMETIMES IT CRASHES HERE!!! WHY????
             _applicationDbContext.SaveChangesAsync();
        }
    }
}
