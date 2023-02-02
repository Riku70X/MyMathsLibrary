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

    public static MyVector4 MultiplyMatrices4x4by4x1 (MyMatrix4x4 matrix, MyVector4 vector)
    {
        MyVector4 returnVector = new MyVector4(0, 0, 0, 0);

        returnVector.x = matrix.values[0, 0] * vector.x + matrix.values[0, 1] * vector.y + matrix.values[0, 2] * vector.z + matrix.values[0, 3] * vector.w;
        returnVector.y = matrix.values[1, 0] * vector.x + matrix.values[1, 1] * vector.y + matrix.values[1, 2] * vector.z + matrix.values[1, 3] * vector.w;
        returnVector.z = matrix.values[2, 0] * vector.x + matrix.values[2, 1] * vector.y + matrix.values[2, 2] * vector.z + matrix.values[2, 3] * vector.w;
        returnVector.w = matrix.values[3, 0] * vector.x + matrix.values[3, 1] * vector.y + matrix.values[3, 2] * vector.z + matrix.values[3, 3] * vector.w;

        return returnVector;
    }

    public static MyVector4 operator *(MyMatrix4x4 lhs, MyVector4 rhs)
    {
        return MultiplyMatrices4x4by4x1(lhs, rhs);
    }

    public static MyVector3 ScaleVector (MyVector3 vector3, float scalarX, float scalarY, float scalarZ)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new MyVector4(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4 = new MyVector4(0, 0, 0, 0);
        MyMatrix4x4 scaleMatrix = new MyMatrix4x4(returnVector4, returnVector4, returnVector4, returnVector4); // gives a warning about "new MyMatrix4x4", not sure why

        scaleMatrix.values[0, 0] = scalarX;
        scaleMatrix.values[1, 1] = scalarY;
        scaleMatrix.values[2, 2] = scalarZ;
        scaleMatrix.values[3, 3] = 1;
        returnVector4 = scaleMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 TranslateVector(MyVector3 vector3, float translateX, float translateY, float translateZ)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new MyVector4(vector3.x, vector3.y, vector3.z, 1); 
        MyVector4 returnVector4 = new MyVector4(0, 0, 0, 0);
        MyMatrix4x4 translateMatrix = new MyMatrix4x4(returnVector4, returnVector4, returnVector4, returnVector4); // gives a warning about "new MyMatrix4x4", not sure why

        translateMatrix.values[0, 0] = 1; translateMatrix.values[1, 1] = 1; translateMatrix.values[2, 2] = 1;
        translateMatrix.values[0, 3] = translateX;
        translateMatrix.values[1, 3] = translateY;
        translateMatrix.values[2, 3] = translateZ;
        translateMatrix.values[3, 3] = 1;
        returnVector4 = translateMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }
}