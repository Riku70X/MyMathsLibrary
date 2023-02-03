using UnityEngine;

public class MyMatrix4x4 : MonoBehaviour
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
                new MyVector4(0, 0, 0, 1)); // gives a warning about "new MyMatrix4x4", not sure why
        }
    }

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

    public static MyVector4 MultiplyMatrices4x4by4x1 (MyMatrix4x4 matrix, MyVector4 vector)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = matrix.values[0, 0] * vector.x + matrix.values[0, 1] * vector.y + matrix.values[0, 2] * vector.z + matrix.values[0, 3] * vector.w,
            y = matrix.values[1, 0] * vector.x + matrix.values[1, 1] * vector.y + matrix.values[1, 2] * vector.z + matrix.values[1, 3] * vector.w,
            z = matrix.values[2, 0] * vector.x + matrix.values[2, 1] * vector.y + matrix.values[2, 2] * vector.z + matrix.values[2, 3] * vector.w,
            w = matrix.values[3, 0] * vector.x + matrix.values[3, 1] * vector.y + matrix.values[3, 2] * vector.z + matrix.values[3, 3] * vector.w
        };

        return returnVector;
    }

    public static MyVector4 operator *(MyMatrix4x4 lhs, MyVector4 rhs)
    {
        return MultiplyMatrices4x4by4x1(lhs, rhs);
    }

    public static MyMatrix4x4 MultiplyMatrices4x4by4x4 (MyMatrix4x4 matrixA, MyMatrix4x4 matrixB)
    {
        MyMatrix4x4 returnMatrix = identity;

        for (int row = 0; row < 4; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                returnMatrix.values[row, column] = MyVector4.GetDotProduct(matrixA.GetRow(row), matrixB.GetColumn(column));
            }
        }
        
        return returnMatrix;
    }

    public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
    {
        return MultiplyMatrices4x4by4x4(lhs, rhs);
    }

    public static MyVector3 ScaleVector (MyVector3 vector3, MyVector3 scalar)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;
        MyMatrix4x4 scaleMatrix = new(new MyVector3(scalar.x, 0, 0), new MyVector3(0, scalar.y, 0), new MyVector3(0, 0, scalar.z), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why

        returnVector4 = scaleMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 RotateVector(MyVector3 vector3, float pitch, float yaw, float roll)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;
        MyMatrix4x4 rotateMatrix = new(new MyVector3(Mathf.Cos(roll), Mathf.Sin(roll), 0), new MyVector3(-Mathf.Sin(roll), Mathf.Cos(roll), 0), new MyVector3(0, 0, 1), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why

        returnVector4 = rotateMatrix * vector4;

        rotateMatrix = new MyMatrix4x4(new MyVector3(Mathf.Cos(yaw), 0, -Mathf.Sin(yaw)), new MyVector3(0, 1, 0), new MyVector3(Mathf.Sin(yaw), 0, Mathf.Cos(yaw)), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why

        returnVector4 = rotateMatrix * returnVector4;

        rotateMatrix = new MyMatrix4x4(new MyVector3(1, 0, 0), new MyVector3(0, Mathf.Cos(pitch), Mathf.Sin(pitch)), new MyVector3(0, -Mathf.Sin(pitch), Mathf.Cos(pitch)), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why

        returnVector4 = rotateMatrix * returnVector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 TranslateVector(MyVector3 vector3, MyVector3 translation)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;
        MyMatrix4x4 translateMatrix = new(new MyVector3(1,0,0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1), new MyVector3(translation.x, translation.y, translation.z)); // gives a warning about "new MyMatrix4x4", not sure why

        returnVector4 = translateMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 TransformVector(MyVector3 vector3, MyVector3 scalar, MyVector3 rotation, MyVector3 translation)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;

        MyMatrix4x4 scaleMatrix = new(new MyVector3(scalar.x, 0, 0), new MyVector3(0, scalar.y, 0), new MyVector3(0, 0, scalar.z), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why
        MyMatrix4x4 rollMatrix = new(new MyVector3(Mathf.Cos(rotation.z), Mathf.Sin(rotation.z), 0), new MyVector3(-Mathf.Sin(rotation.z), Mathf.Cos(rotation.z), 0), new MyVector3(0, 0, 1), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why
        MyMatrix4x4 yawMatrix = new(new MyVector3(Mathf.Cos(rotation.y), 0, -Mathf.Sin(rotation.y)), new MyVector3(0, 1, 0), new MyVector3(Mathf.Sin(rotation.y), 0, Mathf.Cos(rotation.y)), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why
        MyMatrix4x4 pitchMatrix = new(new MyVector3(1, 0, 0), new MyVector3(0, Mathf.Cos(rotation.x), Mathf.Sin(rotation.x)), new MyVector3(0, -Mathf.Sin(rotation.x), Mathf.Cos(rotation.x)), new MyVector3(0, 0, 0)); // gives a warning about "new MyMatrix4x4", not sure why
        MyMatrix4x4 translateMatrix = new(new MyVector3(1, 0, 0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1), new MyVector3(translation.x, translation.y, translation.z)); // gives a warning about "new MyMatrix4x4", not sure why

        MyMatrix4x4 transformMatrix = translateMatrix * (pitchMatrix * (yawMatrix * (rollMatrix * scaleMatrix)));

        returnVector4 = transformMatrix * vector4;

        returnVector3 = new(returnVector4.x, returnVector4.y, returnVector4.z);
        return returnVector3;
    }
    public MyMatrix4x4 ScaleInverse()
    {
        MyMatrix4x4 returnMatrix = identity;
        returnMatrix.values[0, 0] = 1 / values[0, 0];
        returnMatrix.values[1, 1] = 1 / values[1, 1];
        returnMatrix.values[2, 2] = 1 / values[2, 2];
        return returnMatrix;
    }

    public MyMatrix4x4 RotationInverse()
    {
        return new MyMatrix4x4(GetRow(0), GetRow(1), GetRow(2), GetRow(3)); // gives a warning about "new MyMatrix4x4", not sure why
    }

    public MyMatrix4x4 TranslationInverse()
    {
        MyMatrix4x4 returnMatrix = identity;

        returnMatrix.values[0, 3] = -values[0, 3];
        returnMatrix.values[1, 3] = -values[1, 3];
        returnMatrix.values[2, 3] = -values[2, 3];

        return returnMatrix;
    }
}