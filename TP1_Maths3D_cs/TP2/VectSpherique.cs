using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class VectSpherique
    {
        private double r;
        private double p;
        private double h;

        public VectSpherique(double r, double p, double h)
        {
            this.r = r;
            this.p = p;
            this.h = h;
        }

        public override string ToString()
        {
            return "(" + this.r + "," + this.p + "," + h + ")";
        }

        public VectCartesien toCartesien()
        {
            double x = r * Math.Cos(p) * Math.Sin(h);
            double y = -r * Math.Sin(p);
            double z = r * Math.Cos(p) * Math.Cos(h);
            return new VectCartesien(x, y, z);
        }

        public void toCanonique()
        {
            if (r == 0)
            {
                p = 0;
                h = 0;
            }

            if (r < 0)
            {
                r = -r;
                p = -p;
                h += Utils.ConvertDegreesToRadians(180);
            }

            if (p < Utils.ConvertDegreesToRadians(-90))
            {
                while (p < Utils.ConvertDegreesToRadians(-90))
                    p += Utils.ConvertDegreesToRadians(360);
            }

            if (p > Utils.ConvertDegreesToRadians(270))
            {
                while (p > Utils.ConvertDegreesToRadians(270))
                    p -= Utils.ConvertDegreesToRadians(360);
            }

            if (p > Utils.ConvertDegreesToRadians(90))
            {
                h += Utils.ConvertDegreesToRadians(180);
                p = Utils.ConvertDegreesToRadians(180) - p;
            }

            if (h <= Utils.ConvertDegreesToRadians(-180))
            {
                while (h <= Utils.ConvertDegreesToRadians(-180))
                    h += Utils.ConvertDegreesToRadians(360);
            }

            if (h > Utils.ConvertDegreesToRadians(180))
            {
                while (h > Utils.ConvertDegreesToRadians(180))
                    h -= Utils.ConvertDegreesToRadians(360);
            }
        }
    }
}
