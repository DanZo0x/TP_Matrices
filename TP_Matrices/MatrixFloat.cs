namespace TP_Matrices;

public class MatrixFloat
{
    private float[,] _matrix;

    public MatrixFloat(int inLines, int inColumns)
    {
        _matrix = new float[inLines, inColumns];
    }

    public MatrixFloat(float[,] inArray)
    {
        _matrix = inArray;
    }

    public int NbLines => _matrix.GetLength(0);
    public int NbColumns => _matrix.GetLength(1);
    
    public float this[int inLine, int inColumn]
    {
        get => _matrix[inLine, inColumn];
        set => _matrix[inLine, inColumn] = value;
    }

    public float[,] ToArray2D()
    {
        return _matrix;
    }
    
    public static MatrixFloat Identity(int inNumber)
    {
        float[,] tempMatrix = new float[inNumber, inNumber];

        for (int i = 0; i < inNumber; i++)
        {
            tempMatrix[i, i] = 1;
        }

        return new MatrixFloat(tempMatrix);
    }

    public MatrixFloat InvertByRowReduction()
    {
        MatrixFloat tempIdentityMatrix = Identity(NbLines);
        (MatrixFloat, MatrixFloat) tempMatrix = MatrixRowReductionAlgorithm.Apply(this, tempIdentityMatrix);
        return tempMatrix.Item2;
    }

    public static MatrixFloat InvertByRowReduction(MatrixFloat inMatrix)
    {
        return inMatrix.InvertByRowReduction();
    }

    public MatrixFloat SubMatrix(int inLine, int inColumn)
    {
        int tempLines = NbLines - 1;
        int tempColumns = NbColumns - 1;
        
        float[,] result = new float[tempLines, tempColumns];
        int resultLine = 0;

        for (int i = 0; i < NbLines; i++)
        {
            if (i == inLine) continue;

            int resultColumn = 0;
            for (int j = 0; j < NbColumns; j++)
            {
                if (j == inColumn) continue;

                result[resultLine, resultColumn] = this[i, j];
                resultColumn++;
            }

            resultLine++;
        }

        return new MatrixFloat(result);
    }

    public static MatrixFloat SubMatrix(MatrixFloat inMatrix, int inLine, int inColumn)
    {
        return inMatrix.SubMatrix(inLine, inColumn);
    }

    public static float Determinant(MatrixFloat inMatrix)
    {
        if (inMatrix.NbLines != inMatrix.NbColumns)
        {
            throw new MatrixDeterminantException("Determinant: matrixFloat.NbLines != matrixFloat.NbColumns");
        }

        switch (inMatrix.NbLines)
        {
            case 1:
                return inMatrix[0, 0];
            case 2:
                return inMatrix[0, 0] * inMatrix[1, 1] - inMatrix[0, 1] * inMatrix[1, 0];
            default:
                float determinant = 0f;
                for (int column = 0; column < inMatrix.NbLines; column++)
                {
                    MatrixFloat tempSubMatrix = inMatrix.SubMatrix(0, column);
            
                    float coFactor = (column % 2 == 0 ? 1 : -1) * inMatrix[0, column] * Determinant(tempSubMatrix);
                    determinant += coFactor;
                }

                return determinant;
        }
    }

    public MatrixFloat Adjugate()
    {
        if (NbLines != NbColumns)
        {
            throw new MatrixAdjugateException("Adjugate: NbLines != NbColumns");
        }

        MatrixFloat adjugateMatrix = new MatrixFloat(NbLines, NbLines);

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                MatrixFloat tempSubMatrix = SubMatrix(i, j);
                
                float coFactor = Determinant(tempSubMatrix) * ((i + j) % 2 == 0 ? 1 : -1);
                adjugateMatrix[j, i] = coFactor;
            }
        }

        return adjugateMatrix;
    }

    public static MatrixFloat Adjugate(MatrixFloat inMatrix)
    {
        return inMatrix.Adjugate();
    }

    public MatrixFloat InvertByDeterminant()
    {
        MatrixFloat tempMatrix = new MatrixFloat(_matrix);
        float determinant = Determinant(tempMatrix);
        
        if (NbLines != NbColumns || Math.Abs(determinant) < 0.001)
        {
            throw new MatrixInvertException("InvertByDeterminant: NbLines != NbColumns || Math.Abs(determinant) < 0.001");
        }
        
        int lines = NbLines;
        int columns = NbColumns;
        
        MatrixFloat tempAdjugateMatrix = Adjugate(this);
        
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                tempAdjugateMatrix[i, j] /= determinant;
            }
        }

        return tempAdjugateMatrix;
    }

    public static MatrixFloat InvertByDeterminant(MatrixFloat inMatrix)
    {
        return inMatrix.InvertByDeterminant();
    }
    
    public MatrixFloat Multiply(MatrixFloat inMatrix)
    {
        if (NbColumns != inMatrix.NbLines)
        {
            throw new MatrixMultiplyException("Multiply: NbColumns != inMatrix.NbLines");
        }
        
        float[,] tempMatrix = new float[NbLines, inMatrix.NbColumns];

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < inMatrix.NbColumns; j++)
            {
                float add = 0;
                
                for (int k = 0; k < NbColumns; k++)
                {
                    add += _matrix[i, k] * inMatrix[k, j];
                }
                
                tempMatrix[i, j] = add;
            }
        }

        return new MatrixFloat(tempMatrix);
    }
}