using UnityEngine;

public class MathsLibrary
{
    public static float GetVector2Angle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static Vector2 GetVector2Direction(float angle)
    {
        Vector2 direction = Vector2.zero;
        direction.x = Mathf.Cos(angle);
        direction.y = Mathf.Sin(angle);
        return direction;
    }

    public static MyVector3 ConvertEulerToDirection(MyVector3 euler)
    {
        MyVector3 direction = new MyVector3(0, 0, 0);
        direction.x = Mathf.Cos(euler.y) * Mathf.Cos(euler.x);
        direction.y = Mathf.Sin(euler.x);
        direction.z = Mathf.Cos(euler.x) * Mathf.Sin(euler.y);
        return direction;
    }

    public static MyVector3 GetCrossProduct(MyVector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorC = new MyVector3(0, 0, 0);
        vectorC.x = (vectorA.y * vectorB.z) - (vectorA.z * vectorB.y);
        vectorC.y = (vectorA.z * vectorB.x) - (vectorA.x * vectorB.z);
        vectorC.z = (vectorA.x * vectorB.y) - (vectorA.y * vectorB.x);
        return vectorC;
    }
}