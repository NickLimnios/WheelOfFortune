using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ViewResult TransactionList() => View(transactionRepository.Transactions);
    }
}
