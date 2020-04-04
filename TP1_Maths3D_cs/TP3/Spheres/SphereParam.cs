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

        public VectCartesien getCentre()
        {
            return centre;
        }

        public double getRayon()
        {
            return rayon;
        }

        public Polygone ToPolygone()
        {
            int total = 10;
            List<Triangle> triangles = new List<Triangle>();
            VectCartesien[,] points = new VectCartesien[total, total];
            double lon, lat, x, y, z;
            for (int i = 0; i < total; i++)
            {
                lon = -((total - i) / (double)total) * Math.PI + (i / (double)total) * Math.PI;
                for (int j = 0; j < total; j++)
                {
                    lat = -((total - j) / (double)total) * Math.PI + (j / (double)total) * Math.PI;
                    x = rayon * Math.Sin(lon) * Math.Cos(lat) + centre[0];
                    y = rayon * Math.Sin(lon) * Math.Sin(lat) + centre[1];
                    z = rayon * Math.Cos(lon) + centre[2];
                    points[j, i] = new VectCartesien(x, y, z);
                }
            }

            for (int j = 0; j < total; j++)
            {
                triangles.Add(new Triangle(points[0, j], points[1, j], points[1, (j + 1) % total]));
            }

            for (int i = 1; i < total - 1; i++)
            {
                for (int j = 0; j < total; j++)
                {
                    triangles.Add(new Triangle(points[i, j], points[i + 1, j], points[i, (j + 1) % total]));
                    triangles.Add(new Triangle(points[i + 1, j], points[i, j], points[i + 1, (j + 1) % total]));
                }
            }

            for (int j = 0; j < total; j++)
            {
                triangles.Add(new Triangle(points[total - 1, j], points[total - 2, j], points[total - 2, (j + 1) % total]));
            }

            return new Polygone(triangles.ToArray());
        }
    }
}
