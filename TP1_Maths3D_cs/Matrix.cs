using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Maths3D_cs
{
    class Matrix
    {
        readonly int nb_row; // number of vects
        readonly int nb_col; // size of vects
        double[,] elems; // matrix[i][j] get the element of the i-th row, j-th col

        public Matrix(params Vect[] vects_arg)
        {
            nb_row = vects_arg.Length;
            nb_col = vects_arg[0].getDim();

            this.elems = new double[nb_row, nb_col];

            for (int i = 0; i < vects_arg.Length; i++)
            {
                Vect vec = vects_arg[i];
                if (vec.getDim() != nb_col)
                    throw new System.ArgumentException("Vectors don't have the same dimension.");

                for (int j = 0; j < vec.getDim(); j++)
                    this.elems[i,j] = vec[j];
            }
        }

        public Matrix(int nb_row, int nb_col, int val=0)
        {
            this.nb_row = nb_row;
            this.nb_col = nb_col;

            this.elems = new double[nb_row, nb_col];

            // Initialize at 0
            for (int i = 0; i < nb_row; i++)
                for (int j = 0; j < nb_col; j++)
                    if (i == j)
                        this[i, j] = val;
                    else
                        this[i, j] = 0;
        }

        // Access to matrix components
        public double this[int i, int j]
        {
            get => elems[i,j];
            set => elems[i,j] = value;
        }

        // Access to row
        public Vect getRow(int k)
        {
            double[] res_row = new double[nb_col];
            for (int i = 0; i < nb_col; i++)
                res_row[i] = this[k,i];
            return new Vect(res_row);
        }
        public Vect getCol(int k)
        {
            double[] res_row = new double[nb_row];
            for (int i = 0; i < nb_row; i++)
                res_row[i] = this[i,k];
            return new Vect(res_row);
        }

        // Display matrix
        public override string ToString()
        {
            String res = "Matrix:\n";
            for (int i = 0; i < this.nb_row; i++)
                res += this.getRow(i).ToString() + "\n";
            return res;
        }

        // Operations intra-matrix

        // Addition
        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_row || mat1.nb_col != mat2.nb_col)
                throw new System.ArgumentException("both matrix don't have the same dimension.");

            Matrix res_mat = new Matrix(mat1.nb_row, mat1.nb_col);
            for (int i = 0; i < mat1.nb_row; i++)
                for (int j = 0; j < mat1.nb_col; j++)
                    res_mat[i, j] = mat1[i, j] + mat2[i, j];
            return res_mat;
        }

        // Substraction
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_row || mat1.nb_col != mat2.nb_col)
                throw new System.ArgumentException("both matrix don't have the same dimension.");

            Matrix res_mat = new Matrix(mat1.nb_row, mat1.nb_col);
            for (int i = 0; i < mat1.nb_row; i++)
                for (int j = 0; j < mat1.nb_col; j++)
                    res_mat[i,j] = mat1[i,j] - mat2[i,j];
            return res_mat;
        }

        // Product
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_col)
                throw new System.ArgumentException("both matrix don't have the right dimension: mat1 must have exactly as much rows than mat2 has columns");

            Matrix res_mat = new Matrix(mat1.nb_row, mat2.nb_col);
            for (int i = 0; i < res_mat.nb_row; i++)
                for (int j = 0; j < res_mat.nb_col; j++)
                    for (int k = 0; k < mat1.nb_col; k++)
                        res_mat[i,j] += mat1[i,k] * mat2[k,j];
            return res_mat;
        }


    }
}
