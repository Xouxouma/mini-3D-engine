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

            // Cercles
            Console.WriteLine(new CercleDirect(p1, p2));
            Console.WriteLine(new CercleParam(p1,  4.5));
            Console.WriteLine(new CercleImplicite(10,2,4));

            // AABB
            Console.WriteLine("\n__AABB");
            VectCartesien v1 = new VectCartesien(7, 11, -5);
            VectCartesien v2 = new VectCartesien(2, 3, 8);
            VectCartesien v3 = new VectCartesien(-3, 3, 1);
            VectCartesien v4 = new VectCartesien(-5, -7, 0);
            VectCartesien v5 = new VectCartesien(6, 3, 4);
            Matrix rot = Matrix.rotation_z(Math.PI/4);

            AABB aabb = new AABB(v1, v2, v3, v4, v5);
            Console.WriteLine(aabb);
            Console.WriteLine("après transfo (par M = " + rot + "): \n" + aabb.Rotate(rot));

            /*AABB aabb_test = new AABB(v1 * rot, v2 * rot, v3 * rot, v4 * rot, v5 * rot);
            Console.WriteLine("aabb from vect rotated in first place : " + aabb_test);
            Console.WriteLine("aabb == aabb__test ? : " + (aabb == aabb_test));
            Console.WriteLine("Vects rotated : " + v1*rot + v2*rot + v3*rot + v4*rot + v5*rot);*/

            // Plans
            Console.WriteLine("__Plans");
            Point3D A = new Point3D(6, 10, -2);
            Point3D B = new Point3D(3, -1, 17);
            Point3D C = new Point3D(-8, 8, 0);
            Point3D D = new Point3D(3, 4, 5);
            PlanImplicite plani = new PlanImplicite(A, 5.4);
            PlanImplicite plan = new PlanImplicite(A, B, C);
            Console.WriteLine("plani = " + plani);
            Console.WriteLine("plan = " + plan);
            Console.WriteLine(D.ToString() + " est à une distance du plan de : " + plan.Distance(D));

            // Triangles
            Console.WriteLine("__Triangles");
            Point3D E = new Point3D(13.6, -0.46, 17.11);
            Triangle t = new Triangle(A, B, C);
            //Triangle t = new Triangle(new Point2D(1,2), new Point2D(3,5), new Point2D(4, 3));
            //Point2D E = new Point2D(2.6667,3.33333);
            Console.WriteLine(t);
            Console.WriteLine(t.Barycentre());
            Console.WriteLine("Coordonnées barycentriques de (" + E + ") : " + t.ToBarycentrique(E));
            Console.WriteLine("incenter : " + t.Incenter());
            Console.WriteLine("circumcenter : " + t.Circumcenter());

            // Mesh
            VectCartesien A2 = B + C - A;
            Triangle t2 = new Triangle(A2, B, C);
            Polygone para = new Polygone(t,t2);
            Console.WriteLine("parallelogramme = " + para);
        }

      
    }
}
