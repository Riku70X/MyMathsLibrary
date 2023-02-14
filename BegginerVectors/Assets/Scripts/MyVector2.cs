using System;
using UnityEngine;

[Serializable]
public class MyVector2
{
    public float x, y;

    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    
    public MyVector2(Vector2 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
    }

    public static MyVector2 zero => new MyVector2(0, 0);

    public static MyVector2 one => new MyVector2(1, 1);

    public static MyVector2 right => new MyVector2(1, 0);

    public static MyVector2 up => new MyVector2(0, 1);

    public override string ToString() => ($"({x}, {y})");

    public static implicit operator Vector2(MyVector2 vector) => new(vector.x, vector.y);

    public static Vector2[] ConvertToUnityVectorArray(MyVector2[] vectorArray)
    {
        Vector2[] returnVectorArray = new Vector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector2(Vector2 vector) => new(vector.x, vector.y);

    public static MyVector2[] ConvertToCustomVectorArray(Vector2[] vectorArray)
    {
        MyVector2[] returnVectorArray = new MyVector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static implicit operator MyVector3(MyVector2 vector) => new(vector.x, vector.y, 1);

    public static implicit operator MyVector4(MyVector2 vector) => new(vector.x, vector.y, 1, 1);

    public float GetVectorLength() => Mathf.Sqrt((x* x) + (y* y));

    public float GetVectorLengthSquared() => (x * x) + (y * y);

    public static MyVector2 AddVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorSum = new(0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y
        };
        return vectorSum;
    }

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs) => AddVector(lhs, rhs);

    public static MyVector2 SubtractVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorDifference = new(0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y
        };
        return vectorDifference;
    }

    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs) => SubtractVector(lhs, rhs);

    public static MyVector2 MultiplyVector(MyVector2 vector, float multiplier)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x *= multiplier; returnVector.y *= multiplier;
        return returnVector;
    }

    public static MyVector2 operator *(MyVector2 lhs, float rhs) => MultiplyVector(lhs, rhs);

    public static MyVector2 DivideVector(MyVector2 vector, float divisor)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x /= divisor; returnVector.y /= divisor;
        return returnVector;
    }

    public static MyVector2 operator /(MyVector2 lhs, float rhs) => DivideVector(lhs, rhs);

    public static bool CheckIfIdentical(MyVector2 vectorA, MyVector2 vectorB)
    {
        if (vectorA.x == vectorB.x && vectorA.y == vectorB.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator ==(MyVector2 lhs, MyVector2 rhs) => CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector2 lhs, MyVector2 rhs) => !CheckIfIdentical(lhs, rhs);

    public MyVector2 NormaliseVector()
    {
        MyVector2 returnVector = new(x, y);
        returnVector /= GetVectorLength();
        return returnVector;
    }

    public static float GetDotProduct(MyVector2 vectorA, MyVector2 vectorB, bool shouldNormalise = true)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.NormaliseVector();
            vectorB = vectorB.NormaliseVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y));

        return dotProduct;
    }

    public static MyVector2 GetLerp(MyVector2 vectorA, MyVector2 vectorB, float t)
    {
        MyVector2 returnVector;
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }
}