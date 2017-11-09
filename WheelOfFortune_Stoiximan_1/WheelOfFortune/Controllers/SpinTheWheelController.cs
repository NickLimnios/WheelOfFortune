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
                    spinStatus = "Not a valid spin",
                    userPlacedAmount = spinBetAmount
                });
            }
            //else

            //Do transaction
            ApplySpinResultToTransaction(ServerSpinResult(spinBetAmount));

            //Reply to client the result.
            return Json(new
            {
                spinStatus = "Valid spin",
                userPlacedAmount = spinBetAmount
            });

        }

        private bool UserAbleToSpin(float AmountToSpinFor)
        {
            //TODO make 0f a StaRes(static resource) value, controllable by admin(or code or something)?
            return AmountToSpinFor > 0f && AmountToSpinFor  <= UserBalance();
        }

        private float UserBalance()
        {
            float userBalance = _applicationDbContext.GetDBSet<Transaction>().
                Where(u => u.UserId == Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).
                Sum(t => t.Amount);
            return userBalance;
        }


        private float ServerSpinResult(float bet)
        {
            return new Random().Next((int)MathF.Ceiling(-bet), (int)MathF.Ceiling(bet));
        }

        private void ApplySpinResultToTransaction(float spinResultToWrite)
        {
            _applicationDbContext.GetDBSet<Transaction>().Add(new Transaction
            {
                UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Amount = spinResultToWrite,
                Comment = "Wheel Spin",
                TransactionDate = DateTime.Now
            });
            //TODO SOMETIMES IT CRASHES HERE!!! WHY???? it crashes if you spin and change immidiatly tab.
            _applicationDbContext.SaveChangesAsync();
        }
    }
}
