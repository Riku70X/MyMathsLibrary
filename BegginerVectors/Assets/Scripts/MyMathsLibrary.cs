using UnityEngine;

public class MyMathsLibrary
{
    public static float GetVector2Angle(MyVector2 vector) => Mathf.Atan2(vector.y, vector.x);

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
            x = Mathf.Cos(euler.x) * Mathf.Sin(euler.y),
            y = Mathf.Sin(euler.x),
            z = Mathf.Cos(euler.y) * Mathf.Cos(euler.x)
        };
        return direction;
    }

    public static MyQuat ConvertEulerToQuaternion(MyVector3 euler)
    {
        float sp = Mathf.Sin(euler.x * 0.5f);
        float cp = Mathf.Cos(euler.x * 0.5f);
        float sy = Mathf.Sin(euler.y * 0.5f);
        float cy = Mathf.Cos(euler.y * 0.5f);
        float sr = Mathf.Sin(euler.z * 0.5f);
        float cr = Mathf.Cos(euler.z * 0.5f);

        MyQuat returnQuat = new(0, 0, 0, 0);
        {
            returnQuat.w = cr * cp * cy + sr * sp * sy;
            returnQuat.vectorComponent.x = cr * sp * cy - sr * cp * sy;
            returnQuat.vectorComponent.y = cr * cp * sy - sr * sp * cy;
            returnQuat.vectorComponent.z = sr * cp * cy - cr * sp * sy;
        }

        return returnQuat;
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
        axis = axis.GetNormalisedVector();
        // The Rodrigues Rotation Formula, ensure angle is in radians
        MyVector3 returnVertex = new((vertex * Mathf.Cos(angle)) +
                                 (axis * (1 - Mathf.Cos(angle)) * MyVector3.GetDotProduct(vertex, axis)) +
                                 (GetCrossProduct(axis, vertex) * Mathf.Sin(angle)));
        return returnVertex;
    }
}