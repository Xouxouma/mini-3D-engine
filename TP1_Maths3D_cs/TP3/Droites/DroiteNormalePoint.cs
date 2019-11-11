using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteNormalePoint
    {
        private VectCartesien q;
        private VectCartesien n;

        public DroiteNormalePoint(VectCartesien n, VectCartesien q)
        {
            if (n.getDim() != 2)
                throw new System.ArgumentException("VectCartesien n must be of size 2.");
            if (q.getDim() != 2)
                throw new System.ArgumentException("VectCartesien q must be of size 2.");

            this.q = q;
            if (n.magnitude() != 1)
                this.n = n.normalize();
            else this.n = n;
        }

        public override string ToString()
        {
            return "Droite : {n : " + n + ", q :" + q + "}";
        }

        // Conversions
        public DroiteNormaleDistance ToDroiteNormaleDistance()
        {
            double distance = q * n;
            return new DroiteNormaleDistance(n, distance);
        }
    }
}
