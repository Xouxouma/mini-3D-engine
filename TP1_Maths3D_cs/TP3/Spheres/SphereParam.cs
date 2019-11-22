using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class SphereParam
    {
        private VectCartesien centre;
        private double rayon;

        public SphereParam(VectCartesien centre, double rayon)
        {
            if (centre.getDim() != 3)
                throw new System.ArgumentException("VectCartesien centre must be of size 2.");
            this.centre = (VectCartesien)centre;
            this.rayon = rayon;
        }
        public override string ToString()
        {
            return "SphereParam : {" + centre + "; " + rayon + "}";
        }
        // Conversions
        public SphereImplicite ToSphereImplicite()
        {
            double x = centre[0];
            double y = centre[1];
            double z = centre[2];
            return new SphereImplicite(x, y, z, rayon);
        }
    }
}
