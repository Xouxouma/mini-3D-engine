using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class SphereImplicite
    {
        private double x;
        private double y;
        private double z;
        private double r;

        public SphereImplicite(double x, double y, double z, double r)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.r = r;
        }

        public override string ToString()
        {
            return "CercleImplicite : { (x - " + x + ")² + (y - " + y + ")² = " + r + "²";
        }

        // Conversions
        public SphereParam ToSphereParam()
        {
            VectCartesien centre = new VectCartesien(x, y, z);
            return new SphereParam(centre, r);
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
        public double Surface()
        {
            return 4 * Math.PI * r * r;
        }
        public double Volume()
        {
            return 4 * Math.PI * r * r * r / 3;
        }
    }
}
