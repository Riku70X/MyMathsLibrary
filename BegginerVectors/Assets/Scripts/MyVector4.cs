using System;
using UnityEngine;

[Serializable]
public class MyVector4
{
    public float x, y, z, w;

    public MyVector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public MyVector4(MyVector4 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        w = vector.w;
    }

    public static MyVector4 zero => new MyVector4(0, 0, 0, 0);

    public static MyVector4 one => new MyVector4(1, 1, 1, 1);

    public override string ToString() => ($"({x}, {y}, {z}, {w})");

    public static implicit operator Vector4(MyVector4 vector) => new(vector.x, vector.y, vector.z, vector.w);

    public static Vector4[] ConvertToUnityVectorArray(MyVector4[] vectorArray)
    {
        Vector4[] returnVectorArray = new Vector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector4(Vector4 vector) => new(vector.x, vector.y, vector.z, vector.w);

    public static MyVector4[] ConvertToCustomVectorArray(Vector4[] vectorArray)
    {
        MyVector4[] returnVectorArray = new MyVector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector2(MyVector4 vector) => new(vector.x, vector.y);

    public static implicit operator MyVector3(MyVector4 vector) => new(vector.x, vector.y, vector.z);

    public static MyVector4 operator -(MyVector4 vector) => MyMathsLibrary.GetNegativeVector(vector);

    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs) => MyMathsLibrary.AddVector(lhs, rhs);

    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs) => MyMathsLibrary.SubtractVector(lhs, rhs);

    public static MyVector4 operator *(MyVector4 lhs, float rhs) => MyMathsLibrary.MultiplyVector(lhs, rhs);

    public static MyVector4 operator /(MyVector4 lhs, float rhs) => MyMathsLibrary.DivideVector(lhs, rhs);

    public static bool operator ==(MyVector4 lhs, MyVector4 rhs) => MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector4 lhs, MyVector4 rhs) => !MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public float GetVectorLength() => Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));

    public float GetVectorLengthSquared() => (x * x) + (y * y) + (z * z) + (w * w);

    public MyVector4 NormaliseVector()
    {
        MyVector4 returnVector = new(x, y, z, w);
        returnVector /= GetVectorLength();
        return returnVector;
    }
}