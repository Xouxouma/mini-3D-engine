using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class RayonParam
    {
        private VectCartesien p0; // point 2d
        private VectCartesien d; // vect 2d

        public RayonParam(VectCartesien p0, VectCartesien d)
        {
            if (p0.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p0 must be of size 2.");
            if (d.getDim() != 2)
                throw new System.ArgumentException("VectCartesien d must be of size 2.");
            this.p0 = p0;
            this.d = d;
        }
        public RayonParam(double p0_x, double p0_y, double dir, double dist)
        {
            this.p0 = new VectCartesien(p0_x, p0_y);
            this.d = new VectCartesien(dir, dist);
        }
        public override String ToString()
        {
            return "Rayon : p(t) = " + p0 + " + t*" + d;
        }

        // Conversions
        public RayonDirect ToRayonDirect()
        {
            VectCartesien p_end = p0 + d;
            return new RayonDirect(p0, p_end);
        }

        public DroiteImplicite ToDroiteImplicite()
        {
            double a = d[1];
            double b = -d[0];
            double c = p0[0] * d[1] - p0[1] * d[0];
            return new DroiteImplicite(a, b, c);
        }
    }
}
