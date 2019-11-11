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

        // Conversions
        public DroiteReduite ToDroiteReduite()
        {
            double m = -a / b;
            double y0 = c / b;
            return new DroiteReduite(y0, m);
        }
        public DroiteNormaleDistance ToDroiteNormaleDistance()
        {
            VectCartesien n = new VectCartesien(a, b);
            double distance = c / n.magnitude();
            return new DroiteNormaleDistance(n, distance);
        }
    }
}
