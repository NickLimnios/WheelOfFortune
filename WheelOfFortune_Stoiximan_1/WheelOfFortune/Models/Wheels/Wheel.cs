using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheelOfFortune.Models.Wheels
{
    //When the Admin creates a wheel, this is what the DB will store.
    public class Wheel
    {
        public int Id { get; set; }

        public string WheelJson{ get; set; }
    }
}
