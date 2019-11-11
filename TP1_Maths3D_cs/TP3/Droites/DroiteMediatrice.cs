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

        public DroiteMediatrice(Point2D p0, Point2D p1)
        {
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
            double a = p0[1] - p1[1];
            double b = p1[0] - p0[0];
            double c = p1[0] * p0[1] - p1[1] * p0[0];
            return new DroiteImplicite(a, b, c);
        }
    }
}
