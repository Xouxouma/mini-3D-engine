using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class VectCartesien
    {
        readonly int dim;
        private double[] elems;

        public VectCartesien(params double[] elements)
        {
            dim = elements.Length;
            elems = new double[dim];
            for (int i = 0; i < this.dim; i++)
            {
                this.elems[i] = elements[i];
            }
        }

        // Accéder aux composantes d'un vecteur
        public double this[int i]
        {
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
            for (int i = 0; i < this.dim - 1; i++)
            {
                res += this.elems[i] + ", ";
            }
            res += this.elems[dim - 1];
            res += "]";

            return res;
        }

        // Calculer la négative d'un vecteur
        public static VectCartesien operator -(VectCartesien vec)
        {
            double[] inv_elems = new double[vec.dim];
            for (int i = 0; i < vec.dim; i++)
                inv_elems[i] = -vec.elems[i];
            return new VectCartesien(inv_elems);
        }

        // Calculer la norme d'un vecteur
        public double magnitude()
        {
            double[] squares = new double[dim];
            for (int i = 0; i < dim; i++)
                squares[i] = elems[i] * elems[i];
            return Math.Sqrt(squares.Sum());
        }

        // Addition
        public static VectCartesien operator +(VectCartesien vec1, VectCartesien vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = vec1.elems[i] + vec2.elems[i];
            return new VectCartesien(res_elems);
        }

        // Soustraction
        public static VectCartesien operator -(VectCartesien vec1, VectCartesien vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = vec1.elems[i] - vec2.elems[i];
            return new VectCartesien(res_elems);
        }

        // Distance
        public double distance(VectCartesien vec2)
        {
            if (this.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[this.dim];
            for (int i = 0; i < this.dim; i++)
                res_elems[i] = (this.elems[i] - vec2.elems[i]) * (this.elems[i] - vec2.elems[i]);
            return Math.Sqrt(res_elems.Sum());
        }

        // Produit scalaire
        public static double operator *(VectCartesien vec1, VectCartesien vec2)
        {
            if (vec1.dim != vec2.dim)
                throw new System.ArgumentException("Vectors don't have the same dimension.");

            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * vec2.elems[i]);
            return res_elems.Sum();
        }

        // Produit vectoriel
        public VectCartesien produit_vectoriel(VectCartesien vec2)
        {
            if (this.dim != 3 || vec2.dim != 3)
                throw new System.ArgumentException("Vectors have to be of size 3");

            double[] res_elems = new double[this.dim];
            for (int k = 0; k < 3; k++)
            {
                int i = (k + 1) % 3;
                int j = (k + 2) % 3;
                res_elems[k] = (this.elems[i] * vec2.elems[j]) - (this.elems[j] * vec2.elems[i]);
                //Console.WriteLine("produitvect: " + res_elems[k] + " i : " + i + j + k + " // " + this.elems[i] + vec2.elems[j] + " / " + this.elems[j] + vec2.elems[i]);
            }
            return new VectCartesien(res_elems);
        }

        // Opérations avec un scalaire

        // Multiplication
        public static VectCartesien operator *(VectCartesien vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * n);
            return new VectCartesien(res_elems);
        }
        public static VectCartesien operator *(double n, VectCartesien vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] * n);
            return new VectCartesien(res_elems);
        }

        // Addition
        public static VectCartesien operator +(VectCartesien vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] + n);
            return new VectCartesien(res_elems);
        }
        public static VectCartesien operator +(double n, VectCartesien vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] + n);
            return new VectCartesien(res_elems);
        }

        // Soustraction
        public static VectCartesien operator -(VectCartesien vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] - n);
            return new VectCartesien(res_elems);
        }
        public static VectCartesien operator -(double n, VectCartesien vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] - n);
            return new VectCartesien(res_elems);
        }

        // Division
        public static VectCartesien operator /(VectCartesien vec1, double n)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] / n);
            return new VectCartesien(res_elems);
        }
        public static VectCartesien operator /(double n, VectCartesien vec1)
        {
            double[] res_elems = new double[vec1.dim];
            for (int i = 0; i < vec1.dim; i++)
                res_elems[i] = (vec1.elems[i] / n);
            return new VectCartesien(res_elems);
        }

        // Normaliser un vecteur
        public VectCartesien normalize()
        {
            return this / this.magnitude();
        }

        // Vecteurs utiles
        public static VectCartesien zeros(int dim)
        {
            double[] array = new double[dim];
            for (int i = 0; i < dim; i++)
                array[i] = 0;

            return new VectCartesien(array);
        }

        public VectCartesien increase_dim()
        {
            VectCartesien res = zeros(dim + 1);
            for (int i = 0; i < dim; i++)
                res[i] = this[i];
            return res;
        }

        // TP 2

        // Conversion vartésien -> polaire
        public VectPolaire toPolaire()
        {
            if (this.dim != 2)
                throw new System.ArgumentException("Vector have to be of dim 2");

            double r = Math.Sqrt(this[0] * this[0] + this[1] * this[1]);
            double theta = Math.Atan(this[1] / this[0]);
            return new VectPolaire(r, theta);
        }

        public VectSpherique toSpherique()
        {
            if (this.dim != 3)
                throw new System.ArgumentException("Vector have to be of dim 3");

            double r = Math.Sqrt(this[0] * this[0] + this[1] * this[1] + this[2] * this[2]);
            double p = Math.Asin(-this[1] / r);
            double h = Math.Atan2(this[0], this[2]);
            return new VectSpherique(r, p, h);
        }

        public Color ToArgbColor()
        {
            return Color.FromArgb((int) (elems[0]*255), (int)(elems[1] * 255), (int)(elems[2] * 255), (int)(elems[3] * 255));
        }
    }
}