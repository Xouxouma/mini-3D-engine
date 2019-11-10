using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class TP3
    {
        public static void exec()
        {
            Console.WriteLine(" TP 3");

            // Points
            Console.WriteLine("__Points");
            Point2D p1 = new Point2D(1.5, 2.6);
            Point2D p2 = new Point2D(0.5, 2.4);
            Console.WriteLine("p1 = " + p1);
            Console.WriteLine("p1 + P2 = " + (p1+p2));

            // Rayons
            Console.WriteLine("Rayons");
            RayonDirect rd = new RayonDirect(p1, p2);
            RayonParam rp = new RayonParam(p1, new VectCartesien(10.5,4.2));
            Console.WriteLine("rd = " + rd + " ; ToRayonParam : " + rd.ToRayonParam());
            Console.WriteLine("rp = " + rp + " ; ToRayonDirect : " + rp.ToRayonDirect());
            
            // Droites
            Console.WriteLine("Droite");
            DroiteImplicite di = new DroiteImplicite(10, 2, 3);
            DroiteReduite dr = new DroiteReduite(4, 3);
            DroiteNorm dn = new DroiteNorm(p1, new VectCartesien(10.5, 4.2));
            Console.WriteLine("di = " + di);
            Console.WriteLine("dr = " + dr);


            VectCartesien v = new VectCartesien(1, 2);
            VectCartesien vtest = p1 + v;
            Point2D p = new Point2D(p1 + p2);
            Point2D ptest = new Point2D(p1 + v);
        }

      
    }
}
