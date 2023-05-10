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

    public static MyVector2 zero => new(0, 0);

    public static MyVector2 one => new(1, 1);

    public static MyVector2 right => new(1, 0);

    public static MyVector2 up => new(0, 1);

    public static MyVector2 left => new(-1, 0);

    public static MyVector2 down => new(0, -1);

    public override string ToString() => $"({x}, {y})";

    public static implicit operator Vector2(MyVector2 vector) => new(vector.x, vector.y);

    public static implicit operator MyVector2(Vector2 vector) => new(vector.x, vector.y);

    public static implicit operator MyVector3(MyVector2 vector) => new(vector.x, vector.y, 1);

    public static implicit operator MyVector4(MyVector2 vector) => new(vector.x, vector.y, 1, 1);

    public static MyVector2 operator -(MyVector2 vector) => MyMathsLibrary.GetNegativeVector(vector);

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs) => MyMathsLibrary.AddVector(lhs, rhs);

    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs) => MyMathsLibrary.SubtractVector(lhs, rhs);

    public static MyVector2 operator *(MyVector2 lhs, float rhs) => MyMathsLibrary.MultiplyVector(lhs, rhs);

    public static MyVector2 operator /(MyVector2 lhs, float rhs) => MyMathsLibrary.DivideVector(lhs, rhs);

    public static bool operator ==(MyVector2 lhs, MyVector2 rhs) => MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public static bool operator !=(MyVector2 lhs, MyVector2 rhs) => !MyMathsLibrary.CheckIfIdentical(lhs, rhs);

    public float GetVectorLength() => Mathf.Sqrt((x* x) + (y* y));

    public float GetVectorLengthSquared() => (x * x) + (y * y);

    public MyVector2 NormaliseVector()
    {
        MyVector2 returnVector = new(x, y);
        returnVector /= GetVectorLength();
        return returnVector;
    }

    public float GetAngle() => Mathf.Atan2(y, x);
}