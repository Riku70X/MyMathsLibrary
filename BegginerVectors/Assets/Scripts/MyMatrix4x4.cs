using UnityEngine;

public class MyMatrix4x4
{
    public float[,] values;

    public MyMatrix4x4(MyVector4 column1, MyVector4 column2, MyVector4 column3, MyVector4 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column2.w;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }

    public MyMatrix4x4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public static MyMatrix4x4 identity
    {
        get
        {
            return new MyMatrix4x4(
                new MyVector4(1, 0, 0, 0),
                new MyVector4(0, 1, 0, 0),
                new MyVector4(0, 0, 1, 0),
                new MyVector4(0, 0, 0, 1));
        }
    }

    public override string ToString()
    {
        return ($"({GetRow(0)}\n{GetRow(1)}\n{GetRow(2)}\n{GetRow(3)})");
    }

    public static MyVector4 operator *(MyMatrix4x4 lhs, MyVector4 rhs) => MyMathsLibrary.MultiplyMatrices4x4by4x1(lhs, rhs);

    public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs) => MyMathsLibrary.MultiplyMatrices4x4by4x4(lhs, rhs);

    public MyVector4 GetRow(int row)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = values[row, 0],
            y = values[row, 1],
            z = values[row, 2],
            w = values[row, 3]
        };

        return returnVector;
    }

    public MyVector4 GetColumn(int column)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = values[0, column],
            y = values[1, column],
            z = values[2, column],
            w = values[3, column]
        };

        return returnVector;
    }

    public MyMatrix4x4 ScaleInverse()
    {
        MyMatrix4x4 returnMatrix = identity;
        returnMatrix.values[0, 0] = 1 / values[0, 0];
        returnMatrix.values[1, 1] = 1 / values[1, 1];
        returnMatrix.values[2, 2] = 1 / values[2, 2];
        return returnMatrix;
    }

    public MyMatrix4x4 RotationInverse() => new MyMatrix4x4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));

    public MyMatrix4x4 TranslationInverse()
    {
        MyMatrix4x4 returnMatrix = identity;

        returnMatrix.values[0, 3] = -values[0, 3];
        returnMatrix.values[1, 3] = -values[1, 3];
        returnMatrix.values[2, 3] = -values[2, 3];

        return returnMatrix;
    }
}