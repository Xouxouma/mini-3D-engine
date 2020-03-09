using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Triangle
    {
        private VectCartesien[] v;
        private int dim;

        public Triangle(VectCartesien v1, VectCartesien v2, VectCartesien v3)
        {
            int size = v1.getDim();
            if ((size != 3 && size != 2) || v2.getDim() != size || v3.getDim() != size)
                throw new System.ArgumentException("VectCartesien v1, v2, v3 must be of dim 2 or 3, and all the same dim");

            this.dim = size;
            this.v = new VectCartesien[] { v1, v2, v3 };
        }

        public override string ToString()
        {
            return "Triangle : {" + v[0] + " ; " + v[1] + ";" + v[2] + "}";
        }

        public VectCartesien[] getVertices()
        {
            return v;
        }

        // Calcul attributs de base
        public VectCartesien[] getEdges()
        {
            VectCartesien[] e = new VectCartesien[3];
            for(int i = 0; i < 3; i++)
                e[i] = v[(i + 2) % 3] - v[(i + 1) % 3];
            return e;
        }
        public double[] getLenghts()
        {
            double[] l = new double[3];
            VectCartesien[] e = getEdges();
            for (int i = 0; i < 3; i++)
                l[i] = e[i].magnitude();
            return l;
        }
        public double[] getAngles()
        {
            double[] a = new double[3];
            double[] l = getLenghts();
            for (int i = 0; i < 3; i++)
            {
                int i2 = (i + 2) % 3;
                int i1 = (i + 1) % 3;
                double cosTetha = (- l[i] * l[i] + l[i2] * l[i2] + l[i1] * l[i1]) / (2 * l[i2] * l[i1]);
                a[i] = Math.Acos(cosTetha);
            }
            return a;
        }
        // Attributs
        public double Perimetre()
        {
            double[] l = getLenghts();
            double p = l[0] + l[1] + l[2];
            return p;
        }
        public double Aire()
        {
            double s = Perimetre() / 2;
            double[] l = getLenghts();
            double product = s;
            for (int i = 0; i < 3; i++)
                product *= s - l[i];
            return Math.Sqrt(product);
        }

        // Coordonnées Barycentriques
        public VectCartesien ToBarycentrique2D(VectCartesien p)
        {
            if (p.getDim() != 2)
                throw new System.ArgumentException("VectCartesien point must be of dim 2");
            VectCartesien numerateur = VectCartesien.zeros(3);
            for (int i =0; i < 3; i++)
            {
                int i2 = (i + 2) % 3;
                int i1 = (i + 1) % 3;
                numerateur[i] = (p[1]-v[i2][1])*(v[i1][0]-v[i2][0]) + (v[i1][1]-v[i2][1])*(v[i2][0]-p[0]);
            }
            double denominateur = 2 * Aire();
            return numerateur / denominateur;
        }
        public VectCartesien ToBarycentrique3D(VectCartesien p)
        {
            if (p.getDim() != 3)
                throw new System.ArgumentException("VectCartesien point must be of dim 3");

            // Calcul d'une normale
            VectCartesien[] e = getEdges();
            VectCartesien n = e[0].produit_vectoriel(e[1]).normalize();
            // Vecteurs d, entre p et chaque sommet
            VectCartesien[] d = new VectCartesien[3];
            for (int i = 0; i < 3; i++)
                d[i] = p - v[i];
            // Calcul des aires des sous-triangles
            VectCartesien numerateur = VectCartesien.zeros(3);
            for (int i = 0; i < 3; i++)
            {
                int i2 = (i + 2) % 3;
                int i1 = (i + 1) % 3;
                numerateur[i] = (e[i].produit_vectoriel(d[i2])) * n;
            }
            double denominateur = e[0].produit_vectoriel(e[1]) * n;
            return numerateur / denominateur;
        }
        public VectCartesien ToBarycentrique(VectCartesien point)
        {
            if (point.getDim() != dim)
                throw new System.ArgumentException("VectCartesien point must be of same dim than the triangle used.");
            
            if (dim == 2)
            {
                return ToBarycentrique2D(point);
            }
            else
            {
                return ToBarycentrique3D(point);
            }
        }
        // Points spéciaux
        public VectCartesien Barycentre()
        {
            return (v[0] + v[1] + v[2]) / 3;
        }
        public VectCartesien Incenter()
        {
            double[] l = getLenghts();
            double p = 0;
            VectCartesien c = VectCartesien.zeros(3);
            for (int i=0; i < 3; i++)
            {
                p += l[i];
                c += l[i] * v[i];
            }
            return c / p;
        }
        public VectCartesien Circumcenter()
        {
            VectCartesien[] e = getEdges();
            VectCartesien d = VectCartesien.zeros(3);
            for (int i = 0; i < 3; i++)
                d[i] = -e[(i + 1) % 3] * e[(i + 2) % 3];
            VectCartesien c = VectCartesien.zeros(3);
            for (int i = 0; i < 3; i++)
                c[i] = -d[(i + 1) % 3] * d[(i + 2) % 3];
            VectCartesien num = VectCartesien.zeros(3);
            for (int i = 0; i < 3; i++)
                num += (c[(i + 1) % 3] +c[(i + 2) % 3]) * v[i];
            return num / (2*c[0]+c[1]+c[2]);
            return VectCartesien.zeros(3);
        }
    }
}
