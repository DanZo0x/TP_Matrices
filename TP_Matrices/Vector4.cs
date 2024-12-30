using TP_Matrices;

namespace TP_Matrices;

public class Vector4
{
    private float[] _vector;

    public Vector4(float x, float y, float z, float w)
    {
        _vector = new float[] { x, y, z, w };
    }

    public Vector4(float[] inArray)
    {
        if (inArray.Length != 4)
        {
            throw new ArgumentException("4 elements or no");
        }
        _vector = inArray;
    }

    public float x => _vector[0];
    public float y => _vector[1];
    public float z => _vector[2];
    public float w => _vector[3];

    public float this[int index]
    {
        get => _vector[index];
        set => _vector[index] = value;
    }

    public float[] ToArray2D()
    {
        return _vector;
    }
    
    public static Vector4 operator *(MatrixFloat inMatrix, Vector4 inVector)
    {
        if (inMatrix.NbLines != 4 || inMatrix.NbColumns != 4)
        {
            throw new MatrixMultiplyException("Vector4 *: inMatrix.NbLines != 4 || inMatrix.NbColumns != 4");
        }

        MatrixFloat tempMatrix = new MatrixFloat(new[,]
        {
            { inVector.x },
            { inVector.y },
            { inVector.z },
            { inVector.w }
        });

        MatrixFloat resultMatrix = inMatrix.Multiply(tempMatrix);

        return new Vector4(resultMatrix[0, 0], resultMatrix[1, 0], resultMatrix[2, 0], resultMatrix[3, 0]);
    }
}