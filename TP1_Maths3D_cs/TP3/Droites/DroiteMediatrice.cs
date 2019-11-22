using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteMediatrice
    {
        private VectCartesien p0;
        private VectCartesien p1;

        public DroiteMediatrice(VectCartesien p0, VectCartesien p1)
        {
            if (p0.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p0 must be of size 2.");
            if (p1.getDim() != 2)
                throw new System.ArgumentException("VectCartesien p1 must be of size 2.");
            this.p0 = p0;
            this.p1 = p1;
        }
        public DroiteMediatrice(double p_org_x, double p_org_y, double p_end_x, double p_end_y)
        {
            this.p0 = new VectCartesien(p_org_x, p_org_y);
            this.p1 = new VectCartesien(p_end_x, p_end_y);
        }
        public override string ToString()
        {
            return "DroiteMédiatrice : {" + p0 + ", " + p1 + "}";
        }
        
        // Conversions
        public DroiteImplicite ToDroiteImplicite()
        {
            // Formule du cours, mauvais résultats ?
            double a = p0[1] - p1[0];
            double b = p1[0] - p0[1];
            double c = p1[0] * p0[1] - p0[0] * p1[1];
            /*double a = p1[0] - p0[0];
            double b = p0[0] - p1[0];
            double c = p0[0] * p0[0] + p0[1] * p0[1] - p1[0] * p1[0] - p1[1] * p1[1];*/
            return new DroiteImplicite(a, b, c);
        }
    }
}
