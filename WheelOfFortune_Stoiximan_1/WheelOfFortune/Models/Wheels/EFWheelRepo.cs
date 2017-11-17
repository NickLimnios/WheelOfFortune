using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFortune.Data;

namespace WheelOfFortune.Models.Wheels
{
    public class EFWheelRepo
    {
        private readonly ApplicationDbContext context;

        public EFWheelRepo(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Wheel> Transactions => context.GetDBSet<Wheel>();
    }
}
