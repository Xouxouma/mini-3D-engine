using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class RayonParam
    {
        private Point2D p0;
        private VectCartesien d;

        public RayonParam(Point2D p0, VectCartesien d)
        {
            if (d.getDim() != 2)
                throw new System.ArgumentException("VectCartesien d must be of size 2.");
            this.p0 = p0;
            this.d = d;
        }
        public RayonParam(double p0_x, double p0_y, double dir, double dist)
        {
            this.p0 = new Point2D(p0_x, p0_y);
            this.d = new VectCartesien(dir, dist);
        }
        public override String ToString()
        {
            return "Rayon : p(t) = " + p0 + " + t*" + d;
        }

        // Conversions
        public RayonDirect ToRayonDirect()
        {
            Point2D p_end = new Point2D(p0.GetX() + d[0], p0.GetY() + d[1]);
            return new RayonDirect(p0, p_end);
        }
    }
}
