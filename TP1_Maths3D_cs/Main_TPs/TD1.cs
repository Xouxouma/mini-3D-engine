using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class TD1
    {
        public static void exec()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("TD1 : \n");

            Console.WriteLine("1.");
            VectCartesien vec1 = new VectCartesien(3, 7);
            VectCartesien vec2 = new VectCartesien(-12, 5);
            VectCartesien vec3 = new VectCartesien(8, -3, 1 / 2);
            VectCartesien vec4 = new VectCartesien(4, -7, 0);
            VectCartesien vec5 = new VectCartesien(4, 5);
            Console.WriteLine(" (a) " + (-vec1));
            Console.WriteLine(" (b) " + vec2.magnitude());
            Console.WriteLine(" (c) " + vec3.magnitude());
            Console.WriteLine(" (d) " + (3 * vec4));
            Console.WriteLine(" (e) " + (vec5 / 2));

            Console.WriteLine("\n2.");
            VectCartesien vec6 = new VectCartesien(12, 5);
            VectCartesien vec7 = new VectCartesien(0, 743.632);
            VectCartesien vec8 = new VectCartesien(-12, 3, -4);
            VectCartesien vec9 = new VectCartesien(1, 1, 1, 1);
            Console.WriteLine(" (a) " + vec6.normalize());
            Console.WriteLine(" (b) " + vec7.normalize());
            Console.WriteLine(" (c) " + vec3.normalize());
            Console.WriteLine(" (d) " + vec8.normalize());
            Console.WriteLine(" (e) " + vec9.normalize());


            Console.WriteLine("\n3.");
            VectCartesien vec10 = new VectCartesien(7, -2, -3);
            VectCartesien vec11 = new VectCartesien(6, 6, -4);
            VectCartesien vec12 = new VectCartesien(2, 9, -1);
            VectCartesien vec13 = new VectCartesien(-2, -9, 1);
            VectCartesien vec14 = new VectCartesien(3, 10, 7);
            VectCartesien vec15 = new VectCartesien(8, -7, 4);
            VectCartesien vec16 = new VectCartesien(4, 5, -11);
            VectCartesien vec17 = new VectCartesien(-4, -5, 11);
            Console.WriteLine(" (a) " + (vec10 + vec11));
            Console.WriteLine(" (b) " + (vec12 + vec13));
            Console.WriteLine(" (c) " + (vec14 - vec15));
            Console.WriteLine(" (d) " + (vec16 - vec17));

            Console.WriteLine("\n4.");
            VectCartesien vec18 = new VectCartesien(10, 6);
            VectCartesien vec19 = new VectCartesien(-14, 30);
            VectCartesien vec20 = new VectCartesien(0, 0);
            VectCartesien vec21 = new VectCartesien(-12, 5);
            VectCartesien vec22 = new VectCartesien(-2, -4, 9);
            VectCartesien vec23 = new VectCartesien(6, -7, 9.5);
            VectCartesien vec24 = new VectCartesien(4, -4, -4, 4);
            VectCartesien vec25 = new VectCartesien(-6, 6, 6, -6);
            Console.WriteLine(" (a) " + (vec18.distance(vec19)));
            Console.WriteLine(" (b) " + (vec20.distance(vec21)));
            Console.WriteLine(" (c) " + (vec14.distance(vec15)));
            Console.WriteLine(" (d) " + (vec22.distance(vec23)));
            Console.WriteLine(" (e) " + (vec24.distance(vec25)));

            Console.WriteLine("\n5.");
            VectCartesien vec26 = new VectCartesien(2, 6);
            VectCartesien vec27 = new VectCartesien(-3, 8);
            VectCartesien vec28 = new VectCartesien(1, 2);
            VectCartesien vec29 = new VectCartesien(11, -4);
            VectCartesien vec30 = new VectCartesien(-5, 1, 3);
            VectCartesien vec31 = new VectCartesien(4, -13, 9);
            VectCartesien vec32 = new VectCartesien(-2, 0, 4);
            VectCartesien vec33 = new VectCartesien(8, -2, 3 / 2);
            VectCartesien vec34 = new VectCartesien(0, 9, 7);
            Console.WriteLine(" (a) " + (vec26 * vec27));
            Console.WriteLine(" (b) " + (-7 * vec28 * vec29));
            Console.WriteLine(" (c) " + (10 + vec30 * vec31));
            Console.WriteLine(" (d) " + (3 * vec32 * (vec33 + vec34)));

            Console.WriteLine("\n6.");
            VectCartesien vec35 = new VectCartesien(0, -1, 0);
            VectCartesien vec36 = new VectCartesien(0, 0, 1);
            VectCartesien vec37 = new VectCartesien(-2, 4, 1);
            VectCartesien vec38 = new VectCartesien(1, -2, -1);
            VectCartesien vec39 = new VectCartesien(3, 10, 7);
            VectCartesien vec40 = new VectCartesien(8, -7, 4);
            Console.WriteLine(" (a) a*b = " + vec35.produit_vectoriel(vec36));
            Console.WriteLine("     b*a = " + vec35.produit_vectoriel(vec36));
            Console.WriteLine(" (b) a*b = " + vec37.produit_vectoriel(vec38));
            Console.WriteLine("     b*a = " + vec38.produit_vectoriel(vec37));
            Console.WriteLine(" (c) a*b = " + vec39.produit_vectoriel(vec40));
            Console.WriteLine("     b*a = " + vec40.produit_vectoriel(vec39));

            VectCartesien vec41 = new VectCartesien(1, 2);
            VectCartesien vec42 = new VectCartesien(-6, 3);
            Console.WriteLine("\n7. " + (vec41 * vec42));

            Console.WriteLine("\n8.");
            Matrix m1 = new Matrix(new VectCartesien(1, -2), new VectCartesien(5, 0));
            Matrix m2 = new Matrix(new VectCartesien(-3, 7), new VectCartesien(4, 1 / 3));
            Matrix m3 = new Matrix(new VectCartesien(6, -7), new VectCartesien(-4, 5));
            Matrix m4 = new Matrix(new VectCartesien(3, 3));
            Matrix m5 = new Matrix(new VectCartesien(3, -1, 4));
            Matrix m6 = new Matrix(new VectCartesien(-2, 0, 3), new VectCartesien(5, 7, -6), new VectCartesien(1, -4, 2));
            Matrix m7 = new Matrix(new VectCartesien(7, -2, 7, 3));
            Matrix m8 = new Matrix(new VectCartesien(-5), new VectCartesien(1));
            Matrix m9 = new Matrix(new VectCartesien(3, 3));
            Matrix m10 = new Matrix(new VectCartesien(6, -7), new VectCartesien(-4, 5));
            try
            {
                Console.WriteLine(" (a) " + m1 * m2);
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (a) Impossible");
            }

            try
            {
                Console.WriteLine(" (b) " + m3 * m4);
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (b) Impossible");
            }

            try
            {
                Console.WriteLine(" (c) " + m5 * m6);
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (c) Impossible");
            }

            try
            {
                Console.WriteLine(" (e) " + m7 * m8);
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (e) Impossible");
            }

            try
            {
                Console.WriteLine(" (g) " + m9 * m10);
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (g) Impossible");
            }

            Console.WriteLine("\n9.");
            Matrix m11_ligne = new Matrix(new VectCartesien(5, -1, 2));
            Matrix m11_colonne = m11_ligne.transposee();
            Matrix m12 = new Matrix(new VectCartesien(1, 0, 0), new VectCartesien(0, 1, 0), new VectCartesien(0, 0, 1));
            Matrix m13 = new Matrix(new VectCartesien(1, 7, 2), new VectCartesien(7, 0, -3), new VectCartesien(2, -3, -1));
            Matrix m14 = new Matrix(new VectCartesien(2, 5, -3), new VectCartesien(1, 7, 1), new VectCartesien(-2, -1, 4));
            Matrix m15 = new Matrix(new VectCartesien(0, -4, 3), new VectCartesien(4, 0, -1), new VectCartesien(-3, 1, 0));

            try
            {
                Console.WriteLine(" (a) Ligne : " + (m11_ligne * m12));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (a) Ligne : Impossible");
            }

            try
            {
                Console.WriteLine(" (b) Colonne : " + (m11_colonne * m12));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (a) Colonne : Impossible");
            }

            try
            {
                Console.WriteLine(" (b) Ligne : " + (m11_ligne * m13));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (b) Ligne : Impossible");
            }

            try
            {
                Console.WriteLine(" (b) Colonne : " + (m11_colonne * m13));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (b) Colonne : Impossible");
            }

            try
            {
                Console.WriteLine(" (c) Ligne : " + (m11_ligne * m14));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (c) Ligne : Impossible");
            }

            try
            {
                Console.WriteLine(" (c) Colonne : " + (m11_colonne * m14));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (c) Colonne : Impossible");
            }

            try
            {
                Console.WriteLine(" (d) Ligne : " + (m11_ligne * m15));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (d) Ligne : Impossible");
            }

            try
            {
                Console.WriteLine(" (d) Colonne : " + (m11_colonne * m15));
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine(" (d) Colonne : Impossible");
            }

            Console.WriteLine("\n10. " + Matrix.rotation_x(-22.0));

            Console.WriteLine("\n11. " + Matrix.rotation_y(30.0));

            Console.WriteLine("\n12. " + Matrix.rotation(-15.0, 0.267, -0.535, 0.802));

            Console.WriteLine("\n13. " + Matrix.scale(2,1,1,1));

            Console.WriteLine("\n14. " + Matrix.orthographic_projection(new VectCartesien(0.267, -0.535, 0.802)));

            Matrix m16 = new Matrix(new VectCartesien(3, -2), new VectCartesien(1, 4));
            Console.WriteLine("\n15. " + m16.calculDeterminant());

            Matrix m17 = new Matrix(new VectCartesien(3, -2, 0), new VectCartesien(1, 4, 0), new VectCartesien(0, 0, 2));
            Console.WriteLine("\n16. Déterminant = " + m17.calculDeterminant());
            Console.WriteLine("      Comatrice = " + m17.comatrice());
            Console.WriteLine("      Inverse = " + m17.inverse());

            Matrix m18 = new Matrix(new VectCartesien(-0.1495, 0.1986, -0.9685), new VectCartesien(-0.8256, 0.5640, 0.0117), new VectCartesien(-0.5439, -0.8015, 0.2484));
            Console.WriteLine("\n17. Inverse = " + m18.inverse());
            if (m18.isOrthogonale())
                Console.WriteLine("La matrice est orthogonale");
            else
                Console.WriteLine("La matrice n'est pas orthogonale");

            Matrix m19 = new Matrix(new VectCartesien(-0.1495, -0.1986, -0.9685, 0), new VectCartesien(-0.8256, 0.5640, 0.0117, 0), new VectCartesien(-0.5439, -0.8015, 0.2484, 0), new VectCartesien(1.7928, -5.3116, 8.0151, 1));
            Console.WriteLine("\n18. " + m19.inverse());

            VectCartesien v43 = new VectCartesien(4, 2, 3);
            Matrix v43_translation = Matrix.translation(v43);
            Console.WriteLine("\n19. " + v43_translation);

            Matrix rotation = Matrix.rotation_x(20).increase_dim();
            Console.WriteLine("\n20. " + (rotation * v43_translation));

            Console.WriteLine("\n21. " + (v43_translation * rotation));

            //Console.WriteLine("\n22. " + Matrix.perspetive_projection)
        }
    }
}
