using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class TD2
    {
        public static void exec()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("TD2 : \n");

            Console.WriteLine("1.");
            VectPolaire vp1 = new VectPolaire(4, Utils.ConvertDegreesToRadians(207));
            VectPolaire vp2 = new VectPolaire(-5, Utils.ConvertDegreesToRadians(-720));
            VectPolaire vp3 = new VectPolaire(0, Utils.ConvertDegreesToRadians(45.2));
            VectPolaire vp4 = new VectPolaire(12.6, 11 * Math.PI / 4);
            vp1.toCanonique();
            vp2.toCanonique();
            vp3.toCanonique();
            vp4.toCanonique();
            Console.WriteLine(" (a) " + vp1);
            Console.WriteLine(" (b) " + vp2);
            Console.WriteLine(" (c) " + vp3);
            Console.WriteLine(" (c) " + vp4);

            Console.WriteLine();
            Console.WriteLine("2.");
            Console.WriteLine(" (a) " + (new VectPolaire(1, Utils.ConvertDegreesToRadians(45))).toCartesien());
            Console.WriteLine(" (b) " + (new VectPolaire(3, Utils.ConvertDegreesToRadians(0))).toCartesien());
            Console.WriteLine(" (c) " + (new VectPolaire(4, Utils.ConvertDegreesToRadians(90))).toCartesien());
            Console.WriteLine(" (d) " + (new VectPolaire(10, Utils.ConvertDegreesToRadians(-30))).toCartesien());
            Console.WriteLine(" (e) " + (new VectPolaire(5.5, Math.PI).toCartesien()));

            Console.WriteLine();
            Console.WriteLine("3.");
            VectPolaire vp5 = (new VectCartesien(10, 20)).toPolaire();
            VectPolaire vp6 = (new VectCartesien(-12, -5)).toPolaire();
            VectPolaire vp7 = (new VectCartesien(0, 4.5)).toPolaire();
            VectPolaire vp8 = (new VectCartesien(-3, 4)).toPolaire();
            VectPolaire vp9 = (new VectCartesien(0, 0)).toPolaire();
            VectPolaire vp10 = (new VectCartesien(-5280, 0)).toPolaire();
            vp5.toCanonique();
            vp6.toCanonique();
            vp7.toCanonique();
            vp8.toCanonique();
            vp9.toCanonique();
            vp10.toCanonique();
            Console.WriteLine(" (a) " + vp5);
            Console.WriteLine(" (b) " + vp6);
            Console.WriteLine(" (c) " + vp7);
            Console.WriteLine(" (d) " + vp8);
            Console.WriteLine(" (e) " + vp9);
            Console.WriteLine(" (f) " + vp10);

            Console.WriteLine();
            Console.WriteLine("4.");

            VectSpherique vs1 = (new VectCartesien(Math.Sqrt(2), 2 * Math.Sqrt(3), -Math.Sqrt(2))).toSpherique();
            VectSpherique vs2 = (new VectCartesien(2 * Math.Sqrt(3), 6, -4)).toSpherique();
            VectSpherique vs3 = (new VectCartesien(-1, -1, -1)).toSpherique();
            VectSpherique vs4 = (new VectCartesien(2, -2 * Math.Sqrt(3), 4)).toSpherique();
            VectSpherique vs5 = (new VectCartesien(-Math.Sqrt(3), -Math.Sqrt(3), 2 * Math.Sqrt(2))).toSpherique();
            VectSpherique vs6 = (new VectCartesien(3, 4, 12)).toSpherique();
            vs1.toCanonique();
            vs2.toCanonique();
            vs3.toCanonique();
            vs4.toCanonique();
            vs5.toCanonique();
            vs6.toCanonique();
            Console.WriteLine(" (a) " + vs1);
            Console.WriteLine(" (b) " + vs2);
            Console.WriteLine(" (c) " + vs3);
            Console.WriteLine(" (d) " + vs4);
            Console.WriteLine(" (e) " + vs5);
            Console.WriteLine(" (f) " + vs6);

            Quaternion q1 = new Quaternion();
        }
    }
}
