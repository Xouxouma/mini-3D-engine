using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moteur3D;

namespace Moteur3D
{
    class TP1
    {
        public static void exec()
        {

            // Définir vecteurs 3d et 4d
            VectCartesien vec3 = new VectCartesien(3, 4, 5);
            VectCartesien vec3b = new VectCartesien(2, 3, 6);
            VectCartesien vec4 = new VectCartesien(3, 4, 5, 7);

            // Accçès aux composantes individuelles
            vec3[0] = 1;
            Console.WriteLine(vec3[0]);

            // Afficher le contenu
            Console.WriteLine(vec3.ToString());
            Console.WriteLine(vec3b.ToString());
            Console.WriteLine(vec4.ToString());

            // Négative d'un vecteur
            Console.WriteLine("negative vec3: " + -vec3);

            Console.WriteLine("magnitude vec3 :" + vec3.magnitude());

            Console.WriteLine("vec3 normalisé:" + vec3.normalize());

            Console.WriteLine("Sum vec3 and vec3 :" + (vec3 + vec3));

            Console.WriteLine("Difference vec3 and vec3b :" + (vec3 - vec3b));

            Console.WriteLine("Distance vec3 and vec3b :" + vec3.distance(vec3b));

            Console.WriteLine("Produit scalaire de vec3 et vec3b :" + vec3b * vec3b);

            Console.WriteLine("Produit scalaire de vec3b et vec3b :" + vec4 * vec4);

            Console.WriteLine("Produit vectoriel de vec3 et vec3b :" + vec3.produit_vectoriel(vec3b));

            Console.WriteLine("vec3 dim:" + vec3.getDim());

            VectCartesien[] vects = new VectCartesien[2];
            vects[0] = vec3;
            vects[1] = vec3b;

            // Matrix

            Matrix id3 = new Matrix(3, 3, 1);

            Matrix mat43 = new Matrix(vec3, vec3b, vec3, vec3b);
            Matrix mat33 = new Matrix(vec3 * 2, 0.5 * vec3b, vec3b - vec3);
            Matrix mat44 = new Matrix(vec4, 2 - vec4 + 3, vec4 + vec4 / 4, -3 * vec4 * 2);
            Matrix mat34 = new Matrix(vec4, 2 - vec4 + 3, vec4 + vec4 / 4);

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
            Console.WriteLine("mat34 transp true " + mat34.transposee());
            Console.WriteLine("mat34  " + mat34);
            Console.WriteLine("mat34 transp false " + mat34.transposee(false));
            Console.WriteLine("mat34 " + mat34);

            // Créations de matrices

            // Matrices de rotations
            Matrix Rx = Matrix.rotation_x(45);
            Matrix Ry = Matrix.rotation_y(45);
            Matrix Rz = Matrix.rotation_z(45);

            Matrix Rrand = Matrix.rotation(45, 1, 2, 3);

            Console.WriteLine("Rx = " + Rx);
            Console.WriteLine("Ry = " + Ry);
            Console.WriteLine("Rz = " + Rz);
            Console.WriteLine("Rrand = " + Rrand);

            // Matrices de redimensionnement
            Matrix S_ord = Matrix.ordinal_scale(1.75, 2.5, -4.2);
            Console.WriteLine("S_ord = " + S_ord);
            Matrix S = Matrix.scale(-1, new VectCartesien(1.5, -2, 3.4));
            Console.WriteLine("S_rand = " + S);

            // Matrices de projection orthographique
            Matrix Px = Matrix.orthographic_projection(0);
            Console.WriteLine("Px = " + Px);
            Matrix Prand = Matrix.orthographic_projection(new VectCartesien(1.75, 2.5, -4.2));
            Console.WriteLine("Prand = " + Prand);

            // Matrices de réflexion
            Matrix Rex = Matrix.reflect(1);
            Console.WriteLine("Rex = " + Rex);
            Matrix Rerand = Matrix.reflect(new VectCartesien(1, 1, 0));
            Console.WriteLine("Rerand = " + Rerand);

            // Matrices de cisaillement
            Console.WriteLine("cisaillement xy = " + Matrix.shearing_xy(3, 2));
            Console.WriteLine("cisaillement xz = " + Matrix.shearing_xz(3, 2));
            Console.WriteLine("cisaillement yz = " + Matrix.shearing_yz(3, 2));

            // Augmentation
            Console.WriteLine("mat33 = " + mat33 + "\n mat33 augmenté :" + mat33.increase_dim());


            // Matrice de translation [4 3 2]
            Matrix T = Matrix.translation(new VectCartesien(4, 3, 2));

            // Matrice faisant une rotation de 20° autour de l’axe des x, puis une translation de [4 2 3]
            Matrix RT = Matrix.rotation_x(20).increase_dim() * T;
            Console.WriteLine("Matrice RT de rotation x 20°, puis translation [4 3 2] " + RT);

            // Matrice faisant une translation puis une rotation de 20° autour de l’axe des x
            Matrix TR = T * Matrix.rotation_x(20).increase_dim();
            Console.WriteLine("Matrice TR " + TR);

            // Matrice de projection perspective x = 5
            Matrix PP = Matrix.perspetive_projection(5);
            Console.WriteLine("Matrice de projection perspective " + PP);

            // Utilisez la matrice précédente pour calculer les coordonnées 3D de la projection du point (105, -243, 89) sur le plan x = 5
            VectCartesien v_test = new VectCartesien(105, -243, 89);
            Console.WriteLine("projection perspective de " + v_test + " sur le plan x = 5 : " + v_test.increase_dim() * PP);
            // Appliquer une transformation linéaire sur un vecteur
            Console.WriteLine("vec3 : " + vec3);
            Console.WriteLine("vec3 avec rotation de 45° autour de x: " + vec3 * Rx);

            // Calcul de déterminant
            Matrix m = new Matrix(new VectCartesien(1, -2, 4), new VectCartesien(3, 5, -1), new VectCartesien(2, -6, -3));            double det = m.calculDeterminant();            Console.WriteLine("det de m : " + m + "\n det =  " + det);
            // Comatrice
            Console.WriteLine("comatrice de m: " + m.comatrice());

            // Inversible
            Console.WriteLine("inversibilité de m: " + m.isInversible());

        }
    }
}
