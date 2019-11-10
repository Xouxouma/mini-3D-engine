using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteImplicite
    {
        private double a;
        private double b;
        private double c;

        public DroiteImplicite(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public override string ToString()
        {
            return "Droite : " + a + "x + " + b + "y + " + c + " = 0";
        }
    }
}
