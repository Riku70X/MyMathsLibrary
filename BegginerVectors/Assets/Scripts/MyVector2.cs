using UnityEngine;

public class MyVector2
{
    public float x, y, z;

    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static MyVector2 AddVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorSum = new MyVector2(0, 0);
        vectorSum.x = vectorA.x + vectorB.x;
        vectorSum.y = vectorA.y + vectorB.y;
        return vectorSum;
    }

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs)
    {
        return AddVector(lhs, rhs);
    }

    public static MyVector2 SubtractVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorDifference = new MyVector2(0, 0);
        vectorDifference.x = vectorA.x - vectorB.x;
        vectorDifference.y = vectorA.y - vectorB.y;
        return vectorDifference;
    }

    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs)
    {
        return SubtractVector(lhs, rhs);
    }

    public float GetVectorLength()
    {
        float length = Mathf.Sqrt((x * x) + (y * y));
        return length;
    }

    public Vector2 ConvertToUnityVector()
    {
        Vector2 returnVector = new Vector2(x, y);
        return returnVector;
    }

    public static MyVector2 ConvertToCustomVector(Vector2 vector)
    {
        MyVector2 returnVector = new MyVector2(vector.x, vector.y);
        return returnVector;
    }

    public float GetVectorLengthSquared()
    {
        float length = (x * x) + (y * y);
        return length;
    }

    public static MyVector2 MultiplyVector(MyVector2 vector, float multiplier)
    {
        MyVector2 returnVector = new MyVector2(vector.x, vector.y);
        returnVector.x *= multiplier; returnVector.y *= multiplier;
        return returnVector;
    }

    public static MyVector2 operator *(MyVector2 lhs, float rhs)
    {
        return MultiplyVector(lhs, rhs);
    }

    public static MyVector2 DivideVector(MyVector2 vector, float divisor)
    {
        MyVector2 returnVector = new MyVector2(vector.x, vector.y);
        returnVector.x /= divisor; returnVector.y /= divisor;
        return returnVector;
    }

    public static MyVector2 operator /(MyVector2 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

    public MyVector2 NormaliseVector()
    {
        MyVector2 returnVector = new MyVector2(x, y);
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