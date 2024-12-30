namespace TP_Matrices;

public class MatrixElementaryOperations
{
    public static void SwapLines(MatrixInt inMatrix, int inLine1, int inLine2)
    {
        for (int column = 0; column < inMatrix.NbColumns; column++)
        {
            (inMatrix[inLine1, column], inMatrix[inLine2, column]) = (inMatrix[inLine2, column], inMatrix[inLine1, column]);
        }
    }
    
    public static void SwapLinesFloat(MatrixFloat inMatrix, int inLine1, int inLine2)
    {
        for (int column = 0; column < inMatrix.NbColumns; column++)
        {
            (inMatrix[inLine1, column], inMatrix[inLine2, column]) = (inMatrix[inLine2, column], inMatrix[inLine1, column]);
        }
    }

    public static void SwapColumns(MatrixInt inMatrix, int inLine1, int inLine2)
    {
        for (int line = 0; line < inMatrix.NbLines; line++)
        {
            (inMatrix[line, inLine1], inMatrix[line, inLine2]) = (inMatrix[line, inLine2], inMatrix[line, inLine1]);
        }
    }

    public static void MultiplyLine(MatrixInt inMatrix, int inLine, int inScalar)
    {
        if (inScalar == 0)
        {
            throw new MatrixScalarZeroException("MultiplyLine: inScalar == 0");
        }
        
        for (int column = 0; column < inMatrix.NbColumns; column++)
        {
            inMatrix[inLine, column] *= inScalar;
        }
    }

    public static void MultiplyColumn(MatrixInt inMatrix, int inColumn, int inScalar)
    {
        if (inScalar == 0)
        {
            throw new MatrixScalarZeroException("MultiplyColumn: inScalar == 0");
        }
        
        for (int line = 0; line < inMatrix.NbLines; line++)
        {
            inMatrix[inColumn, line] *= inScalar;
        }
    }

    public static void AddLineToAnother(MatrixInt inMatrix, int inLine1, int inLine2, int inScalar)
    {
        for (int column = 0; column < inMatrix.NbColumns; column++)
        {
            inMatrix[inLine1, column] += inMatrix[inLine2, column] * inScalar;
        }
    }

    public static void AddColumnToAnother(MatrixInt inMatrix, int inLine1, int inLine2, int inScalar)
    {
        for (int line = 0; line < inMatrix.NbLines; line++)
        {
            inMatrix[line, inLine2] += inMatrix[line, inLine1] * inScalar;
        }
    }
}