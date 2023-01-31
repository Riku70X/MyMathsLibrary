using UnityEngine;

public class MathsLibrary
{
    public static float GetVector2Angle(MyVector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static MyVector2 GetVector2Direction(float angle)
    {
        MyVector2 direction = new MyVector2(0, 0);
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

    public static MyVector2 GetLerp(MyVector2 vectorA, MyVector2 vectorB, float t)
    {
        MyVector2 returnVector = new MyVector2(0, 0);
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }

    public static MyVector3 GetLerp(MyVector3 vectorA, MyVector3 vectorB, float t)
    {
        MyVector3 returnVector = new MyVector3(0, 0, 0);
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }
}