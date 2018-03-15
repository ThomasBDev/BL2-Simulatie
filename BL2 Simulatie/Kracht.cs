using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL2_Simulatie
{
    public class Kracht
    {
        public double direction, power;

        public Kracht(double direction, double power)
        {
            this.direction = direction;
            this.power = power;
        }
    }
}
