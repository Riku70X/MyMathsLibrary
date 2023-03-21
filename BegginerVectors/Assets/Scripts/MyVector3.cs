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

    public static MyVector3 zero => new(0, 0, 0);

    public static MyVector3 one => new(1, 1, 1);

    public static MyVector3 right => new(1, 0, 0);

    public static MyVector3 up => new(0, 1, 0);

    public static MyVector3 forward => new(0, 0, 1);

    public override string ToString() => $"({x}, {y}, {z})";

    public static implicit operator Vector3(MyVector3 vector) => new(vector.x, vector.y, vector.z);

    public static implicit operator MyVector3(Vector3 vector) => new(vector.x, vector.y, vector.z);

    public static implicit operator MyVector2(MyVector3 vector) => new(vector.x, vector.y);

    public static implicit operator MyVector4(MyVector3 vector) => new(vector.x, vector.y, vector.z, 1);

    public static MyVector3 operator -(MyVector3 vector) => MyMathsLibrary.GetNegativeVector(vector);

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs) => MyMathsLibrary.AddVector(lhs, rhs);

    public static MyVector3 operator +(Vector3 lhs, MyVector3 rhs) => MyMathsLibrary.AddVector(lhs, rhs);

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs) => MyMathsLibrary.SubtractVector(lhs, rhs);

    public static MyVector3 operator -(Vector3 lhs, MyVector3 rhs) => MyMathsLibrary.SubtractVector(lhs, rhs);

    public static MyVector3 operator *(MyVector3 lhs, float rhs) => MyMathsLibrary.MultiplyVector(lhs, rhs);

    public static MyVector3 operator *(float lhs, MyVector3 rhs) => MyMathsLibrary.MultiplyVector(rhs, lhs);

    public static MyVector3 operator /(MyVector3 lhs, float rhs) => MyMathsLibrary.DivideVector(lhs, rhs);

    public static MyVector3 operator /(float lhs, MyVector3 rhs) => MyMathsLibrary.DivideVector(rhs, lhs);

    public static bool operator ==(MyVector3 lhs, MyVector3 rhs) => MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector3 lhs, MyVector3 rhs) => !MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public float GetVectorLength() => Mathf.Sqrt((x * x) + (y * y) + (z * z));

    public float GetVectorLengthSquared() => (x * x) + (y * y) + (z * z);

    public MyVector3 GetNormalisedVector()
    {
        MyVector3 returnVector = new(x, y, z);
        returnVector /= GetVectorLength();
        return returnVector;
    }

    public MyVector3 ConvertEulerToDirection()
    {
        MyVector3 direction = new(0, 0, 0)
        {
            x = Mathf.Cos(x) * Mathf.Sin(y),
            y = -Mathf.Sin(x),
            z = Mathf.Cos(y) * Mathf.Cos(x)
        };
        return direction;
    }

    public MyQuat ConvertEulerToQuaternion()
    {
        // Euler to Quaternion code adapted from code on these two sites:
        // https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
        // https://math.stackexchange.com/questions/2975109/how-to-convert-euler-angles-to-quaternions-and-get-the-same-euler-angles-back-fr

        float sp = Mathf.Sin(y * 0.5f);
        float cp = Mathf.Cos(y * 0.5f);
        float sy = Mathf.Sin(z * 0.5f);
        float cy = Mathf.Cos(z * 0.5f);
        float sr = Mathf.Sin(x * 0.5f);
        float cr = Mathf.Cos(x * 0.5f);

        MyQuat returnQuat = new(0, 0, 0, 0);
        {
            returnQuat.w = cr * cp * cy + sr * sp * sy;
            returnQuat.vectorComponent.x = sr * cp * cy - cr * sp * sy;
            returnQuat.vectorComponent.y = cr * sp * cy + sr * cp * sy;
            returnQuat.vectorComponent.z = cr * cp * sy - sr * sp * cy;
        }

        return returnQuat;
    }
}