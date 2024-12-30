namespace TP_Matrices;

public class MatrixInt
{
    private int[,] _matrix;

    public MatrixInt(int inLines, int inColumns)
    {
        _matrix = new int[inLines, inColumns];
    }

    public MatrixInt(int[,] inArray)
    {
        _matrix = inArray;
    }

    public int NbLines => _matrix.GetLength(0);
    public int NbColumns => _matrix.GetLength(1);
    

    public int[,] ToArray2D()
    {
        return _matrix;
    }

    public int this[int inLine, int inColumn]
    {
        get => _matrix[inLine, inColumn];
        set => _matrix[inLine, inColumn] = value;
    }

    public MatrixInt(MatrixInt inMatrix)
    {
        int lines = inMatrix.NbLines;
        int columns = inMatrix.NbColumns;
        _matrix = new int[lines, columns];

        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                _matrix[i, j] = inMatrix[i, j];
            }
        }
    }

    public static MatrixInt Identity(int inNumber)
    {
        int[,] tempMatrix = new int[inNumber, inNumber];

        for (int i = 0; i < inNumber; i++)
        {
            tempMatrix[i, i] = 1;
        }

        return new MatrixInt(tempMatrix);
    }


    public bool IsIdentity()
    {
        if (NbLines != NbColumns)
        {
            return false;
        }

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                if (i == j)
                {
                    if (_matrix[i, j] != 1)
                    {
                        return false;
                    }
                }
                else
                {
                    if (_matrix[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    
    public MatrixInt Multiply(MatrixInt inMatrix)
    {
        if (NbColumns != inMatrix.NbLines)
        {
            throw new MatrixMultiplyException("Multiply: NbColumns != inMatrix.NbLines");
        }
        
        int[,] tempMatrix = new int[NbLines, inMatrix.NbColumns];

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < inMatrix.NbColumns; j++)
            {
                int add = 0;
                
                for (int k = 0; k < NbColumns; k++)
                {
                    add += _matrix[i, k] * inMatrix[k, j];
                }
                
                tempMatrix[i, j] = add;
            }
        }

        return new MatrixInt(tempMatrix);
    }
    
    public MatrixInt Multiply(int inFactor)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _matrix[i, j] *= inFactor;
            }
        }

        return new MatrixInt(_matrix);
    }
    
    public static MatrixInt Multiply(MatrixInt inMatrix, int inFactor)
    {
        MatrixInt tempMatrix = new MatrixInt(inMatrix);
        tempMatrix.Multiply(inFactor);
        return tempMatrix;
    }

    public static MatrixInt Multiply(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        if (inMatrix1.NbColumns != inMatrix2.NbLines)
        {
            throw new MatrixMultiplyException("Multiply: inMatrix1.NbColumns != inMatrix2.NbLines");
        }

        int[,] tempMatrix = new int[inMatrix1.NbLines, inMatrix2.NbColumns];

        for (int i = 0; i < inMatrix1.NbLines; i++)
        {
            for (int j = 0; j < inMatrix2.NbColumns; j++)
            {
                int add = 0;
                
                for (int k = 0; k < inMatrix1.NbColumns; k++)
                {
                    add += inMatrix1[i, k] * inMatrix2[k, j];
                }
                
                tempMatrix[i, j] = add;
            }
        }

        return new MatrixInt(tempMatrix);
    }
    
    public static MatrixInt operator *(MatrixInt inMatrix, int inFactor)
    {
        return Multiply(inMatrix, inFactor);
    }
    
    public static MatrixInt operator *(int inFactor, MatrixInt inMatrix)
    {
        return Multiply(inMatrix, inFactor);
    }

    public static MatrixInt operator *(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        return Multiply(inMatrix1, inMatrix2);
    }
    
    public static MatrixInt operator -(MatrixInt inMatrix)
    {
        return Multiply(inMatrix, -1);
    }

    public static MatrixInt operator +(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        return Add(inMatrix1, inMatrix2);
    }

    public static MatrixInt operator -(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        return Subtract(inMatrix1, inMatrix2);
    }

    public MatrixInt Add(MatrixInt inMatrix)
    {
        if (NbLines != inMatrix.NbLines || NbColumns != inMatrix.NbColumns)
        {
            throw new MatrixSumException("Add: NbLines != inMatrix.NbLines || NbColumns != inMatrix.NbColumns");
        }

        int[,] tempMatrix = new int[NbLines, NbColumns];

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                tempMatrix[i, j] = _matrix[i, j] + inMatrix[i, j];
            }
        }

        return new MatrixInt(tempMatrix);
    }
    
    public static MatrixInt Add(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        return inMatrix1.Add(inMatrix2);
    }
    
    public static MatrixInt Subtract(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        if (inMatrix1.NbLines != inMatrix2.NbLines || inMatrix1.NbColumns != inMatrix2.NbColumns)
        {
            throw new InvalidOperationException("Subtract: inMatrix1.NbLines != inMatrix2.NbLines || inMatrix1.NbColumns != inMatrix2.NbColumns");
        }

        int[,] tempMatrix = new int[inMatrix1.NbLines, inMatrix1.NbColumns];

        for (int i = 0; i < inMatrix1.NbLines; i++)
        {
            for (int j = 0; j < inMatrix1.NbColumns; j++)
            {
                tempMatrix[i, j] = inMatrix1[i, j] - inMatrix2[i, j];
            }
        }

        return new MatrixInt(tempMatrix);
    }

    public MatrixInt Transpose()
    {
        int[,] tempMatrix = new int[NbColumns, NbLines];

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                tempMatrix[j, i] = _matrix[i, j];
            }
        }

        return new MatrixInt(tempMatrix);
    }

    public static MatrixInt Transpose(MatrixInt inMatrix)
    {
        MatrixInt tempMatrix = new MatrixInt(inMatrix);
        return tempMatrix.Transpose();
    }

    public static MatrixInt GenerateAugmentedMatrix(MatrixInt inMatrix1, MatrixInt inMatrix2)
    {
        int[,] tempMatrix = new int[inMatrix1.NbLines, inMatrix1.NbColumns + 1];
        
        for (int i = 0; i < inMatrix1.NbLines; i++)
        {
            for (int j = 0; j < inMatrix1.NbColumns; j++)
            {
                tempMatrix[i, j] = inMatrix1[i, j];
            }
        }

        for (int i = 0; i < inMatrix1.NbLines; i++)
        {
            tempMatrix[i, inMatrix1.NbColumns] = inMatrix2[i, 0];
        }

        return new MatrixInt(tempMatrix);
    }

    public (MatrixInt m1, MatrixInt m2) Split(int inColumnIndex)
    {
        int[,] tempMatrix1 = new int[NbLines, inColumnIndex + 1];
        int[,] tempMatrix2 = new int[NbLines, NbColumns - inColumnIndex - 1];
        
        for (int i = 0; i < tempMatrix1.GetLength(1); i++)
        {
            for (int j = 0; j < tempMatrix2.GetLength(0); j++)
            {
                if (i <= inColumnIndex)
                {
                    tempMatrix1[i, j] = _matrix[i, j];
                }
                else
                {
                    tempMatrix2[i, j - inColumnIndex - 1] = _matrix[i, j];
                }
            }
        }
        
        return (new MatrixInt(tempMatrix1), new MatrixInt(tempMatrix2));
    }
}