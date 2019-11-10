using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class VectPolaire
    {
        private double r;
        private double theta;
        private double phi;

        // Constructeurs
        public VectPolaire(double r, double theta, double phi)
        {
            this.r = r;
            this.theta = theta;
            this.phi = phi;
        }

        public VectPolaire()
        {
            this.r = 0;
            this.theta = 0;
            this.phi = 0;
        }

        // toString
        public override string ToString() {
            return "(" + this.r + "," + this.theta + ", " + this.phi + ")";
        }

        // toCartesien
        public VectCartesien toCartesien()
        {
            double x = r * Math.Cos(theta) * Math.Sin(phi);
            double y = -r * Math.Sin(theta);
            double z = r * Math.Cos(theta) * Math.Cos(phi);
            return new VectCartesien(x, y, z);
        }
    }
}
