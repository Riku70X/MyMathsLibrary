using System;
using UnityEngine;

[Serializable]
public class MyVector3
{
    [SerializeField] public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static MyVector3 zero
    { get { return new MyVector3(0, 0, 0); } }

    public static MyVector3 one
    { get { return new MyVector3(1, 1, 1); } }

    public static MyVector3 right
    { get { return new MyVector3(1, 0, 0); } }

    public static MyVector3 up
    { get { return new MyVector3(0, 1, 0); } }

    public static MyVector3 forward
    { get { return new MyVector3(0, 0, 1); } }

    public Vector3 ConvertToUnityVector()
    {
        return new Vector3(x, y, z);
    }

    public static Vector3[] ConvertToUnityVectorArray(MyVector3[] vectorArray)
    {
        Vector3[] returnVectorArray = new Vector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i].ConvertToUnityVector();
        }
        return returnVectorArray;
    }

    public static MyVector3 ConvertToCustomVector(Vector3 vector)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        return returnVector;
    }

    public static MyVector3[] ConvertToCustomVectorArray(Vector3[] vectorArray)
    {
        MyVector3[] returnVectorArray = new MyVector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = ConvertToCustomVector(vectorArray[i]);
        }
        return returnVectorArray;
    }

    public MyVector4 ConvertToMyVector4()
    {
        return new MyVector4(x, y, z, 1);
    }

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

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    {
        return AddVector(lhs, rhs);
    }

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

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return SubtractVector(lhs, rhs);
    }

    public float GetVectorLength()
    {
        float length = Mathf.Sqrt((x * x) + (y * y) + (z * z));
        return length;
    }

    public float GetVectorLengthSquared()
    {
        float length = (x * x) + (y * y) + (z * z);
        return length;
    }

    public static MyVector3 MultiplyVector(MyVector3 vector, float multiplier)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier;
        return returnVector;
    }

    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return MultiplyVector(lhs, rhs);
    }

    public static MyVector3 DivideVector(MyVector3 vector, float divisor)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor;
        return returnVector;
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs)
    {
        return DivideVector(lhs, rhs);
    }

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

    public static bool operator ==(MyVector3 lhs, MyVector3 rhs)
    {
        return CheckIfIdentical(lhs, rhs);
    }

    public static bool operator !=(MyVector3 lhs, MyVector3 rhs)
    {
        return !CheckIfIdentical(lhs, rhs);
    }

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
}