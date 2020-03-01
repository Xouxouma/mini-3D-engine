using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Polygone
    {
        Triangle[] triangles;

        public Polygone(params Triangle[] triangles)
        {
            this.triangles = triangles;
        }

        public override string ToString()
        {
            string str = "";
            foreach (Triangle t in triangles)
            {
                str += t + " ; " ;
            }
            return "Polygone : # " + str + "#";
        }
    }
}
