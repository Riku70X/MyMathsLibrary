using UnityEngine;

public class MyVector3
{
    // Workshop 1

    public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static MyVector3 AddVector(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorSum = new MyVector3(0, 0, 0);
        vectorSum.x = vectorA.x + vectorB.x;
        vectorSum.y = vectorA.y + vectorB.y;
        vectorSum.z = vectorA.z + vectorB.z;
        return vectorSum;
    }

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    {
        return AddVector(lhs, rhs);
    }

    public static MyVector3 SubtractVector(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorDifference = new MyVector3(0, 0, 0);
        vectorDifference.x = vectorA.x - vectorB.x;
        vectorDifference.y = vectorA.y - vectorB.y;
        vectorDifference.z = vectorA.z - vectorB.z;
        return vectorDifference;
    }

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return SubtractVector(lhs, rhs);
    }

    public float GetVectorLength()
    {
        float length = Mathf.Sqrt((x * x) + (y * y) + (z * z));
        return length;
    }

    public Vector3 ConvertToUnityVector()
    {
        Vector3 returnVector = new Vector3(x, y, z);
        return returnVector;
    }

    public static MyVector3 ConvertToCustomVector(Vector3 vector)
    {
        MyVector3 returnVector = new MyVector3(vector.x, vector.y, vector.z);
        return returnVector;
    }

    // Workshop 2

    public float GetVectorLengthSquared()
    {
        float length = (x * x) + (y * y) + (z * z);
        return length;
    }

    public static MyVector3 MultiplyVector(MyVector3 vector, float multiplier)
    {
        MyVector3 returnVector = new MyVector3(vector.x, vector.y, vector.z);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier;
        return returnVector;
    }

    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return MultiplyVector(lhs, rhs);
    }

    public static MyVector3 DivideVector(MyVector3 vector, float divisor)
    {
        MyVector3 returnVector = new MyVector3(vector.x, vector.y, vector.z);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor;
        return returnVector;
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

    public MyVector3 NormaliseVector()
    {
        MyVector3 returnVector = new MyVector3(x, y, z);
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
}