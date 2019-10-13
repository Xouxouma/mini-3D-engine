using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Maths3D_cs
{
    class Matrix
    {
        int nb_row; // number of vects
        int nb_col; // size of vects
        double[,] elems; // matrix[i][j] get the element of the i-th row, j-th col

        // Constructors

        // Constructor from vects
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

        // Constructor from dimensions
        public Matrix(int nb_row, int nb_col, double val=0)
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

        // Constructor from double array2D
        public Matrix(double[,] array)
        {
            this.nb_row = array.GetLength(0);
            this.nb_col = array.GetLength(1);
            this.elems = new double[nb_row, nb_col];
            for (int i = 0; i < nb_row; i++)
                for (int j = 0; j < nb_col; j++)
                    this[i, j] = array[i, j];
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
            {
                res_row[i] = this[k,i];
            }
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

        // copy
        public Matrix copy()
        {
            Matrix res_mat = new Matrix(nb_row, nb_col);
            for (int i = 0; i < nb_row; i++)
                for (int j = 0; j < nb_col; j++)
                    res_mat[i, j] = this[i, j];
            return res_mat;
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

        // Matrix Product
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            if (mat1.nb_row != mat2.nb_col)
                throw new System.ArgumentException("both matrix don't have the right dimension: mat1 must have exactly as much rows than mat2 has columns");

            Matrix res_mat = new Matrix(mat1.nb_row, mat2.nb_col);
            for (int i = 0; i < res_mat.nb_row; i++)
                for (int j = 0; j < res_mat.nb_col; j++)
                    for (int k = 0; k < mat1.nb_col; k++)
                    {
                        res_mat[i,j] += mat1[i,k] * mat2[k,j];
                    }
            return res_mat;
        }

        // product with R
        public static Matrix operator *(double scalaire, Matrix mat)
        {
            Matrix res_mat = mat.copy();
            for (int i = 0; i < res_mat.nb_row; i++)
                for (int j = 0; j < res_mat.nb_col; j++)
                    res_mat[i, j] *= scalaire;
            return res_mat;
        }

        public static Matrix operator *(Matrix mat, double scalaire)
        {
            return scalaire * mat;
        }

        // transposition
        public Matrix transposer(Boolean keep_in_same_matrix=true)
        {
            int tmp;
            double[,] elems_bis;

            elems_bis = new double[nb_col, nb_row];

            for (int i = 0; i < nb_row; i++)
                for (int j = 0; j < nb_col; j++)
                    elems_bis[j,i] = elems[i,j];

            if (keep_in_same_matrix)
            {
                tmp = nb_col;
                nb_col = nb_row;
                nb_row = tmp;
                elems = elems_bis;
                return this;
            }
            else
                return new Matrix(elems_bis);
        }

        public static Matrix operator *(Vect vec, Matrix mat)
        {
            if (vec.getDim() != mat.nb_col)
                throw new System.ArgumentException("matrix don't have the right dimension: mat colmns must equals vec dim");

            Matrix res_mat = new Matrix(1,vec.getDim());
            for (int i = 0; i < vec.getDim(); i++)
            {
                res_mat[0,i] = vec * mat.getCol(i);
            }
            return res_mat;
        }

        public static Matrix operator *(Matrix mat, Vect vec)
        {
            if (vec.getDim() != mat.nb_col)
                throw new System.ArgumentException("matrix don't have the right dimension: mat colmns must equals vec dim");

            Matrix res_mat = new Matrix(1, vec.getDim());
            for (int i = 0; i < vec.getDim(); i++)
            {
                res_mat[0, i] = mat.getRow(i) * vec;
            }
            return res_mat;
        }

        // Definition of rotation matrix
        public static Matrix rotation(double angle, int axe)
        /// create a rotation matrix 3*3 around the selected axe:
        /// 0 for x, 1 for y, 2 for z
        {
            double[,] double_array2D = new double[3, 3];
            
            Matrix res_mat = new Matrix(3,3,0);

            res_mat[axe, axe] = 1;

            res_mat[(axe+1)%3, (axe+1)%3] = Math.Cos(angle);
            res_mat[(axe+1)%3, (axe+2)%3] = Math.Sin(angle);
            res_mat[(axe+2)%3, (axe+1)%3] = -Math.Sin(angle);
            res_mat[(axe+2)%3, (axe+2)%3] = Math.Cos(angle);

            return res_mat;
        }

        public static Matrix rotation_x(double angle)
        {
            return Matrix.rotation(angle, 0);
        }
        public static Matrix rotation_y(double angle)
        {
            return Matrix.rotation(angle, 1);
        }
        public static Matrix rotation_z(double angle)
        {
            return Matrix.rotation(angle, 2);
        }

        // Useful matrix
        public static Matrix vectShadingMatrix(Vect v)
        {
            Vect n = v.normalize();
            Matrix res_mat = new Matrix(n.getDim(), n.getDim());
            for (int i = 0; i < res_mat.nb_row; i++)
                for (int j = 0; j < res_mat.nb_col; j++)
                    res_mat[i, j] += n[i] * n[j];
            return res_mat;
        }

        public static Matrix I(int size)
        {
            return new Matrix(size, size, 1);
        }

        // Rotation around an arbitrary axis
        public static Matrix rotation(double angle, int x, int y, int z)
        {
            Vect n = new Vect(x, y, z).normalize();
            Matrix res_mat = ( Matrix.rotation(angle, 0) + Matrix.rotation(angle, 0) + Matrix.rotation(angle, 0) ) * (new Matrix(3, 3, 0.5));
            for (int i = 0; i < res_mat.nb_row; i++)
                for (int j = 0; j < res_mat.nb_col; j++)
                    res_mat[i,j] += n[i] * n[j] * (1 - Math.Cos(angle));
            return res_mat;
        }

        // Scale Matrix

        // Ordinal Scale
        public static Matrix ordinal_scale(Vect k)
        {
            Matrix res_mat = new Matrix(3, 3);
            for (int i = 0; i < 3; i++)
                res_mat[i, i] = k[i];
            return res_mat;
        }
        public static Matrix ordinal_scale(double kx, double ky, double kz)
        {
            return ordinal_scale(new Vect(kx, ky, kz));
        }

        // Arbitrary scale
        public static Matrix scale(double k, Vect direction)
        {
            return (k-1) * vectShadingMatrix(direction) + I(3);
        }
        public static Matrix scale(double k, double x, double y, double z)
        {
            return scale(k, new Vect(x, y, z));
        }

        // Projection Matrix
        public static Matrix orthographic_projection(int axis)
        {
            Matrix res_mat = I(3);
            res_mat[axis,axis] = 0;
            return res_mat;
        }
        public static Matrix orthographic_projection(Vect v)
        {
            return scale(0, v);
        }

        // Reflection matrix
        public static Matrix reflect(int axis)
        {
            Vect n = Vect.zeros(3);
            n[axis] = 1;
            return scale(-1, n);
        }
        public static Matrix reflect(Vect v)
        {
            return scale(-1, v);
        }

        // Shearing matrix
        public static Matrix shearing_xy(double s, double t)
        {
            Matrix res_mat = I(3);
            res_mat[2, 0] = s;
            res_mat[2, 1] = t;
            return res_mat;
        }
        public static Matrix shearing_yz(double s, double t)
        {
            Matrix res_mat = I(3);
            res_mat[0, 1] = s;
            res_mat[0, 2] = t;
            return res_mat;
        }
        public static Matrix shearing_xz(double s, double t)
        {
            Matrix res_mat = I(3);
            res_mat[1, 0] = s;
            res_mat[1, 2] = t;
            return res_mat;
        }

        // Matrix 3*3 to 4*4
        public Matrix increase_dim()
        {
            if (nb_row != nb_col)
                throw new System.ArgumentException("Matrix must be a square matrix");

            Matrix res_mat = new Matrix(nb_row+1, nb_col+1);

            for (int i = 0; i < nb_row; i++)
                for (int j = 0; j < nb_col; j++)
                    res_mat[i,j] = this[i,j];
            res_mat[nb_row, nb_col] = 1;

            return res_mat;
        }


    }
}
