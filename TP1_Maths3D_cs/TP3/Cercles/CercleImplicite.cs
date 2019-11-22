using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class CercleImplicite
    {
        private double x;
        private double y;
        private double r;

        public CercleImplicite(double x, double y, double r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }

        public override string ToString()
        {
            return "CercleImplicite : { (x - " + x + ")² + (y - " + y + ")² = " + r + "²";
        }

        // Conversions
        public CercleParam ToCercleParam()
        {
            VectCartesien centre = new VectCartesien(x, y);
            return new CercleParam(centre, r);
        }

        // Quantités associées
        public double Diametre()
        {
            return 2 * r;
        }
        public double Circonference()
        {
            return 2 * r * Math.PI;
        }
        public double Aire()
        {
            return Math.PI * r * r;
        }
    }
}
