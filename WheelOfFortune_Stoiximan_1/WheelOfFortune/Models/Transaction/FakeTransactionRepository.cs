using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models
{
    public class FakeTransactionRepository : ITransactionRepository
    {
        public IEnumerable<Transaction> Transactions => new List<Transaction> {
            new Transaction{Id = 1, CouponId=1, UserId=1,Amount=10,Comment="Fake Transaction 1",TransactionDate=DateTime.Now },
            new Transaction{Id = 2, CouponId=2, UserId=2,Amount=10,Comment="Fake Transaction 2",TransactionDate=DateTime.Now },
            new Transaction{Id = 3, CouponId=3, UserId=3,Amount=10,Comment="Fake Transaction 3",TransactionDate=DateTime.Now }
        };
    }
}
