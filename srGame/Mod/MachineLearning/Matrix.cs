using System;
public class Matrix
{
    private readonly int rows;
    private readonly int cols;
    private readonly double[,] data;

    public int Rows => rows;
    public int Columns => cols;

    public Matrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.data = new double[rows, cols];
    }

    public double this[int row, int col]
    {
        get => data[row, col];
        set => data[row, col] = value;
    }

    // Phép nhân ma trận
    // Phương thức này thực hiện phép nhân hai ma trận và trả về một ma trận mới
    public static Matrix Multiply(Matrix a, Matrix b)
    {
        if (a.cols != b.rows)
            throw new ArgumentException("Number of columns in the first matrix must be equal to the number of rows in the second matrix.");

        Matrix result = new Matrix(a.rows, b.cols);
        for (int i = 0; i < result.rows; i++)
        {
            for (int j = 0; j < result.cols; j++)
            {
                for (int k = 0; k < a.cols; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return result;
    }

    // Chuyển vị ma trận
    // Phương thức này thực hiện chuyển vị của một ma trận và trả về ma trận kết quả
    public static Matrix Transpose(Matrix a)
    {
        Matrix result = new Matrix(a.cols, a.rows);
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.cols; j++)
            {
                result[j, i] = a[i, j];
            }
        }
        return result;
    }

    // Tính tích vô hướng của hai vector
    // Phương thức này tính tích vô hướng của hai vector và trả về một giá trị scalar
    public static double DotProduct(Matrix a, Matrix b)
    {
        if (a.Rows != 1 || b.Rows != 1 || a.Columns != b.Columns)
            throw new ArgumentException("Both matrices must be vectors of the same length.");

        double result = 0;
        for (int i = 0; i < a.Columns; i++)
        {
            result += a[0, i] * b[0, i];
        }
        return result;
    }

    // Nhân một ma trận với một scalar
    // Phương thức này nhân một ma trận với một scalar và trả về ma trận kết quả
    public static Matrix Multiply(Matrix a, double scalar)
    {
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] * scalar;
            }
        }
        return result;
    }

    // Chia một ma trận cho một scalar
    // Phương thức này chia một ma trận cho một scalar và trả về ma trận kết quả
    public static Matrix Divide(Matrix a, double scalar)
    {
        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] / scalar;
            }
        }
        return result;
    }

    // Tính norm của một vector
    // Phương thức này tính norm của một vector và trả về một giá trị scalar
    public static double Norm(Matrix a)
    {
        if (a.Rows != 1 && a.Columns != 1)
            throw new ArgumentException("Matrix must be a vector.");

        double sumOfSquares = 0;
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                sumOfSquares += a[i, j] * a[i, j];
            }
        }
        return Math.Sqrt(sumOfSquares);
    }
    public static Matrix Add(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Matrix dimensions must be the same for addition.");

        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }
    public static Matrix Subtract(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Matrix dimensions must be the same for subtraction.");

        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }
    public static Matrix HadamardProduct(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            throw new ArgumentException("Matrix dimensions must be the same for Hadamard product.");

        Matrix result = new Matrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] * b[i, j];
            }
        }
        return result;
    }
    public static bool AreEqual(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns)
            return false;

        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                if (a[i, j] != b[i, j])
                    return false;
            }
        }
        return true;
    }

}
