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

    public static MyVector2 zero
    { get { return new MyVector2(0, 0); } }

    public static MyVector2 one
    { get { return new MyVector2(1, 1); } }

    public static MyVector2 right
    { get { return new MyVector2(1, 0); } }

    public static MyVector2 up
    { get { return new MyVector2(0, 1); } }

    public string ToString()
    {
        return ($"({x}, {y})");
    }

    public Vector2 ConvertToUnityVector()
    {
        Vector2 returnVector = new(x, y);
        return returnVector;
    }

    public static Vector2[] ConvertToUnityVectorArray(MyVector2[] vectorArray)
    {
        Vector2[] returnVectorArray = new Vector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i].ConvertToUnityVector();
        }
        return returnVectorArray;
    }

    public static MyVector2 ConvertToCustomVector(Vector2 vector)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        return returnVector;
    }

    public static MyVector2[] ConvertToCustomVectorArray(Vector2[] vectorArray)
    {
        MyVector2[] returnVectorArray = new MyVector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = ConvertToCustomVector(vectorArray[i]);
        }
        return returnVectorArray;
    }

    public MyVector3 ConvertToMyVector3()
    {
        return new MyVector3(x, y, 1);
    }

    public MyVector4 ConvertToMyVector4()
    {
        return new MyVector4(x, y, 1, 1);
    }

    public float GetVectorLength()
    {
        float length = Mathf.Sqrt((x * x) + (y * y));
        return length;
    }

    public float GetVectorLengthSquared()
    {
        float length = (x * x) + (y * y);
        return length;
    }

    public static MyVector2 AddVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorSum = new(0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y
        };
        return vectorSum;
    }

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs)
    {
        return AddVector(lhs, rhs);
    }

    public static MyVector2 SubtractVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorDifference = new(0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y
        };
        return vectorDifference;
    }

    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs)
    {
        return SubtractVector(lhs, rhs);
    }
    
    public static MyVector2 MultiplyVector(MyVector2 vector, float multiplier)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x *= multiplier; returnVector.y *= multiplier;
        return returnVector;
    }

    public static MyVector2 operator *(MyVector2 lhs, float rhs)
    {
        return MultiplyVector(lhs, rhs);
    }

    public static MyVector2 DivideVector(MyVector2 vector, float divisor)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x /= divisor; returnVector.y /= divisor;
        return returnVector;
    }

    public static MyVector2 operator /(MyVector2 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

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

    public static bool operator ==(MyVector2 lhs, MyVector2 rhs)
    {
        return CheckIfIdentical(lhs, rhs);
    }

    public static bool operator !=(MyVector2 lhs, MyVector2 rhs)
    {
        return !CheckIfIdentical(lhs, rhs);
    }

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
}
