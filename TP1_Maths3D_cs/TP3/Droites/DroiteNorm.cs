using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteNorm
    {
        private VectCartesien p;
        private VectCartesien n;

        public DroiteNorm(VectCartesien p, VectCartesien n)
        {
            if (p.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p must be of size 2.");
            if (n.getDim() != 2)
                throw new System.ArgumentException("VectCartesien n must be of size 2.");
            this.p = p;
            this.n = n;
        }

        public DroiteNorm(VectCartesien p, double n_x, double n_y)
        {
            this.p = p;
            this.n = new VectCartesien(n_x, n_y);
        }
        public DroiteNorm(double p_x, double p_y, double n_x, double n_y)
        {
            this.p = new Point2D(p_x, p_y);
            this.n = new VectCartesien(n_x, n_y);
        }
        public DroiteNorm(double p_x, double p_y, VectCartesien n)
        {
            this.p = new VectCartesien(p_x, p_y);
            this.n = n;
        }

        public override string ToString()
        {
            return "Droite : {n : " + n + ", p :" + p + "}";
        }
    }
}
