﻿using System;
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

            VectCartesien v = new VectCartesien(2,3);
            
            // Points
            Console.WriteLine("__Points");
            Point2D p1 = new Point2D(5, 3);
            Point2D p2 = new Point2D(0.5, 2.4);
            p1 = new Point2D(2, 4);
            p2 = new Point2D(4, 2);
            Console.WriteLine("p1 = " + p1);
            Console.WriteLine("p1 + P2 = " + (p1+p2));

            // Rayons
            Console.WriteLine("__Rayons");
            RayonDirect rd = new RayonDirect(p1, p2);
            RayonParam rp = new RayonParam(p1, new VectCartesien(7,-5));
            Console.WriteLine("rd = " + rd + " ; ToRayonParam : " + rd.ToRayonParam());
            Console.WriteLine("rp = " + rp + " ; ToRayonDirect : " + rp.ToRayonDirect());
            
            // Droites
            Console.WriteLine("__Droite");
            DroiteImplicite di = new DroiteImplicite(4, 7, 42);
            DroiteReduite dr = new DroiteReduite(4, 3);
            DroiteNormaleDistance dnd = new DroiteNormaleDistance(v, 5);
            DroiteNormalePoint dnp = new DroiteNormalePoint(v,p1);
            DroiteMediatrice dm = new DroiteMediatrice(p1, p2);
            Console.WriteLine("di = " + di + " ; ToDroiteReduite : " + di.ToDroiteReduite() + " ; ToDroiteImplicite : " + di.ToDroiteReduite().ToDroiteImplicite());
            Console.WriteLine("dr = " + dr);
            Console.WriteLine("dnd = " + dnd + " ; ToNormalPoint : " + dnd.ToDroiteNormalePoint() + " ; ToNormalDist : " + dnd.ToDroiteNormalePoint().ToDroiteNormaleDistance());
            Console.WriteLine("dnd = " + dnd + " ; ToImplicite : " + dnd.ToDroiteImplicite() + " ; ToNormalDist : " + dnd.ToDroiteImplicite().ToDroiteNormaleDistance());
            Console.WriteLine("dnp = " + dnp + " ; ToDND : " + dnp.ToDroiteNormaleDistance() + "; ToDNP : " + dnp.ToDroiteNormaleDistance().ToDroiteNormalePoint());
           // Console.WriteLine("dm = " + dm + " ; ToImplicite : " + dm.ToDroiteImplicite() + " ; ToMediatrice : " + dm.ToDroiteImplicite().ToDroiteMediatrice() + " ; ToImplicite2 : " + dm.ToDroiteImplicite().ToDroiteMediatrice().ToDroiteImplicite());
            Console.WriteLine("\n\ndm = " + dm + " ; ToImplicite : " + dm.ToDroiteImplicite());
            



        }

      
    }
}
