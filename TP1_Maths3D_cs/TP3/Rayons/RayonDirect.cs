using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class RayonDirect
    {
        private Point2D p_org;
        private Point2D p_end;

        public RayonDirect(Point2D p_org, Point2D p_end)
        {
            this.p_org = p_org;
            this.p_end = p_end;
        }

        public RayonDirect(double p_org_x, double p_org_y, double p_end_x, double p_end_y)
        {
            this.p_org = new Point2D(p_org_x, p_org_y);
            this.p_end = new Point2D(p_end_x, p_end_y);
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
    }
}
