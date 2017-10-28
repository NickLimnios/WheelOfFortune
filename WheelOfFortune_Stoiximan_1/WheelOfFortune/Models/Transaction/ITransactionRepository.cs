using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> Transactions { get; }
    }
}
