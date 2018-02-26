using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BL2_Simulatie
{
    class Formules
    {
        public double Snelheid(double a, double t)
        {
            return a * t;
        }

        public double Verplaatsing(double a, double t)
        {
            return 0.5 * a * Math.Pow(t, 2);
        }



        //Krachten-----------------------------------------------------------------------------
        public double F(double m, double a)
        {
            return m * a;
        }

        public double Fz(double m, double g)
        {
            return m * g;
        }

        public double Fgrav(double m1, double m2, double r)
        {
            double G = 0.00000000006674;

            return (G * m1 * m2) / Math.Pow(r, 2);
        }

        public double Fmpz(double m, double v, double r)
        {
            return (m * Math.Pow(v, 2)) / r;
        }



        //Ellips
        //public double LangeAs(Vector2 middelpunt, Vector2 positie)
        //{
        //    return 
        //}

        //public double KorteAs(Vector2 middelpunt, Vector2 positie)
        //{

        //}

        public double AfstandMiddelpuntEnFocus(double a, double b)
        {
            double c = Math.Pow(a, 2) - Math.Pow(b, 2);

            return Math.Sqrt(c);
        }

        public double KorteAsVanEccentriciteit(double a, double e)
        {
            double kwadraatA = Math.Pow(a, 2);
            double kwadraatE = Math.Pow(e, 2);

            return Math.Sqrt(kwadraatA - (kwadraatA * kwadraatE));
        }
    }
}
