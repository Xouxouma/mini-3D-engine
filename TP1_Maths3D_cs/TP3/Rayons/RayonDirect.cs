using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class RayonDirect
    {
        private VectCartesien p_org; // point 2d
        private VectCartesien p_end; // point 2d

        public RayonDirect(VectCartesien p_org, VectCartesien p_end)
        {
            if (p_org.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p_org must be of size 2.");
            if (p_end.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p_end must be of size 2.");
            this.p_org = p_org;
            this.p_end = p_end;
        }

        public RayonDirect(double p_org_x, double p_org_y, double p_end_x, double p_end_y)
        {
            this.p_org = new VectCartesien(p_org_x, p_org_y);
            this.p_end = new VectCartesien(p_end_x, p_end_y);
        }

        public override String ToString()
        {
            return "Rayon : " + p_org + " -> " + p_end;
        }

        // Conversions
        public RayonParam ToRayonParam()
        {
            return new RayonParam(p_org, p_end - p_org);
        }
        public DroiteImplicite ToDroiteImplicite()
        {
            return ToRayonParam().ToDroiteImplicite();
        }
    }
}
