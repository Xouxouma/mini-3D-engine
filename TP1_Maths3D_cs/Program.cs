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

            Console.WriteLine("vec3 normalisé:" + vec3.normalize());

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

            Matrix id3 = new Matrix(3, 3, 1);

            Matrix mat43 = new Matrix(vec3, vec3b, vec3, vec3b);
            Matrix mat33 = new Matrix(vec3*2, 0.5*vec3b, vec3b-vec3);
            Matrix mat44 = new Matrix(vec4, 2-vec4+3, vec4+vec4/4, -3*vec4*2);
            Matrix mat34 = new Matrix(vec4, 2-vec4+3, vec4+vec4/4);

            Console.WriteLine("mat33 = " + mat33);
            Console.WriteLine("mat34 = " + mat34);
            Console.WriteLine("mat43 = " + mat43);
            Console.WriteLine("mat44 = " + mat44);
            Console.WriteLine("identité 3x3 = " + id3);

            // Operations on matrix

            Console.WriteLine("mat34 + mat34 = " + (mat34 + mat34));
            Console.WriteLine("mat34 - mat34 = " + (mat34 + mat34));
            Console.WriteLine("mat33 * mat33 = " + (mat33 * mat33));
            Console.WriteLine("mat34 * mat43 = " + (mat34 * mat43));
            Console.WriteLine("mat34 transp true " + mat34.transposer());
            Console.WriteLine("mat34  " + mat34);
            Console.WriteLine("mat34 transp false " + mat34.transposer(false));
            Console.WriteLine("mat34 " + mat34);

            // Créations de matrices

            // Matrices de rotations
            Matrix Rx = Matrix.rotation_x(45);
            Matrix Ry = Matrix.rotation_y(45);
            Matrix Rz = Matrix.rotation_z(45);

            Matrix Rrand = Matrix.rotation(45,1,2,3);

            Console.WriteLine("Rx = " + Rx);
            Console.WriteLine("Ry = " + Ry);
            Console.WriteLine("Rz = " + Rz);
            Console.WriteLine("Rrand = " + Rrand);

            // Matrices de redimensionnement
            Matrix S_ord = Matrix.ordinal_scale(1.75, 2.5, -4.2);
            Console.WriteLine("S_ord = " + S_ord);
            Matrix S = Matrix.scale(-1, new Vect(1.5, -2, 3.4));
            Console.WriteLine("S_rand = " + S);

            // Matrices de projection orthographique
            Matrix Px = Matrix.orthographic_projection(0);
            Console.WriteLine("Px = " + Px);
            Matrix Prand = Matrix.orthographic_projection(new Vect(1.75, 2.5, -4.2));
            Console.WriteLine("Prand = " + Prand);

            // Matrices de réflexion
            Matrix Rex = Matrix.reflect(1);
            Console.WriteLine("Rex = " + Rex);
            Matrix Rerand = Matrix.reflect(new Vect(1,1,0));
            Console.WriteLine("Rerand = " + Rerand);

            // Matrices de cisaillement
            Console.WriteLine("cisaillement xy = " + Matrix.shearing_xy(3,2));
            Console.WriteLine("cisaillement xz = " + Matrix.shearing_xz(3,2));
            Console.WriteLine("cisaillement yz = " + Matrix.shearing_yz(3,2));

            // Augmentation
            Console.WriteLine("mat33 = " + mat33 + "\n mat33 augmenté :" + mat33.increase_dim());




            // Appliquer une transformation linéaire ) un vecteur
            Console.WriteLine("vec3 : " + vec3);
            Console.WriteLine("vec3 avec rotation de 45° autour de x: " + vec3*Rx);

            // Program closes auto close without that
            while (true)
            {

            }
            
        }
    }
}
