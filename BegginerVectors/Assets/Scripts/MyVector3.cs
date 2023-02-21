using System;
using UnityEngine;

[Serializable]
public class MyVector3
{
    public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyVector3(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public static MyVector3 zero => new(0, 0, 0);

    public static MyVector3 one => new(1, 1, 1);

    public static MyVector3 right => new(1, 0, 0);

    public static MyVector3 up => new(0, 1, 0);

    public static MyVector3 forward => new(0, 0, 1);

    public override string ToString() => ($"({x}, {y}, {z})");

    public static implicit operator Vector3(MyVector3 vector) => new(vector.x, vector.y, vector.z);

    public static Vector3[] ConvertToUnityVectorArray(MyVector3[] vectorArray)
    {
        Vector3[] returnVectorArray = new Vector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector3(Vector3 vector) => new(vector.x, vector.y, vector.z);

    public static MyVector3[] ConvertToCustomVectorArray(Vector3[] vectorArray)
    {
        MyVector3[] returnVectorArray = new MyVector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector2(MyVector3 vector) => new(vector.x, vector.y);

    public static implicit operator MyVector4(MyVector3 vector) => new(vector.x, vector.y, vector.z, 1);

    public float GetVectorLength() => Mathf.Sqrt((x * x) + (y * y) + (z * z));

    public float GetVectorLengthSquared() => (x * x) + (y * y) + (z * z);

    public static MyVector3 GetNegativeVector(MyVector3 vector) => new MyVector3(-vector.x, -vector.y, -vector.z);

    public static MyVector3 operator -(MyVector3 vector) => GetNegativeVector(vector);

    public static MyVector3 AddVector(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorSum = new(0, 0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y,
            z = vectorA.z + vectorB.z
        };
        return vectorSum;
    }

    public static MyVector3 AddVector(Vector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorSum = new(0, 0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y,
            z = vectorA.z + vectorB.z
        };
        return vectorSum;
    }

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs) => AddVector(lhs, rhs);
    public static MyVector3 operator +(Vector3 lhs, MyVector3 rhs) => AddVector(lhs, rhs);

    public static MyVector3 SubtractVector(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorDifference = new(0, 0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y,
            z = vectorA.z - vectorB.z
        };
        return vectorDifference;
    }

    public static MyVector3 SubtractVector(Vector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorDifference = new(0, 0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y,
            z = vectorA.z - vectorB.z
        };
        return vectorDifference;
    }

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs) => SubtractVector(lhs, rhs);
    public static MyVector3 operator -(Vector3 lhs, MyVector3 rhs) => SubtractVector(lhs, rhs);

    public static MyVector3 MultiplyVector(MyVector3 vector, float multiplier)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier;
        return returnVector;
    }

    public static MyVector3 operator *(MyVector3 lhs, float rhs) => MultiplyVector(lhs, rhs);
    public static MyVector3 operator *(float lhs, MyVector3 rhs) => MultiplyVector(rhs, lhs);

    public static MyVector3 DivideVector(MyVector3 vector, float divisor)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor;
        return returnVector;
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs) => DivideVector(lhs, rhs);
    public static MyVector3 operator /(float lhs, MyVector3 rhs) => DivideVector(rhs, lhs);

    public static bool CheckIfIdentical(MyVector3 vectorA, MyVector3 vectorB)
    {
        if (vectorA.x == vectorB.x && vectorA.y == vectorB.y && vectorA.z == vectorB.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator ==(MyVector3 lhs, MyVector3 rhs) => CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector3 lhs, MyVector3 rhs) => !CheckIfIdentical(lhs, rhs);

    public MyVector3 NormaliseVector()
    {
        MyVector3 returnVector = new(x, y, z);
        returnVector /= GetVectorLength();
        return returnVector;
    }

    public static float GetDotProduct(MyVector3 vectorA, MyVector3 vectorB, bool shouldNormalise = true)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.NormaliseVector();
            vectorB = vectorB.NormaliseVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z));

        return dotProduct;
    }

    public static MyVector3 GetLerp(MyVector3 vectorA, MyVector3 vectorB, float t)
    {
        MyVector3 returnVector;
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }
}