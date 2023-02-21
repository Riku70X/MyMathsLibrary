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

    public float GetVectorLength() => Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));

    public float GetVectorLengthSquared() => (x * x) + (y * y) + (z * z) + (w * w);

    public static MyVector4 AddVector(MyVector4 vectorA, MyVector4 vectorB)
    {
        MyVector4 vectorSum = new(0, 0, 0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y,
            z = vectorA.z + vectorB.z,
            w = vectorA.w + vectorB.w
        };
        return vectorSum;
    }

    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs) => AddVector(lhs, rhs);

    public static MyVector4 SubtractVector(MyVector4 vectorA, MyVector4 vectorB)
    {
        MyVector4 vectorDifference = new(0, 0, 0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y,
            z = vectorA.z - vectorB.z,
            w = vectorA.w - vectorB.w
        };
        return vectorDifference;
    }

    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs) => SubtractVector(lhs, rhs);

    public static MyVector4 MultiplyVector(MyVector4 vector, float multiplier)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier; returnVector.w *= multiplier;
        return returnVector;
    }

    public static MyVector4 operator *(MyVector4 lhs, float rhs) => MultiplyVector(lhs, rhs);

    public static MyVector4 DivideVector(MyVector4 vector, float divisor)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor; returnVector.w /= divisor;
        return returnVector;
    }

    public static MyVector4 operator /(MyVector4 lhs, float rhs) => DivideVector(lhs, rhs);

    public static bool CheckIfIdentical(MyVector4 vectorA, MyVector4 vectorB)
    {
        if (vectorA.x == vectorB.x && vectorA.y == vectorB.y && vectorA.z == vectorB.z && vectorA.w == vectorB.w)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator ==(MyVector4 lhs, MyVector4 rhs) => CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector4 lhs, MyVector4 rhs) => !CheckIfIdentical(lhs, rhs);

    public MyVector4 NormaliseVector()
    {
        MyVector4 returnVector = new(x, y, z, w);
        returnVector /= GetVectorLength();
        return returnVector;
    }

    public static float GetDotProduct(MyVector4 vectorA, MyVector4 vectorB, bool shouldNormalise = true)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.NormaliseVector();
            vectorB = vectorB.NormaliseVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z) + (vectorA.w * vectorB.w));

        return dotProduct;
    }

    public static MyVector4 GetLerp(MyVector4 vectorA, MyVector4 vectorB, float t)
    {
        MyVector4 returnVector;
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }
}