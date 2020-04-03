using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Quaternion
    {
        private double w;
        private double x;
        private double y;
        private double z;

        // Constructeurs
        public Quaternion(double w, double x, double y, double z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Quaternion(double w, VectCartesien v)
        {
            this.w = w;
            this.x = v[0];
            this.y = v[1];
            this.z = v[2];
        }

        // Constructeur identité
        public Quaternion() : this(1.0, 0.0, 0.0, 0.0) { }

        // Constructeur depuis matrice
        public Quaternion(Matrix m)
        {
            if (m.isSpecialOrthogonale() == false)
                throw new Exception("La matrice n'est pas spéciale orthogonale");

            else
            {
                this.w = Math.Sqrt(1 + m[0, 0] + m[1, 1] + m[2, 2]) / 2;
                this.x = (m[2, 1] - m[1, 2]) / (4 * w);
                this.y = (m[0, 2] - m[2, 0]) / (4 * w);
                this.z = (m[1, 0] - m[0, 1]) / (4 * w);
            }
        }

        //ToString
        public override string ToString()
        {
            return "[" + w + " (" + x + " " + y + " " + z + ")]";
        }

        // Getters
        public double getW()
        {
            return w;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public double getZ()
        {
            return z;
        }

        // Copie
        public Quaternion copy()
        {
            return new Quaternion(w, x, y, z);
        }

        // Surcharge d'opérateurs avec un scalaire
        public static Quaternion operator *(double scalaire, Quaternion q)
        {
            return new Quaternion(scalaire * q.w, scalaire * q.x, scalaire * q.y, scalaire * q.z);
        }
        public static Quaternion operator *(Quaternion q, double scalaire)
        {
            return scalaire * q;
        }
        public static Quaternion operator /(Quaternion q, double scalaire)
        {
            return new Quaternion(q.w / scalaire, q.x / scalaire, q.y / scalaire, q.z / scalaire);
        }

        // Opérations spécifiques

        // Mangitude / Norme
        public double magnitude()
        {
            return Math.Sqrt(w * w + x * x + y * y + z * z);
        }

        // Conjugué
        public Quaternion conjugue()
        {
            return new Quaternion(w, -x, -y, -z);
        }

        // Inverse
        public Quaternion inverse()
        {
            return this.conjugue() / this.magnitude();
        }

        // Opérations entre quatérions
        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.w + q2.w, q1.x + q2.x, q1.y + q2.y, q1.z + q2.z);
        }

        // produit de hamilton
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            double w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            double x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            double y = q1.w * q2.y + q1.y * q2.w + q1.z * q2.x - q1.x * q2.z;
            double z = q1.w * q2.z + q1.z * q2.w + q1.x * q2.y - q1.y * q2.x;
            return new Quaternion(w, x, y, z);
        }

        public static Quaternion FromEuler(AngleEuler euler)
        {
            return euler.toQuaterion();
        }

        public static Quaternion FromEuler(double x, double y, double z)
        {
            AngleEuler euler = new AngleEuler(y, x, z);
            return euler.toQuaterion();
        }

        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.w - q2.w, q1.x - q2.x, q1.y - q2.y, q1.z - q2.z);
        }

        // Produit scalaire
        public static double produit_scalaire(Quaternion q1, Quaternion q2)
        {
            return q1.w * q2.w + q1.x * q2.x + q1.y * q2.y + q1.z * q2.z;
        }

        // Logarithme
        public Quaternion ln()
        {
            double new_w = Math.Log(this.magnitude());
            VectCartesien vec = new VectCartesien(x, y, z);
            VectCartesien new_vect = (vec / vec.magnitude()) * Math.Acos(w / this.magnitude());
            return new Quaternion(new_w, new_vect[0], new_vect[1], new_vect[2]);
        }

        // Exponentielle
        public Quaternion exp()
        {
            return this.ln().inverse();
        }

        public Quaternion pow(double p)
        {
            if (p == 0)
                return new Quaternion();
            if (p < 0.0)
            {
                Quaternion t = new Quaternion(Math.Log(-p), Math.PI, 0.0, 0.0);
                return (this * Math.Log(p)).exp();
            }
            else
            {
                return (this * Math.Log(p)).exp();
            }
        }

        // Interpolations
        public static Quaternion LERP(Quaternion q1, Quaternion q2, double beta)
        {
            return (1 - beta) * q1 + beta * q2;
        }

        public static Quaternion SLERP(Quaternion q1, Quaternion q2, double t)
        {
            return (q2 * q1.inverse()).pow(t) * q1; // slow method
            // formule alternative (plus efficace), non complète atm
            /*Quaterion delta = q2 * q1.inverse();
            VectCartesien v0 = new VectCartesien(q1.x, q1.y, q1.z);
            VectCartesien v1 = new VectCartesien(q2.x, q2.y, q2.z);
            double coeff0 = (Math.Sin(1 - t) * delta) / Math.Sin(delta);
            */
        }

        // Conversions
        public AngleEuler toEuler()
        {
            double heading = Math.Atan2(2 * (w * x + y * z), 1 - 2 * (x * x + y * y));
            double pitch = Math.Asin(2 * (w * y - x * z));
            double bank = Math.Atan2(2 * (w * z + x * y), 1 - 2 * (y * y + z * z));
            return new AngleEuler(heading, pitch, bank);
        }

        public Quaternion matrixToQuaternion(Matrix m)
        {
            return new Quaternion(m);
        }

        public VectCartesien getVect()
        {
            return new VectCartesien(x, y, z);
        }
    }
}
