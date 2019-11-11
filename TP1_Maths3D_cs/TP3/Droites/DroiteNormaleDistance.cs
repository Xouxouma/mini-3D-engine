using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteNormaleDistance
    {
        private double dist;
        private VectCartesien n;

        public DroiteNormaleDistance(VectCartesien n, double dist)
        {
            if (n.getDim() != 2)
                throw new System.ArgumentException("VectCartesien n must be of size 2.");

            this.dist = dist;
            if (n.magnitude() != 1)
                this.n = n.normalize();
            else this.n = n;
        }

        public DroiteNormaleDistance(double n_x, double n_y, double dist)
        {
            this.dist = dist;
            this.n = new VectCartesien(n_x, n_y);
        }
        public override string ToString()
        {
            return "Droite : {n : " + n + ", dist :" + dist + "}";
        }

        // Conversions
        public DroiteNormalePoint ToDroiteNormalePoint()
        {
            VectCartesien q = dist / n;
            return new DroiteNormalePoint(n, q);
        }
    }
}
