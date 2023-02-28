using UnityEngine;

public class MyMathsLibrary
{
    #region Static Vector2 functions

    public static MyVector2 GetNegativeVector(MyVector2 vector) => new MyVector2(-vector.x, -vector.y);

    public static MyVector2 AddVector(MyVector2 vectorA, MyVector2 vectorB)
    {
        MyVector2 vectorSum = new(0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y
        };
        return vectorSum;
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

    public static MyVector2 MultiplyVector(MyVector2 vector, float multiplier)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x *= multiplier; returnVector.y *= multiplier;
        return returnVector;
    }

    public static MyVector2 DivideVector(MyVector2 vector, float divisor)
    {
        MyVector2 returnVector = new(vector.x, vector.y);
        returnVector.x /= divisor; returnVector.y /= divisor;
        return returnVector;
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

    public static float GetDotProduct(MyVector2 vectorA, MyVector2 vectorB, bool shouldNormalise = true)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.NormaliseVector();
            vectorB = vectorB.NormaliseVector();
        }

        float dotProduct = (vectorA.x * vectorB.x) + (vectorA.y * vectorB.y);

        return dotProduct;
    }

    public static MyVector2 GetLerp(MyVector2 vectorA, MyVector2 vectorB, float t)
    {
        MyVector2 returnVector;
        vectorA *= 1 - t;
        vectorB *= t;
        returnVector = vectorA + vectorB;
        return returnVector;
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

    #endregion // Static Vector2 functions

    #region Static Vector3 functions

    public static MyVector3 GetNegativeVector(MyVector3 vector) => new MyVector3(-vector.x, -vector.y, -vector.z);

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

    public static MyVector3 AddVector(Vector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorSum = new(0, 0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y,
            z = vectorA.z + vectorB.z
        };
        return vectorSum;
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

    public static MyVector3 SubtractVector(Vector3 vectorA, MyVector3 vectorB)
    {
        MyVector3 vectorDifference = new(0, 0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y,
            z = vectorA.z - vectorB.z
        };
        return vectorDifference;
    }

    public static MyVector3 MultiplyVector(MyVector3 vector, float multiplier)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier;
        return returnVector;
    }

    public static MyVector3 DivideVector(MyVector3 vector, float divisor)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor;
        return returnVector;
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

    public static MyVector3 GetLerp(MyVector3 vectorA, MyVector3 vectorB, float t)
    {
        MyVector3 returnVector;
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }

    public static float GetDotProduct(MyVector3 vectorA, MyVector3 vectorB, bool shouldNormalise = true)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.GetNormalisedVector();
            vectorB = vectorB.GetNormalisedVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z));

        return dotProduct;
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
                                 (axis * (1 - Mathf.Cos(angle)) * GetDotProduct(vertex, axis)) +
                                 (GetCrossProduct(axis, vertex) * Mathf.Sin(angle)));
        return returnVertex;
    }

    #endregion // Static Vector 3 functions


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

}