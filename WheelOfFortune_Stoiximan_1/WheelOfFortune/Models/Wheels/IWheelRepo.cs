using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models.Wheels
{
    public class IWheelRepo
    {
        IEnumerable<Wheel> Wheels { get; }
    }
}
