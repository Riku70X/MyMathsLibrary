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
    
    public MyVector4(Vector4 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
        this.w = vector.w;
    }

    public static MyVector4 zero
    { get { return new MyVector4(0, 0, 0, 0); } }

    public static MyVector4 one
    { get { return new MyVector4(1, 1, 1, 1); } }

    public override string ToString()
    {
        return ($"({x}, {y}, {z}, {w})");
    }

    public Vector4 ConvertToUnityVector()
    {
        Vector4 returnVector = new(x, y, z, w);
        return returnVector;
    }

    public static Vector4[] ConvertToUnityVectorArray(MyVector4[] vectorArray)
    {
        Vector4[] returnVectorArray = new Vector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i].ConvertToUnityVector();
        }
        return returnVectorArray;
    }

    public static MyVector4 ConvertToCustomVector(Vector4 vector)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        return returnVector;
    }

    public static MyVector4[] ConvertToCustomVectorArray(Vector4[] vectorArray)
    {
        MyVector4[] returnVectorArray = new MyVector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = ConvertToCustomVector(vectorArray[i]);
        }
        return returnVectorArray;
    }

    public MyVector2 ConvertToMyVector2()
    {
        return new MyVector2(x, y);
    }

    public MyVector3 ConvertToMyVector3()
    {
        return new MyVector3(x, y, z);
    }

    public float GetVectorLength()
    {
        float length = Mathf.Sqrt((x * x) + (y * y) + (z * z) + (w * w));
        return length;
    }

    public float GetVectorLengthSquared()
    {
        float length = (x * x) + (y * y) + (z * z) + (w * w);
        return length;
    }
    
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

    public static MyVector4 operator +(MyVector4 lhs, MyVector4 rhs)
    {
        return AddVector(lhs, rhs);
    }

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

    public static MyVector4 operator -(MyVector4 lhs, MyVector4 rhs)
    {
        return SubtractVector(lhs, rhs);
    }
    
    public static MyVector4 MultiplyVector(MyVector4 vector, float multiplier)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier; returnVector.w *= multiplier;
        return returnVector;
    }

    public static MyVector4 operator *(MyVector4 lhs, float rhs)
    {
        return MultiplyVector(lhs, rhs);
    }

    public static MyVector4 DivideVector(MyVector4 vector, float divisor)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor; returnVector.w /= divisor;
        return returnVector;
    }

    public static MyVector4 operator /(MyVector4 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

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

    public static bool operator ==(MyVector4 lhs, MyVector4 rhs)
    {
        return CheckIfIdentical(lhs, rhs);
    }

    public static bool operator !=(MyVector4 lhs, MyVector4 rhs)
    {
        return !CheckIfIdentical(lhs, rhs);
    }

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