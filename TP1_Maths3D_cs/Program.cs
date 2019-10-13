using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Maths3D_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start main.");

            // Définir vecteurs 3d et 4d
            Vect vec3 = new Vect(3, 4, 5);
            Vect vec3b = new Vect(2, 3, 6);
            Vect vec4 = new Vect(3, 4, 5, 7);

            // Accçès aux composantes individuelles
            vec3[0] = 1;
            Console.WriteLine(vec3[0]);

            // Afficher le contenu
            Console.WriteLine(vec3.ToString());
            Console.WriteLine(vec3b.ToString());
            Console.WriteLine(vec4.ToString());

            // Négative d'un vecteur
            Console.WriteLine("negative vec3: "+ -vec3);

            Console.WriteLine("magnitude vec3 :" + vec3.magnitude());

            Console.WriteLine("Sum vec3 and vec3 :" + (vec3+vec3));

            Console.WriteLine("Difference vec3 and vec3b :" + (vec3-vec3b));

            Console.WriteLine("Distance vec3 and vec3b :" + vec3.distance(vec3b));

            Console.WriteLine("Produit scalaire de vec3 et vec3b :" + vec3b*vec3b);

            Console.WriteLine("Produit scalaire de vec3b et vec3b :" + vec4*vec4);

            Console.WriteLine("Produit vectoriel de vec3 et vec3b :" + vec3.produit_vectoriel(vec3b));

            Console.WriteLine("vec3 dim:" + vec3.getDim());

            Vect[] vects = new Vect[2];
            vects[0] = vec3;
            vects[1] = vec3b;

            // Matrix

            Matrix mat34 = new Matrix(vec3, vec3b, vec3, vec3b);
            Matrix mat33 = new Matrix(vec3*2, 0.5*vec3b, vec3b-vec3);
            Matrix mat44 = new Matrix(vec4, 2-vec4+3, vec4+vec4/4, -3*vec4*2);

            Console.WriteLine("mat33 = " + mat33);
            Console.WriteLine("mat34 = " + mat34);
            Console.WriteLine("mat44 = " + mat44);

            // Operations on matrix

            Console.WriteLine("mat34 + mat34 = " + (mat34 + mat34));
            Console.WriteLine("mat34 - mat34 = " + (mat34 + mat34));
            Console.WriteLine("mat33 * mat33 = " + (mat33 * mat33));

            // Program closes auto close without that
            while (true)
            {

            }
            
        }
    }
}
