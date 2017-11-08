using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;

namespace WheelOfFortune.Models
{
    public class EFTransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext context;

        public EFTransactionRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Transaction> Transactions => context.GetDBSet<Transaction>();
    }
}
