using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Point2D : VectCartesien
    /*
    * Classe inutilisée pour le moment car c'est plus simple de faire les opérations qu'entre VectCartesien
    */
    {
        double x;
        double y;
        public Point2D(double x, double y) : base(x, y) { }
        public Point2D(VectCartesien v)
        {
            if (v.getDim() != 2)
                throw new System.ArgumentException("VectCartesien v must be of size 2.");
            this.x = v[0];
            this.y = v[1];
        }

        public double GetX()
        {
            return x;
        }
        public double GetY()
        {
            return y;
        }
    }
}
