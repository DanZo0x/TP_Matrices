namespace TP_Matrices;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat m1, MatrixFloat m2) Apply(MatrixFloat inMatrix1, MatrixFloat inMatrix2)
    {
        int lines = inMatrix1.NbLines;
        int columns = inMatrix1.NbColumns;
        int i = 0;
        int j = 0;

        while (j < columns && i < lines)
        {
            int biggestIndex = i;
            float biggestVal = Math.Abs(inMatrix1[i, j]);
            
            for (int currentIndex = i + 1; currentIndex < lines; currentIndex++)
            {
                float currentVal = Math.Abs(inMatrix1[currentIndex, j]);
                
                if (currentVal > biggestVal)
                {
                    biggestVal = currentVal;
                    biggestIndex = currentIndex;
                }
            }

            if (biggestVal == 0)
            {
                j++;
                continue;
            }

            if (biggestIndex != i)
            {
                MatrixElementaryOperations.SwapLinesFloat(inMatrix1, i, biggestIndex);
                MatrixElementaryOperations.SwapLinesFloat(inMatrix2, i, biggestIndex);
            }

            float pivot = inMatrix1[i, j];
            
            for (int col = 0; col < columns; col++)
            {
                inMatrix1[i, col] /= pivot;
            }
            
            for (int col = 0; col < inMatrix2.NbColumns; col++)
            {
                inMatrix2[i, col] /= pivot;
            }

            for (int r = 0; r < lines; r++)
            {
                if (r != i)
                {
                    float factor = -inMatrix1[r, j];
                    
                    for (int col = 0; col < columns; col++)
                    {
                        inMatrix1[r, col] += factor * inMatrix1[i, col];
                    }
                    
                    for (int col = 0; col < inMatrix2.NbColumns; col++)
                    {
                        inMatrix2[r, col] += factor * inMatrix2[i, col];
                    }
                }
            }
            
            i++;
            j++;
        }

        return (inMatrix1, inMatrix2);
    }
}