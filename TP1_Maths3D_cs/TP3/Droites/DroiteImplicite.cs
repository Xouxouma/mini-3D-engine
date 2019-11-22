using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class DroiteImplicite
    {
        private double a;
        private double b;
        private double c;

        public DroiteImplicite(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public override string ToString()
        {
            return "Droite : " + a + "x + " + b + "y + " + c + " = 0";
        }

        // Conversions
        public DroiteReduite ToDroiteReduite()
        {
            double m = -a / b;
            double y0 = c / b;
            return new DroiteReduite(y0, m);
        }
        public DroiteNormaleDistance ToDroiteNormaleDistance()
        {
            VectCartesien n = new VectCartesien(a, b);
            double distance = c / n.magnitude();
            return new DroiteNormaleDistance(n, distance);
        }

        // semble fausse , et inutile
       /* public DroiteMediatrice ToDroiteMediatrice()
        {
            double qx = 1;
            double rx = b + qx;
            double ry = c - a * b - a * qx;
            double qy = a + ry;
            VectCartesien q = new VectCartesien(qx, qy);
            VectCartesien r = new VectCartesien(rx, ry);
            return new DroiteMediatrice(r, q);
        }*/

        // Evaluation
        public double evaluate_y(double x)
        {
            return (-a * x - c) / b;
        }
        public double evaluate_x(double y)
        {
            return (-b * y - c) / a;
        }

        public RayonDirect GetRayonDirect(double x0, double x1)
        {
            VectCartesien p0 = new VectCartesien(x0, evaluate_y(x0));
            VectCartesien p1 = new VectCartesien(x1, evaluate_y(x1));
            return new RayonDirect(p0, p1);
        }
    }
}
