using UnityEngine;

public class MyMathsLibrary
{
    public static float GetVector2Angle(MyVector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static MyVector2 GetVector2Direction(float angle)
    {
        MyVector2 direction = new(0, 0)
        {
            x = Mathf.Cos(angle),
            y = Mathf.Sin(angle)
        };
        return direction;
    }

    public static MyVector3 ConvertEulerToDirection(MyVector3 euler)
    {
        MyVector3 direction = new(0, 0, 0)
        {
            x = Mathf.Cos(euler.y) * Mathf.Cos(euler.x),
            y = Mathf.Sin(euler.x),
            z = Mathf.Cos(euler.x) * Mathf.Sin(euler.y)
        };
        return direction;
    }

    public static MyVector3 GetCrossProduct(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorC = new(0, 0, 0)
        {
            x = (vectorA.y * vectorB.z) - (vectorA.z * vectorB.y),
            y = (vectorA.z * vectorB.x) - (vectorA.x * vectorB.z),
            z = (vectorA.x * vectorB.y) - (vectorA.y * vectorB.x)
        };
        return vectorC;
    }

    public static MyVector3 RotateVertexAroundAxis(MyVector3 vertex, MyVector3 axis, float angle)
    {
        // The Rodrigues Rotation Formula, ensure angle is in radians
        MyVector3 returnVertex = (vertex * Mathf.Cos(angle)) +
                                 (axis * (1 - Mathf.Cos(angle)) * MyVector3.GetDotProduct(vertex, axis, false)) +
                                 (GetCrossProduct(axis, vertex) * Mathf.Sin(angle));
        return returnVertex;
    }
}