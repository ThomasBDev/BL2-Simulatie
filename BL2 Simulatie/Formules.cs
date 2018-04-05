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

        public double FmaVerplaatsing(double F, double m, double t)
        {
            return 0.5 * (F / m) * Math.Pow(t, 2);
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

        public double Fv(double m, double v, double t)
        {
            return (m * v) / t;
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

        //De kracht in de X-richting van Fv.
        public double Fx(double Fs, double newDir)
        {
            return Fs * Math.Sin(newDir);
        }

        //De kracht in de Y-richting van Fv.
        public double Fy(double Fs, double newDir)
        {
            return Fs * Math.Cos(newDir);
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



        //Goniometrie-------------------------------------------------------------
        public double NieuweRichting(double planetDir, double mpzDir)
        {
            double hoek = Math.Atan(mpzDir / planetDir);
            return hoek;
        }

        public double AfstandTussenHemellichamen(Vector2 A, Vector2 B)
        {
            double AKwadraat = Math.Pow( (A.X - B.X), 2 );
            double BKwadraat = Math.Pow( (A.Y - B.Y), 2 );
            double resultaat = Math.Sqrt(AKwadraat + BKwadraat);
            return resultaat;
        }
    }
}
