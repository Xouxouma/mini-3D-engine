using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moteur3D;

namespace Moteur3D
{
    class TP2
    {
        public static void exec()
        {
            Console.WriteLine(" TP 2");

            // vecteurs polaires
            VectPolaire vp = new VectPolaire(5.0, Math.PI * 3 / 2, Math.PI / 4);
            Console.WriteLine("vp = " + vp);
            Console.WriteLine("vp to cartésien= " + vp.toCartesien());
            Console.WriteLine("vp to cartésien to polaire = " + vp.toCartesien().toPolaire());

            // Angles d'Euler
            Console.WriteLine("\n  Angles d'Euler :");
            AngleEuler eul0 = new AngleEuler();
            AngleEuler eul = new AngleEuler(45.0,90.0,180.0);
            Console.WriteLine(eul0);
            Console.WriteLine("eul.getBank : " + eul.getBank());

            // Quatérions
            Console.WriteLine("\nQuaternions");
            Quaternion qI = new Quaternion();
            Quaternion q = new Quaternion(10, 45, 90, 90);
            Console.WriteLine("Quaterion identité : "+qI);
            Console.WriteLine("q = "+q);
        }
    }
}
