using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Maths3D_cs
{
    class Matrix
    {
        // a vect is a row of a matrix : 
        // matrix[i][j] get the element of the i-th row, j-th col
        readonly int nb_row; // number of vects
        readonly int nb_col; // size of vects
        Vect[] vecs;

        public Matrix(params Vect[] vects_arg)
        {
            nb_row = vects_arg.Length;
            nb_col = vects_arg[0].getDim();

            this.vecs = new Vect[nb_row];

            for (int i = 0; i < vects_arg.Length; i++)
            {
                Vect vec = vects_arg[i];
                if (vec.getDim() != nb_col)
                    throw new System.ArgumentException("Vectors don't have the same dimension.");
                
                this.vecs[i] = vec;
            }
        }

        // Access to matrix components
        public Vect this[int i]
        {
            get => vecs[i];
            set => vecs[i] = value;
        }

        // Display matrix
        public override string ToString()
        {
            String res = "Matrix:\n";
            for (int i = 0; i < this.nb_row; i++)
                res += this.vecs[i].ToString() + "\n";
            return res;
        }

        // Operations intra-matrix

        // Addition
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_row || mat1.nb_col != mat2.nb_col)
                throw new System.ArgumentException("mattors don't have the same dimension.");

            Vect[] res_vecs = new Vect[mat1.nb_row];
            for (int i = 0; i < mat1.nb_row; i++)
                res_vecs[i] = mat1[i] + mat2[i];
            return new Matrix(res_vecs);
        }

        // Substraction
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_row || mat1.nb_col != mat2.nb_col)
                throw new System.ArgumentException("mattors don't have the same dimension.");

            Vect[] res_vecs = new Vect[mat1.nb_row];
            for (int i = 0; i < mat1.nb_row; i++)
                res_vecs[i] = mat1[i] - mat2[i];
            return new Matrix(res_vecs);
        }

        // Product
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_col || mat1.nb_col != mat2.nb_row)
                throw new System.ArgumentException("mattors don't have the same dimension.");

            Vect[] res_vecs = new Vect[mat1.nb_row];
            for (int i = 0; i < mat1.nb_row; i++)
                for (int j = 0; j < mat1.nb_col; j++)
                {
                    res_vecs[i][j] = 0;
                    for (int k = 0; k < mat1.nb_row; k++)
                        res_vecs[i][j] += mat1[i][k] * mat2[k][j];
                }
            return new Matrix(res_vecs);
        }


    }
}
