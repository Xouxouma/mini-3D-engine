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

        public Polygone ToPolygone(int nbDecoupes = 12)
        {
            List<Triangle> triangles = new List<Triangle>();
            VectCartesien[,] points = new VectCartesien[nbDecoupes, nbDecoupes];
            double lon, lat, x, y, z;
            for (int i = 0; i < nbDecoupes; i++)
            {
                lat = -((nbDecoupes - 1 - i) / (double)(nbDecoupes - 1)) * Math.PI + (i / ((double)(nbDecoupes - 1)) * Math.PI);
                for (int j = 0; j < nbDecoupes; j++)
                {
                    lon = -((nbDecoupes - 1 - j) / (double)(nbDecoupes - 1)) * Math.PI / 2 + (j / ((double)(nbDecoupes-1)) * Math.PI / 2 );
                    x = rayon * Math.Sin(lat) * Math.Cos(lon) + centre[0];
                    y = rayon * Math.Sin(lat) * Math.Sin(lon) + centre[1];
                    z = rayon * Math.Cos(lat) + centre[2];
                    points[i, j] = new VectCartesien(x, y, z);
                }
            }
            
            for (int i = 0; i < nbDecoupes - 1; i++)
            {
                for (int j = 0; j < nbDecoupes-1; j++)
                {
                    triangles.Add(new Triangle(points[i, j], points[(i + 1) % nbDecoupes, j], points[i, (j + 1) % nbDecoupes]));
                    triangles.Add(new Triangle(points[(i + 1) % nbDecoupes, j], points[i, (j + 1) % nbDecoupes], points[(i + 1) % nbDecoupes, (j + 1) % nbDecoupes]));
                }
            }

            return new Polygone(triangles.ToArray());
        }
    }
}
