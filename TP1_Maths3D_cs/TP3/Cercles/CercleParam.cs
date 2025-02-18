﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class CercleParam
    {
        private VectCartesien centre;
        private double rayon;

        public CercleParam(VectCartesien centre, double rayon)
        {
            if (centre.getDim() != 2)
                throw new System.ArgumentException("VectCartesien centre must be of size 2.");
            this.centre = (VectCartesien)centre;
            this.rayon = rayon;
        }
        public override string ToString()
        {
            return "CercleParam : {" + centre + "; " + rayon + "}";
        }
        // Conversions
        public CercleImplicite ToCercleImplicite()
        {
            double x = centre[0];
            double y = centre[1];
            return new CercleImplicite(x, y, rayon);
        }
    }
}
