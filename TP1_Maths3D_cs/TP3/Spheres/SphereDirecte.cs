using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class SphereDirecte
    {
        private VectCartesien centre;
        private VectCartesien point;

        public SphereDirecte(VectCartesien centre, VectCartesien point)
        {
            if (centre.getDim() != 3)
                throw new System.ArgumentException("VectCartesien centre must be of size 2.");
            if (point.getDim() != 3)
                throw new System.ArgumentException("VectCartesien point must be of size 2.");
            this.centre = centre;
            this.point = point;
        }
        public override string ToString()
        {
            return "CercleDirect: {" + centre + "; " + point + "}";
        }
        // Conversions
        public CercleParam ToCercleParam()
        {
            VectCartesien diff = point - centre;
            double rayon = diff.magnitude();
            return new CercleParam(centre, rayon);
        }
        public CercleImplicite ToCercleImplicite()
        {
            return ToCercleParam().ToCercleImplicite();
        }

    }
}
