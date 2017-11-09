using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WheelOfFortune.Models;

namespace WheelOfFortune.Controllers
{
    public class TransactionController : Controller
    {
        private ITransactionRepository transactionRepository;

        public TransactionController(ITransactionRepository repo)
        {
            transactionRepository = repo;
        }

        [Authorize]
        public ViewResult TransactionList() => View(transactionRepository.Transactions
            .Where(p => p.UserId == Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));

        //Should this be moved to a service or something similar?
        [HttpGet]
        [Authorize]//is this enough for security purposes?
        public IActionResult GetUserBalance()
        {
            
            float userBalance = transactionRepository.Transactions.
                Where(u=>u.UserId == Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).
                Sum(t => t.Amount);

            return Json(new { balance = userBalance });
        }



    }
}
