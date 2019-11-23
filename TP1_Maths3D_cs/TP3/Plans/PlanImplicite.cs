using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class PlanImplicite
    {
        VectCartesien n;
        double d;

        public PlanImplicite(VectCartesien n, double d)
        {
            if (n.getDim() != 3)
                throw new System.ArgumentException("VectCartesien n must be of size 3");

            if (n.magnitude() != 0)
                n = n.normalize();
            this.n = n;
            this.d = d;
        }
        public PlanImplicite(double a, double b, double c, double d)
        {
            this.n = new VectCartesien(a, b, c).normalize();
            this.d = d;
        }
        // Plan 3 points
        private static PlanImplicite From3Points(VectCartesien p1, VectCartesien p2, VectCartesien p3)
        {
            if (p1.getDim() != 3 || p2.getDim() != 3 || p3.getDim() != 3)
                throw new System.ArgumentException("VectCartesien p1 must be of size 3");
            VectCartesien e3 = p2 - p1;
            VectCartesien e1 = p3 - p2;
            VectCartesien n = e3.produit_vectoriel(e1).normalize();
            double d = -n[0] * p1[0] - n[1] * p1[1] - n[2] * p1[2];
            return new PlanImplicite(n, d);
        }
        private static PlanImplicite FromNPoints(params VectCartesien[] vecs)
        {
            VectCartesien n = VectCartesien.zeros(3);
            VectCartesien somme_points = VectCartesien.zeros(3);
            double d;

            for (int i = 0; i < vecs.Length; i++)
            {
                if (vecs[i].getDim() != 3)
                    throw new System.ArgumentException("VectCartesien v must be of size 3");

                for (int axe = 0; axe < 3; axe++)
                {
                    n[axe] += (vecs[i][(axe + 2) % 3] + vecs[i + 1][(axe + 2) % 3]) *
                        (vecs[i][(axe + 1) % 3] - vecs[i + 1][(axe + 1) % 3]);
                    somme_points += vecs[i];
                }
            }

            d = (somme_points * n) / vecs.Length;
            return new PlanImplicite(n, d);
        }
        public PlanImplicite(params VectCartesien[] vecs)
        {
            PlanImplicite plan;

            if (vecs.Length < 3)
                throw new System.ArgumentException("VectCartesien n must be of size 3+");

            if (vecs.Length == 3)
            {
                plan = From3Points(vecs[0], vecs[1], vecs[2]);
            }
            else
            {
                plan = FromNPoints(vecs);
            }
            this.n = plan.n;
            this.d = plan.d;
        }

        public override string ToString()
        {
            return "Plan : {" + n + " ; " + d + "}";
        } 
        // Operations
        public double Distance(VectCartesien point)
        {
            if (point.getDim() < 3)
                throw new System.ArgumentException("VectCartesien point must be of size 3+");

            double dist = -n[0] * point[0] - n[1] * point[1] - n[2] * point[2];
            return dist;
        }
    }
}
