using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class VectPolaire
    {
        protected double r;
        protected double theta;

        // Constructeurs
        public VectPolaire(double r, double theta)
        {
            this.r = r;
            this.theta = theta;
        }

        public VectPolaire()
        {
            this.r = 0;
            this.theta = 0;
        }

        // toString
        public override string ToString()
        {
            return "(" + this.r + "," + this.theta + ")";
        }

        // toCartesien
        public VectCartesien toCartesien()
        {
            double x = r * Math.Cos(theta);
            double y = r * Math.Sin(theta);

            return new VectCartesien(x, y);
        }

        public void toCanonique()
        {
            if (r == 0)
                theta = 0;

            if (r < 0)
            {
                r = -r;
                theta += Utils.ConvertDegreesToRadians(180);
            }

            if (theta <= -Utils.ConvertDegreesToRadians(180))
            {
                while (theta <= -Utils.ConvertDegreesToRadians(180))
                    theta += Utils.ConvertDegreesToRadians(360);
            }

            if (theta > Utils.ConvertDegreesToRadians(180))
            {
                while (theta > Utils.ConvertDegreesToRadians(180))
                    theta -= Utils.ConvertDegreesToRadians(360);
            }
        }
    }
}
