using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class AABB
    {
        VectCartesien p_min;
        VectCartesien p_max;
        int dim;

        public override String ToString()
        {
            return "AABB : {" + p_min + " ; " + p_max + "}";
        }
        public VectCartesien getMin()
        {
            return p_min;
        }

        public VectCartesien getMax()
        {
            return p_max;
        }

        // Vecteurs de l'AABB
        public VectCartesien Centre()
        {
            return (p_min + p_max) / 2;
        }

        public VectCartesien S()
        {
            return p_max - p_min;
        }
        public VectCartesien Radial()
        {
            return p_max - Centre();
        }

        public AABB(params VectCartesien[] points)
        {
            double min = Double.NegativeInfinity;
            double max = Double.PositiveInfinity;
            VectCartesien pmax = new VectCartesien(min, min, min);
            VectCartesien pmin = new VectCartesien(max, max, max);
            dim = points[0].getDim();
            foreach(VectCartesien p in points)
            {
                //if (p.getDim() != 3)
                //    throw new System.ArgumentException("VectCartesien point must be of size 3.");
                if (p.getDim() != dim)
                    throw new System.ArgumentException("VectCartesien points must be of same size.");
                for (int k = 0; k < dim; k++)
                {
                    if (pmin[k] > p[k])
                        pmin[k] = p[k];
                    if (pmax[k] < p[k])
                        pmax[k] = p[k];
                }
            }
            this.p_min = pmin;
            this.p_max = pmax;
        }
        public AABB Rotate(Matrix m)
        {
            VectCartesien new_min = new VectCartesien(0,0,0);
            VectCartesien new_max = new VectCartesien(0,0,0);

            if (m.getRow(0).getDim() != dim || m.getCol(0).getDim() != dim)
                throw new System.ArgumentException("Matrix m must be of size dim*dim.");
            
            for (int i = 0; i<dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (m[i, j] > 0)
                    {
                        new_min[i] += m[i, j] * p_min[i];
                        new_max[i] += m[i, j] * p_max[i];
                    }
                }
            }
            this.p_min = new_min;
            this.p_max = new_max;

            return this;
        }
    }
}
