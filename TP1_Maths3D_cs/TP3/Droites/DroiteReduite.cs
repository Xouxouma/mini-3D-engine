using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteReduite
    {
        private double y0;
        private double m;

        public DroiteReduite(double y0, double m)
        {
            this.y0 = y0;
            this.m = m;
        }
        public override string ToString()
        {
            return "droite : y = " + m + "*x + " + y0;
        }

        // Conversions
        public DroiteImplicite ToDroiteImplicite()
        {
            double a = -m;
            double b = 1.0;
            double c = y0;
            return new DroiteImplicite(a, b, c);
        }
    }
}
