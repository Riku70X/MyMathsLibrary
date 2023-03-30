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
        values[3, 0] = column1.w;

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

    public MyVector4 GetRow(int rowIndex)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = values[rowIndex, 0],
            y = values[rowIndex, 1],
            z = values[rowIndex, 2],
            w = values[rowIndex, 3]
        };

        return returnVector;
    }

    public void SetRow(int rowIndex, MyVector4 row)
    {
        values[rowIndex, 0] = row.x;
        values[rowIndex, 1] = row.y;
        values[rowIndex, 2] = row.z;
        values[rowIndex, 3] = row.w;
    }

    public MyVector4 GetColumn(int columnIndex)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = values[0, columnIndex],
            y = values[1, columnIndex],
            z = values[2, columnIndex],
            w = values[3, columnIndex]
        };

        return returnVector;
    }

    public void SetColumn(int columnIndex, MyVector4 column)
    {
        values[0, columnIndex] = column.x;
        values[1, columnIndex] = column.y;
        values[2, columnIndex] = column.z;
        values[3, columnIndex] = column.w;
    }

    public MyMatrix4x4 GetScaleInverse()
    {
        MyMatrix4x4 returnMatrix = identity;
        returnMatrix.values[0, 0] = 1 / values[0, 0];
        returnMatrix.values[1, 1] = 1 / values[1, 1];
        returnMatrix.values[2, 2] = 1 / values[2, 2];
        return returnMatrix;
    }

    public MyMatrix4x4 GetRotationInverse() => new(GetRow(0), GetRow(1), GetRow(2), GetRow(3));

    public MyMatrix4x4 GetTranslationInverse()
    {
        MyMatrix4x4 returnMatrix = identity;

        returnMatrix.values[0, 3] = -values[0, 3];
        returnMatrix.values[1, 3] = -values[1, 3];
        returnMatrix.values[2, 3] = -values[2, 3];

        return returnMatrix;
    }

    public MyMatrix4x4 GetTRInverse()
    {
        MyMatrix4x4 returnMatrix = identity;

        // transpose the 3x3 section

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                returnMatrix.values[i, j] = values[j, i];

        // set the last column to -transpose(r3)*t

        returnMatrix.SetColumn(3, -(returnMatrix * GetColumn(3)));

        // set the bottom right corner from -1 back to 1

        returnMatrix.values[3, 3] = 1;

        return returnMatrix;
    }
}