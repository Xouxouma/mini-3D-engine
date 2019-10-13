using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Maths3D_cs
{
    class Vect
    {
        readonly int dim;
        double[] elems;

        public Vect(params double[] elements) {
            dim = elements.Length;
            elems = new double[dim];
            for (int i = 0; i < this.dim; i++)
            {
                this.elems[i] = elements[i];
            }
        }

        // Accéder aux composantes d'un vecteur
        public double this[int i]{
            get => elems[i];
            set => elems[i] = value;
        }

        public int getDim()
        {
            return this.dim;
        }
        
        // Afficher le contenu d'un vecteur
        public override string ToString()
        {

            String res = "[";
            for (int i = 0; i < this.dim-1; i++)
            {
                res += this.elems[i] + ", ";
            }
            res += this.elems[dim-1];
            res += "]";

            return res;
        }

        // Calculer la négative d'un vecteur
        public static Vect operator -(Vect vec)
        {
            double[] inv_elems = new double[vec.dim];
            for (int i = 0; i < vec.dim; i++)
                inv_elems[i] = -vec.elems[i];
            return new Vect(inv_elems);
        }

        // Calculer la norme d'un vecteur
        public double magnitude()
        {
            double[] squares = new double[dim];
            for (int i = 0; i < dim; i++)
                squares[i] = elems[i];
            return Math.Sqrt(squares.Sum());
        }

        // Addition
        public static Vect operator +(Vect vec1, Vect vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = vec1.elems[i] + vec2.elems[i];
            return new Vect(res_elems);
        }

        // Soustraction
        public static Vect operator -(Vect vec1, Vect vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = vec1.elems[i] - vec2.elems[i];
            return new Vect(res_elems);
        }

        // Distance
        public double distance(Vect vec2)
        {
            if (this.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[this.dim];
            for (int i = 0; i < this.dim; i++)
                res_elems[i] = (this.elems[i] - vec2.elems[i])* (this.elems[i] - vec2.elems[i]);
            return Math.Sqrt(res_elems.Sum());
        }

        // Produit scalaire
        public static double operator *(Vect vec1, Vect vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * vec2.elems[i]);
            return res_elems.Sum();
        }

        // Produit vectoriel
        public Vect produit_vectoriel(Vect vec2)
        {
            if (this.dim != 3 || vec2.dim != 3)
                throw new System.ArgumentException("Vectors have to be of size 3");

            double[] res_elems = new double[this.dim];
            for (int k = 0; k < 3; k++)
            {
                int i = (k+1) % 3;
                int j = (k+2) % 3;
                res_elems[k] = (this.elems[i] * vec2.elems[j]) - (this.elems[j] * vec2.elems[i]);
                Console.WriteLine("produitvect: " + res_elems[k]+" i : "+i+j+k+" // "+ this.elems[i] + vec2.elems[j]+ " / "+ this.elems[j] + vec2.elems[i]);
            }
            return new Vect(res_elems);
        }

        // Opérations avec un scalaire

        // Multiplication
        public static Vect operator *(Vect vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * n);
            return new Vect(res_elems);
        }
        public static Vect operator *(double n, Vect vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * n);
            return new Vect(res_elems);
        }

        // Addition
        public static Vect operator +(Vect vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] + n);
            return new Vect(res_elems);
        }
        public static Vect operator +(double n, Vect vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] + n);
            return new Vect(res_elems);
        }

        // Soustraction
        public static Vect operator -(Vect vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] - n);
            return new Vect(res_elems);
        }
        public static Vect operator -(double n, Vect vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] - n);
            return new Vect(res_elems);
        }

        // Division
        public static Vect operator /(Vect vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] / n);
            return new Vect(res_elems);
        }
        public static Vect operator /(double n, Vect vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] / n);
            return new Vect(res_elems);
        }

    }
}
