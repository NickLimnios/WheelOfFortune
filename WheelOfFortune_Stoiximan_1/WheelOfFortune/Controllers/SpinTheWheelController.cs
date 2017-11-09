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

        [HttpGet]
        [Authorize]//We need to know that hte user is logged. Is this enough for security purposes?
        public IActionResult GetSpinWheelHash()
        {
            return Json(new
            {
                _wheelHash = GetCurrentSpinHash()
            });
        }

        [HttpPost]
        [Authorize]//We need to know that hte user is logged. Is this enough for security purposes?
        public IActionResult UserSpinTheWheel(string _spinWheelHash, float _spinBetAmount)
        {
            //To avoid smartasses.May need more checks. Should we allow this or block the user if he places < 0?
            float spinBetAmount = Math.Abs(_spinBetAmount);

            //cause we got some null exceptions...
            if (_spinWheelHash is null) _spinWheelHash = "";

            //This will execute if the user CAN'T Spin...
            //TODO automatically update/refresh the page?
            if (!_spinWheelHash.Equals(GetCurrentSpinHash()))
            {
                return Json(new
                {
                    spinStatus = false,
                    spinStatusMsg = "Not a valid spin. Spin wheel out of date. Refresh the page.",
                    userPlacedAmount = spinBetAmount
                });
            }

            //This will execute if the user CAN'T Spin...
            if (!UserSufficentBalance(spinBetAmount))
            {
                return Json(new
                {
                    spinStatus = false,
                    spinStatusMsg = "Not a valid spin. Bet placed amount, is greater than your balance.",
                    userPlacedAmount = spinBetAmount
                });
            }
            //else

            //Server decides if the spin is a winning or a loosing spin.(In a way we roll the dice ).
            int spinRestingSlice = GetNextRestingWheelSlice();

            //Server registers the transaction for the spin.
            float userReceived = ServerSpinAmountResult(spinRestingSlice, spinBetAmount);
            ApplySpinResultToTransaction(userReceived);

            // Server sends the decision back to client's STW.
            return Json(new
            {
                spinStatus = true,
                spinStatusMsg = "Valid spin",
                userPlacedAmount = spinBetAmount,
                userReceivedAmount = userReceived,
                wheelStoppingSlice = spinRestingSlice
            });

        }

        #region User eligible to spin
        private string GetCurrentSpinHash()
        {
            //TODO IMPLEMENT properly :p
            return "CurrentSpinWheelHash-ish";
        }

        private bool UserSufficentBalance(float AmountToSpinFor)
        {
            //TODO make 0f a StaRes(static resource) value, controllable by admin(or code or something)?
            return AmountToSpinFor > 0f && AmountToSpinFor <= GetUserBalance();
        }

        private float GetUserBalance()
        {
            return _applicationDbContext.GetDBSet<Transaction>().
                Where(u => u.UserId == Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).
                Sum(t => t.Amount);
        }
        #endregion

        #region Resolve spin action
        private int GetNextRestingWheelSlice()
        {
            //TODO IMPLEMENT PROPERLY calculate probabilities and what not.
            return new Random().Next(0, 4);
        }

        private float ServerSpinAmountResult(int restingSlice, float bet)
        {
            //Maybe we'll need to do other stuff on the amount before returning it such as calculating bonuses.
            return ResolveRestingSliceValue(restingSlice) * bet /* * ServerGetHolidayBonusWhatever()*/ ;
        }

        //This should return the percentage to multiply with the bet. It could be negative multiplier.
        private float ResolveRestingSliceValue(int _restingSlice)
        {
            //TODO IMPLEMENT PROPER. Read from Json stored in DB with current spin values get value and respond properly.
            if (_restingSlice <= 1)
                return 2f;
            else
                return -1f;
        }

        //TODO SOMETIMES THE SPIN WON'T REGISTER!!! WHY??
        private void ApplySpinResultToTransaction(float spinResultToWrite)
        {
            _applicationDbContext.GetDBSet<Transaction>().Add(new Transaction
            {
                UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Amount = spinResultToWrite,
                Comment = "Wheel Spin",
                TransactionDate = DateTime.Now
            });
            //TODO SOMETIMES IT CRASHES HERE!!! WHY???? it crashes if you spin and change immediately tab.
            _applicationDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
