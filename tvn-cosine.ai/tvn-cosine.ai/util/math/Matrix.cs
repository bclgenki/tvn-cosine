﻿using System.Text;
using tvn.cosine.ai.common;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.exceptions;

namespace tvn.cosine.ai.util.math
{
    /**
     * Jama = Java Matrix class.
     * <P>
     * The Java Matrix Class provides the fundamental operations of numerical linear
     * algebra. Various constructors create Matrices from two dimensional arrays of
     * double precision floating point numbers. Various "gets" and "sets" provide
     * access to submatrices and matrix elements. Several methods implement basic
     * matrix arithmetic, including matrix addition and multiplication, matrix
     * norms, and element-by-element array operations. Methods for reading and
     * printing matrices are also included. All the operations in this version of
     * the Matrix Class involve real matrices. Complex matrices may be handled in a
     * future version.
     * <P>
     * Five fundamental matrix decompositions, which consist of pairs or triples of
     * matrices, permutation vectors, and the like, produce results in five
     * decomposition classes. These decompositions are accessed by the Matrix class
     * to compute solutions of simultaneous linear equations, determinants, inverses
     * and other matrix functions. The five decompositions are:
     * <P>
     * <UL>
     * <LI>Cholesky Decomposition of symmetric, positive definite matrices.
     * <LI>LU Decomposition of rectangular matrices.
     * <LI>QR Decomposition of rectangular matrices.
     * <LI>Singular Value Decomposition of rectangular matrices.
     * <LI>Eigenvalue Decomposition of both symmetric and nonsymmetric square
     * matrices.
     * </UL>
     * <DL>
     * <DT><B>Example of use:</B></DT>
     * <P>
     * <DD>Solve a linear system A x = b and compute the residual norm, ||b - A x||.
     * <P>
     * 
     * <PRE>
     * double[][] vals = { { 1., 2., 3 }, { 4., 5., 6. }, { 7., 8., 10. } };
     * Matrix A = new Matrix(vals);
     * Matrix b = Matrix.random(3, 1);
     * Matrix x = A.solve(b);
     * Matrix r = A.times(x).minus(b);
     * double rnorm = r.normInf();
     * </PRE>
     * 
     * </DD>
     * </DL>
     * 
     * @author The MathWorks, Inc. and the National Institute of Standards and
     *         Technology.
     * @version 5 August 1998
     */
    public class Matrix : ICloneable<object>
    {
        /*
         * ------------------------ Class variables ------------------------
         */

        /**
         * Array for internal storage of elements.
         * 
         * @serial internal array storage.
         */
        private readonly double[,] A;

        /**
         * Row and column dimensions.
         * 
         * @serial row dimension.
         * @serial column dimension.
         */
        private readonly int m;
        private readonly int n;

        /*
         * ------------------------ Constructors ------------------------
         */

        /** Construct a diagonal Matrix from the given List of doubles */
        public static Matrix createDiagonalMatrix(IQueue<double> values)
        {
            Matrix m = new Matrix(values.Size(), values.Size(), 0);
            for (int i = 0; i < values.Size(); i++)
            {
                m.set(i, i, values.Get(i));
            }
            return m;
        }

        /**
         * Construct an m-by-n matrix of zeros.
         * 
         * @param m
         *            Number of rows.
         * @param n
         *            Number of colums.
         */

        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            A = new double[m, n];
        }

        /**
         * Construct an m-by-n constant matrix.
         * 
         * @param m
         *            Number of rows.
         * @param n
         *            Number of colums.
         * @param s
         *            Fill the matrix with this scalar value.
         */

        public Matrix(int m, int n, double s)
        {
            this.m = m;
            this.n = n;
            A = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = s;
                }
            }
        }

        /**
         * Construct a matrix from a 2-D array.
         * 
         * @param A
         *            Two-dimensional array of doubles.
         * @exception IllegalArgumentException
         *                All rows must have the same length
         * @see #constructWithCopy
         */

        public Matrix(double[,] A)
        {
            m = A.GetLength(0);
            n = A.GetLength(1);
            this.A = A;
        }

        /**
         * Construct a matrix quickly without checking arguments.
         * 
         * @param A
         *            Two-dimensional array of doubles.
         * @param m
         *            Number of rows.
         * @param n
         *            Number of colums.
         */

        public Matrix(double[,] A, int m, int n)
        {
            this.A = A;
            this.m = m;
            this.n = n;
        }

        /**
         * Construct a matrix from a one-dimensional packed array
         * 
         * @param vals
         *            One-dimensional array of doubles, packed by columns (ala
         *            Fortran).
         * @param m
         *            Number of rows.
         * @exception IllegalArgumentException
         *                Array length must be a multiple of m.
         */

        public Matrix(double[] vals, int m)
        {
            this.m = m;
            n = (m != 0 ? vals.Length / m : 0);
            if (m * n != vals.Length)
            {
                throw new IllegalArgumentException("Array length must be a multiple of m.");
            }
            A = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = vals[i + j * m];
                }
            }
        }

        /*
         * ------------------------ Public Methods ------------------------
         */

        /**
         * Construct a matrix from a copy of a 2-D array.
         * 
         * @param A
         *            Two-dimensional array of doubles.
         * @exception IllegalArgumentException
         *                All rows must have the same length
         */

        public static Matrix constructWithCopy(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j];
                }
            }
            return X;
        }

        /**
         * Make a deep copy of a matrix
         */

        public Matrix copy()
        {
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j];
                }
            }
            return X;
        }

        /**
         * Clone the Matrix object.
         */


        public object clone()
        {
            return this.copy();
        }

        /**
         * Access the internal two-dimensional array.
         * 
         * @return Pointer to the two-dimensional array of matrix elements.
         */

        public double[,] getArray()
        {
            return A;
        }

        /**
         * Copy the internal two-dimensional array.
         * 
         * @return Two-dimensional array copy of matrix elements.
         */

        public double[,] getArrayCopy()
        {
            double[,] C = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j];
                }
            }
            return C;
        }

        /**
         * Make a one-dimensional column packed copy of the internal array.
         * 
         * @return Matrix elements packed in a one-dimensional array by columns.
         */

        public double[] getColumnPackedCopy()
        {
            double[] vals = new double[m * n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    vals[i + j * m] = A[i, j];
                }
            }
            return vals;
        }

        /**
         * Make a one-dimensional row packed copy of the internal array.
         * 
         * @return Matrix elements packed in a one-dimensional array by rows.
         */

        public double[] getRowPackedCopy()
        {
            double[] vals = new double[m * n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    vals[i * n + j] = A[i, j];
                }
            }
            return vals;
        }

        /**
         * Get row dimension.
         * 
         * @return m, the number of rows.
         */

        public int getRowDimension()
        {
            return m;
        }

        /**
         * Get column dimension.
         * 
         * @return n, the number of columns.
         */

        public int getColumnDimension()
        {
            return n;
        }

        /**
         * Get a single element.
         * 
         * @param i
         *            Row index.
         * @param j
         *            Column index.
         * @return A(i,j)
         * @exception ArrayIndexOutOfBoundsException
         */

        public double get(int i, int j)
        {
            return A[i, j];
        }

        /**
         * Get a submatrix.
         * 
         * @param i0
         *            Initial row index
         * @param i1
         *            Final row index
         * @param j0
         *            Initial column index
         * @param j1
         *            Final column index
         * @return A(i0:i1,j0:j1)
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public Matrix getMatrix(int i0, int i1, int j0, int j1)
        {
            Matrix X = new Matrix(i1 - i0 + 1, j1 - j0 + 1);
            double[,] B = X.getArray();
            try
            {
                for (int i = i0; i <= i1; i++)
                {
                    for (int j = j0; j <= j1; j++)
                    {
                        B[i - i0, j - j0] = A[i, j];
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
            return X;
        }

        /**
         * Get a submatrix.
         * 
         * @param r
         *            Array of row indices.
         * @param c
         *            Array of column indices.
         * @return A(r(:),c(:))
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public Matrix getMatrix(int[] r, int[] c)
        {
            Matrix X = new Matrix(r.Length, c.Length);
            double[,] B = X.getArray();
            try
            {
                for (int i = 0; i < r.Length; i++)
                {
                    for (int j = 0; j < c.Length; j++)
                    {
                        B[i, j] = A[r[i], c[j]];
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
            return X;
        }

        /**
         * Get a submatrix.
         * 
         * @param i0
         *            Initial row index
         * @param i1
         *            Final row index
         * @param c
         *            Array of column indices.
         * @return A(i0:i1,c(:))
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public Matrix getMatrix(int i0, int i1, int[] c)
        {
            Matrix X = new Matrix(i1 - i0 + 1, c.Length);
            double[,] B = X.getArray();
            try
            {
                for (int i = i0; i <= i1; i++)
                {
                    for (int j = 0; j < c.Length; j++)
                    {
                        B[i - i0, j] = A[i, c[j]];
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
            return X;
        }

        /**
         * Get a submatrix.
         * 
         * @param r
         *            Array of row indices.
         * @param j0
         *            Initial column index
         * @param j1
         *            Final column index
         * @return A(r(:),j0:j1)
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public Matrix getMatrix(int[] r, int j0, int j1)
        {
            Matrix X = new Matrix(r.Length, j1 - j0 + 1);
            double[,] B = X.getArray();
            try
            {
                for (int i = 0; i < r.Length; i++)
                {
                    for (int j = j0; j <= j1; j++)
                    {
                        B[i, j - j0] = A[r[i], j];
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
            return X;
        }

        /**
         * Set a single element.
         * 
         * @param i
         *            Row index.
         * @param j
         *            Column index.
         * @param s
         *            A(i,j).
         * @exception ArrayIndexOutOfBoundsException
         */

        public void set(int i, int j, double s)
        {
            A[i, j] = s;
        }

        /**
         * Set a submatrix.
         * 
         * @param i0
         *            Initial row index
         * @param i1
         *            Final row index
         * @param j0
         *            Initial column index
         * @param j1
         *            Final column index
         * @param X
         *            A(i0:i1,j0:j1)
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public void setMatrix(int i0, int i1, int j0, int j1, Matrix X)
        {
            try
            {
                for (int i = i0; i <= i1; i++)
                {
                    for (int j = j0; j <= j1; j++)
                    {
                        A[i, j] = X.get(i - i0, j - j0);
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
        }

        /**
         * Set a submatrix.
         * 
         * @param r
         *            Array of row indices.
         * @param c
         *            Array of column indices.
         * @param X
         *            A(r(:),c(:))
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public void setMatrix(int[] r, int[] c, Matrix X)
        {
            try
            {
                for (int i = 0; i < r.Length; i++)
                {
                    for (int j = 0; j < c.Length; j++)
                    {
                        A[r[i], c[j]] = X.get(i, j);
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
        }

        /**
         * Set a submatrix.
         * 
         * @param r
         *            Array of row indices.
         * @param j0
         *            Initial column index
         * @param j1
         *            Final column index
         * @param X
         *            A(r(:),j0:j1)
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public void setMatrix(int[] r, int j0, int j1, Matrix X)
        {
            try
            {
                for (int i = 0; i < r.Length; i++)
                {
                    for (int j = j0; j <= j1; j++)
                    {
                        A[r[i], j] = X.get(i, j - j0);
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
        }

        /**
         * Set a submatrix.
         * 
         * @param i0
         *            Initial row index
         * @param i1
         *            Final row index
         * @param c
         *            Array of column indices.
         * @param X
         *            A(i0:i1,c(:))
         * @exception ArrayIndexOutOfBoundsException
         *                Submatrix indices
         */

        public void setMatrix(int i0, int i1, int[] c, Matrix X)
        {
            try
            {
                for (int i = i0; i <= i1; i++)
                {
                    for (int j = 0; j < c.Length; j++)
                    {
                        A[i, c[j]] = X.get(i - i0, j);
                    }
                }
            }
            catch (ArrayIndexOutOfBoundsException)
            {
                throw new ArrayIndexOutOfBoundsException("Submatrix indices");
            }
        }

        /**
         * Matrix transpose.
         * 
         * @return A'
         */

        public Matrix transpose()
        {
            Matrix X = new Matrix(n, m);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[j, i] = A[i, j];
                }
            }
            return X;
        }

        /**
         * One norm
         * 
         * @return maximum column sum.
         */

        public double norm1()
        {
            double f = 0;
            for (int j = 0; j < n; j++)
            {
                double s = 0;
                for (int i = 0; i < m; i++)
                {
                    s += System.Math.Abs(A[i, j]);
                }
                f = System.Math.Max(f, s);
            }
            return f;
        }

        /**
         * Two norm
         * 
         * @return maximum singular value.
         */

        // public double norm2 () {
        // return (new SingularValueDecomposition(this).norm2());
        // }
        /**
         * Infinity norm
         * 
         * @return maximum row sum.
         */

        public double normInf()
        {
            double f = 0;
            for (int i = 0; i < m; i++)
            {
                double s = 0;
                for (int j = 0; j < n; j++)
                {
                    s += System.Math.Abs(A[i, j]);
                }
                f = System.Math.Max(f, s);
            }
            return f;
        }

        /**
         * Frobenius norm
         * 
         * @return sqrt of sum of squares of all elements.
         */

        // public double normF () {
        // double f = 0;
        // for (int i = 0; i < m; i++) {
        // for (int j = 0; j < n; j++) {
        // f = Maths.hypot(f,A[i][j]);
        // }
        // }
        // return f;
        // }
        /**
         * Unary minus
         * 
         * @return -A
         */

        public Matrix uminus()
        {
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = -A[i, j];
                }
            }
            return X;
        }

        /**
         * C = A + B
         * 
         * @param B
         *            another matrix
         * @return A + B
         */

        public Matrix plus(Matrix B)
        {
            checkMatrixDimensions(B);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] + B.A[i, j];
                }
            }
            return X;
        }

        /**
         * A = A + B
         * 
         * @param B
         *            another matrix
         * @return A + B
         */

        public Matrix plusEquals(Matrix B)
        {
            checkMatrixDimensions(B);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = A[i, j] + B.A[i, j];
                }
            }
            return this;
        }

        /**
         * C = A - B
         * 
         * @param B
         *            another matrix
         * @return A - B
         */

        public Matrix minus(Matrix B)
        {
            checkMatrixDimensions(B);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] - B.A[i, j];
                }
            }
            return X;
        }

        /**
         * A = A - B
         * 
         * @param B
         *            another matrix
         * @return A - B
         */

        public Matrix minusEquals(Matrix B)
        {
            checkMatrixDimensions(B);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = A[i, j] - B.A[i, j];
                }
            }
            return this;
        }

        /**
         * Element-by-element multiplication, C = A.*B
         * 
         * @param B
         *            another matrix
         * @return A.*B
         */

        public Matrix arrayTimes(Matrix B)
        {
            checkMatrixDimensions(B);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] * B.A[i, j];
                }
            }
            return X;
        }

        /**
         * Element-by-element multiplication in place, A = A.*B
         * 
         * @param B
         *            another matrix
         * @return A.*B
         */

        public Matrix arrayTimesEquals(Matrix B)
        {
            checkMatrixDimensions(B);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = A[i, j] * B.A[i, j];
                }
            }
            return this;
        }

        /**
         * Element-by-element right division, C = A./B
         * 
         * @param B
         *            another matrix
         * @return A./B
         */

        public Matrix arrayRightDivide(Matrix B)
        {
            checkMatrixDimensions(B);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = A[i, j] / B.A[i, j];
                }
            }
            return X;
        }

        /**
         * Element-by-element right division in place, A = A./B
         * 
         * @param B
         *            another matrix
         * @return A./B
         */

        public Matrix arrayRightDivideEquals(Matrix B)
        {
            checkMatrixDimensions(B);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = A[i, j] / B.A[i, j];
                }
            }
            return this;
        }

        /**
         * Element-by-element left division, C = A.\B
         * 
         * @param B
         *            another matrix
         * @return A.\B
         */

        public Matrix arrayLeftDivide(Matrix B)
        {
            checkMatrixDimensions(B);
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = B.A[i, j] / A[i, j];
                }
            }
            return X;
        }

        /**
         * Element-by-element left division in place, A = A.\B
         * 
         * @param B
         *            another matrix
         * @return A.\B
         */

        public Matrix arrayLeftDivideEquals(Matrix B)
        {
            checkMatrixDimensions(B);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = B.A[i, j] / A[i, j];
                }
            }
            return this;
        }

        /**
         * Multiply a matrix by a scalar, C = s*A
         * 
         * @param s
         *            scalar
         * @return s*A
         */

        public Matrix times(double s)
        {
            Matrix X = new Matrix(m, n);
            double[,] C = X.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C[i, j] = s * A[i, j];
                }
            }
            return X;
        }

        /**
         * Multiply a matrix by a scalar in place, A = s*A
         * 
         * @param s
         *            scalar
         * @return replace A by s*A
         */

        public Matrix timesEquals(double s)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = s * A[i, j];
                }
            }
            return this;
        }

        /**
         * Linear algebraic matrix multiplication, A * B
         * 
         * @param B
         *            another matrix
         * @return Matrix product, A * B
         * @exception IllegalArgumentException
         *                Matrix inner dimensions must agree.
         */

        public Matrix times(Matrix B)
        {
            if (B.m != n)
            {
                throw new IllegalArgumentException("Matrix inner dimensions must agree.");
            }
            Matrix X = new Matrix(m, B.n);
            double[,] C = X.getArray();
            double[] Bcolj = new double[n];
            for (int j = 0; j < B.n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    Bcolj[k] = B.A[k, j];
                }
                for (int i = 0; i < m; i++)
                {
                    double s = 0;
                    for (int k = 0; k < n; k++)
                    {
                        s += A[i, k] * Bcolj[k];
                    }
                    C[i, j] = s;
                }
            }
            return X;
        }

        /**
         * LU Decomposition
         * 
         * @return LUDecomposition
         * @see LUDecomposition
         */

        public LUDecomposition lu()
        {
            return new LUDecomposition(this);
        }

        /**
         * Solve A*X = B
         * 
         * @param B
         *            right hand side
         * @return solution if A is square, least squares solution otherwise
         */

        public Matrix solve(Matrix B)
        {
            // assumed m == n
            return new LUDecomposition(this).solve(B);

        }

        /**
         * Solve X*A = B, which is also A'*X' = B'
         * 
         * @param B
         *            right hand side
         * @return solution if A is square, least squares solution otherwise.
         */

        public Matrix solveTranspose(Matrix B)
        {
            return transpose().solve(B.transpose());
        }

        /**
         * Matrix inverse or pseudoinverse
         * 
         * @return inverse(A) if A is square, pseudoinverse otherwise.
         */

        public Matrix inverse()
        {
            return solve(identity(m, m));
        }

        /**
         * Matrix determinant
         * 
         * @return determinant
         */

        public double det()
        {
            return new LUDecomposition(this).det();
        }

        /**
         * Matrix rank
         * 
         * @return effective numerical rank, obtained from SVD.
         */

        // public int rank () {
        // return new SingularValueDecomposition(this).rank();
        // }
        //
        // /** Matrix condition (2 norm)
        // @return ratio of largest to smallest singular value.
        // */
        //
        // public double cond () {
        // return new SingularValueDecomposition(this).cond();
        // }
        /**
         * Matrix trace.
         * 
         * @return sum of the diagonal elements.
         */

        public double trace()
        {
            double t = 0;
            for (int i = 0; i < System.Math.Min(m, n); i++)
            {
                t += A[i, i];
            }
            return t;
        }

        /**
         * Generate matrix with random elements
         * 
         * @param m
         *            Number of rows.
         * @param n
         *            Number of colums.
         * @return An m-by-n matrix with uniformly distributed random elements.
         */

        public static Matrix random(int m, int n)
        {
            IRandom _random = new DefaultRandom();
            Matrix A = new Matrix(m, n);
            double[,] X = A.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    X[i, j] = _random.NextDouble();
                }
            }
            return A;
        }

        /**
         * Generate identity matrix
         * 
         * @param m
         *            Number of rows.
         * @param n
         *            Number of colums.
         * @return An m-by-n matrix with ones on the diagonal and zeros elsewhere.
         */

        public static Matrix identity(int m, int n)
        {
            Matrix A = new Matrix(m, n);
            double[,] X = A.getArray();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    X[i, j] = (i == j ? 1.0 : 0.0);
                }
            }
            return A;
        }

        /**
         * Print the matrix to the output stream. Line the elements up in columns.
         * Use the format object, and right justify within columns of width
         * characters. Note that is the matrix is to be read back in, you probably
         * will want to use a NumberFormat that is set to US Locale.
         * 
         * @param output
         *            the output stream.
         * @param format
         *            A formatting object to format the matrix elements
         * @param width
         *            Column width.
         * @see java.text.DecimalFormat#setDecimalFormatSymbols
         */

        public void print(StringBuilder output, int width)
        {
            output.AppendLine(); // start on new line.
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string s = A[i, j].ToString(); // format the number
                    int padding = System.Math.Max(1, width - s.Length); // At _least_ 1
                                                                        // space
                    for (int k = 0; k < padding; k++)
                        output.Append(' ');
                    output.Append(s);
                }
                output.AppendLine();
            }
            output.AppendLine(); // end with blank line.
        }



        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < getRowDimension(); i++)
            {

                for (int j = 0; j < getColumnDimension(); j++)
                {
                    buf.Append(get(i, j));
                    buf.Append(" ");
                }
                buf.Append("\n");
            }

            return buf.ToString();
        }

        /** Check if size(A) == size(B) * */ 
        private void checkMatrixDimensions(Matrix B)
        {
            if (B.m != m || B.n != n)
            {
                throw new IllegalArgumentException("Matrix dimensions must agree.");
            }
        }
    } 
}
