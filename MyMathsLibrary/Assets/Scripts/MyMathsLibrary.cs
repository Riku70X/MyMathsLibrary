using UnityEngine;

public class MyMathsLibrary
{
    #region Static Vector2 functions

    public static Vector2[] ConvertToUnityVectorArray(MyVector2[] vectorArray)
    {
        Vector2[] returnVectorArray = new Vector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector2[] ConvertToCustomVectorArray(Vector2[] vectorArray)
    {
        MyVector2[] returnVectorArray = new MyVector2[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector2 GetNegativeVector(MyVector2 vector) => new(-vector.x, -vector.y);

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

    public static float GetDotProduct(MyVector2 vectorA, MyVector2 vectorB, bool shouldNormalise = false)
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

    public static Vector3[] ConvertToUnityVectorArray(MyVector3[] vectorArray)
    {
        Vector3[] returnVectorArray = new Vector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector3[] ConvertToCustomVectorArray(Vector3[] vectorArray)
    {
        MyVector3[] returnVectorArray = new MyVector3[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector3 GetNegativeVector(MyVector3 vector) => new(-vector.x, -vector.y, -vector.z);

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

    public static MyVector3 ReciprocalVector(float number, MyVector3 vector)
    {
        MyVector3 returnVector = new(vector.x, vector.y, vector.z);

        returnVector.x = returnVector.x == 0 ? returnVector.x = 0 : returnVector.x = number / returnVector.x;
        returnVector.y = returnVector.y == 0 ? returnVector.y = 0 : returnVector.y = number / returnVector.y;
        returnVector.z = returnVector.z == 0 ? returnVector.z = 0 : returnVector.z = number / returnVector.z;

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

    public static float GetDotProduct(MyVector3 vectorA, MyVector3 vectorB, bool shouldNormalise = false)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.GetNormalisedVector();
            vectorB = vectorB.GetNormalisedVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z));

        return dotProduct;
    }

    public static MyVector3 GetCrossProduct(MyVector3 vectorA, MyVector3 vectorB, bool shouldNormalise = false)
    {
        MyVector3 vectorC = new(0, 0, 0)
        {
            x = (vectorA.y * vectorB.z) - (vectorA.z * vectorB.y),
            y = (vectorA.z * vectorB.x) - (vectorA.x * vectorB.z),
            z = (vectorA.x * vectorB.y) - (vectorA.y * vectorB.x)
        };

        if (shouldNormalise) return vectorC.GetNormalisedVector();
        return vectorC;
    }

    public static MyVector3 RotateVertexAroundAxis(MyVector3 vertex, MyVector3 axis, float angle)
    {
        axis = axis.GetNormalisedVector();

        // The Rodrigues Rotation Formula, ensure angle is in radians
        MyVector3 returnVertex = (vertex * Mathf.Cos(angle)) +
                                 (axis * (1 - Mathf.Cos(angle)) * GetDotProduct(vertex, axis)) +
                                 (GetCrossProduct(axis, vertex) * Mathf.Sin(angle));
        return returnVertex;
    }

    public static float GetShortestDistanceSq(MyVector3 A, MyVector3 B, MyVector3 point)
    {
        // gets the shortest distance squared from the vector AB to the point

        MyVector3 AB = B - A;
        MyVector3 AToPoint = point - A;
        MyVector3 BToPoint = point - B;

        float dotProduct = GetDotProduct(AToPoint, AB);

        if (GetDotProduct(AB, AToPoint) <= 0)
        {
            return AToPoint.GetVectorLengthSquared();
        }
        else if (GetDotProduct(-AB, BToPoint) <= 0)
        {
            return BToPoint.GetVectorLengthSquared();
        }
        else
        {
            return AToPoint.GetVectorLengthSquared() - dotProduct * dotProduct / AB.GetVectorLengthSquared();
        }
    }

    public static MyVector3 GetClosestPointOnLineSegment(MyVector3 point, MyVector3 A, MyVector3 B)
    {
        // code taken from https://wickedengine.net/2020/04/26/capsule-collision-detection/

        MyVector3 AB = B - A;
        float t = GetDotProduct(point - A, AB) / GetDotProduct(AB, AB);
        return A + Mathf.Clamp(t, 0, 1) * AB;
    }

    #region Transforming Vectors

    public static MyVector3 ScaleVector(MyVector3 vector3, MyVector3 scalar)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;

        MyMatrix4x4 scaleMatrix = GetScaleMatrix(scalar);

        returnVector4 = scaleMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 RotateVector(MyVector3 vector3, float pitch, float yaw, float roll)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;

        returnVector4 = GetRotationMatrix(new MyVector3(pitch, yaw, roll)) * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 RotateVectorUsingQuat(MyVector3 vertex, MyQuat rotationQuat)
    {
        MyQuat startVertexQuat = new(vertex);

        MyQuat halfRotatedVertexQuat = rotationQuat * startVertexQuat;

        MyQuat fullRotatedVertexQuat = halfRotatedVertexQuat * rotationQuat.GetInverse();

        return fullRotatedVertexQuat.GetAxis();
    }

    public static MyVector3 TranslateVector(MyVector3 vector3, MyVector3 translation)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;

        MyMatrix4x4 translateMatrix = GetTranslationMatrix(translation);

        returnVector4 = translateMatrix * vector4;

        returnVector3 = new MyVector3(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    public static MyVector3 TransformVector(MyVector3 vector3, MyVector3 scalar, MyVector3 rotation, MyVector3 translation)
    {
        MyVector3 returnVector3;

        MyVector4 vector4 = new(vector3.x, vector3.y, vector3.z, 1);
        MyVector4 returnVector4;

        MyMatrix4x4 transformMatrix = GetTransformationMatrix(scalar, rotation, translation);

        returnVector4 = transformMatrix * vector4;

        returnVector3 = new(returnVector4.x, returnVector4.y, returnVector4.z);

        return returnVector3;
    }

    #endregion // Transforming Vectors

    #endregion // Static Vector 3 functions

    #region Static Vector4 functions

    public static Vector4[] ConvertToUnityVectorArray(MyVector4[] vectorArray)
    {
        Vector4[] returnVectorArray = new Vector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector4[] ConvertToCustomVectorArray(Vector4[] vectorArray)
    {
        MyVector4[] returnVectorArray = new MyVector4[vectorArray.Length];
        for (int i = 0; i < vectorArray.Length; i++)
        {
            returnVectorArray[i] = vectorArray[i];
        }
        return returnVectorArray;
    }

    public static MyVector4 GetNegativeVector(MyVector4 vector) => new(-vector.x, -vector.y, -vector.z, -vector.w);

    public static MyVector4 AddVector(MyVector4 vectorA, MyVector4 vectorB)
    {
        MyVector4 vectorSum = new(0, 0, 0, 0)
        {
            x = vectorA.x + vectorB.x,
            y = vectorA.y + vectorB.y,
            z = vectorA.z + vectorB.z,
            w = vectorA.w + vectorB.w
        };
        return vectorSum;
    }
    public static MyVector4 SubtractVector(MyVector4 vectorA, MyVector4 vectorB)
    {
        MyVector4 vectorDifference = new(0, 0, 0, 0)
        {
            x = vectorA.x - vectorB.x,
            y = vectorA.y - vectorB.y,
            z = vectorA.z - vectorB.z,
            w = vectorA.w - vectorB.w
        };
        return vectorDifference;
    }

    public static MyVector4 MultiplyVector(MyVector4 vector, float multiplier)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x *= multiplier; returnVector.y *= multiplier; returnVector.z *= multiplier; returnVector.w *= multiplier;
        return returnVector;
    }

    public static MyVector4 DivideVector(MyVector4 vector, float divisor)
    {
        MyVector4 returnVector = new(vector.x, vector.y, vector.z, vector.w);
        returnVector.x /= divisor; returnVector.y /= divisor; returnVector.z /= divisor; returnVector.w /= divisor;
        return returnVector;
    }

    public static bool CheckIfIdentical(MyVector4 vectorA, MyVector4 vectorB)
    {
        if (vectorA.x == vectorB.x && vectorA.y == vectorB.y && vectorA.z == vectorB.z && vectorA.w == vectorB.w)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float GetDotProduct(MyVector4 vectorA, MyVector4 vectorB, bool shouldNormalise = false)
    {
        if (shouldNormalise)
        {
            vectorA = vectorA.NormaliseVector();
            vectorB = vectorB.NormaliseVector();
        }

        float dotProduct = ((vectorA.x * vectorB.x) + (vectorA.y * vectorB.y) + (vectorA.z * vectorB.z) + (vectorA.w * vectorB.w));

        return dotProduct;
    }

    public static MyVector4 GetLerp(MyVector4 vectorA, MyVector4 vectorB, float t)
    {
        MyVector4 returnVector;
        vectorA *= (1 - t);
        vectorB *= (t);
        returnVector = vectorA + vectorB;
        return returnVector;
    }

    #endregion // Static Vector4 functions

    #region Static Matrix4x4 functions

    public static MyVector4 MultiplyMatrices4x4by4x1(MyMatrix4x4 matrix, MyVector4 vector)
    {
        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            x = matrix.values[0, 0] * vector.x + matrix.values[0, 1] * vector.y + matrix.values[0, 2] * vector.z + matrix.values[0, 3] * vector.w,
            y = matrix.values[1, 0] * vector.x + matrix.values[1, 1] * vector.y + matrix.values[1, 2] * vector.z + matrix.values[1, 3] * vector.w,
            z = matrix.values[2, 0] * vector.x + matrix.values[2, 1] * vector.y + matrix.values[2, 2] * vector.z + matrix.values[2, 3] * vector.w,
            w = matrix.values[3, 0] * vector.x + matrix.values[3, 1] * vector.y + matrix.values[3, 2] * vector.z + matrix.values[3, 3] * vector.w
        };

        return returnVector;
    }

    public static MyMatrix4x4 MultiplyMatrices4x4by4x4(MyMatrix4x4 matrixA, MyMatrix4x4 matrixB)
    {
        MyMatrix4x4 returnMatrix = MyMatrix4x4.identity;

        for (int row = 0; row < 4; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                returnMatrix.values[row, column] = GetDotProduct(matrixA.GetRow(row), matrixB.GetColumn(column));
            }
        }

        return returnMatrix;
    }

    #region Get Transform Matrix functions

    public static MyMatrix4x4 GetScaleMatrix(MyVector3 scalar) => new(new MyVector3(scalar.x, 0, 0), new MyVector3(0, scalar.y, 0), new MyVector3(0, 0, scalar.z), new MyVector3(0, 0, 0));

    public static MyMatrix4x4 GetRotationMatrix(MyVector3 eulerAngles)
    {
        MyMatrix4x4 rollMatrix = new(new MyVector3(Mathf.Cos(eulerAngles.z), Mathf.Sin(eulerAngles.z), 0), new MyVector3(-Mathf.Sin(eulerAngles.z), Mathf.Cos(eulerAngles.z), 0), new MyVector3(0, 0, 1), new MyVector3(0, 0, 0));

        MyMatrix4x4 yawMatrix = new(new MyVector3(Mathf.Cos(eulerAngles.y), 0, -Mathf.Sin(eulerAngles.y)), new MyVector3(0, 1, 0), new MyVector3(Mathf.Sin(eulerAngles.y), 0, Mathf.Cos(eulerAngles.y)), new MyVector3(0, 0, 0));

        MyMatrix4x4 pitchMatrix = new(new MyVector3(1, 0, 0), new MyVector3(0, Mathf.Cos(eulerAngles.x), Mathf.Sin(eulerAngles.x)), new MyVector3(0, -Mathf.Sin(eulerAngles.x), Mathf.Cos(eulerAngles.x)), new MyVector3(0, 0, 0));

        return pitchMatrix * (yawMatrix * rollMatrix);
    }

    public static MyMatrix4x4 GetRotationMatrixUsingQuat(MyQuat rotation)
    {
        MyMatrix4x4 matrix = rotation.ConvertToRotationMatrix();
        return matrix;
    }

    public static MyMatrix4x4 GetTranslationMatrix(MyVector3 translation) => new(new MyVector3(1, 0, 0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1), new MyVector3(translation.x, translation.y, translation.z));

    public static MyMatrix4x4 GetTransformationMatrix(MyVector3 scalar, MyVector3 rotation, MyVector3 translation)
    {
        MyMatrix4x4 scaleMatrix = GetScaleMatrix(scalar);
        MyMatrix4x4 rotationMatrix = GetRotationMatrix(rotation);
        MyMatrix4x4 translationMatrix = GetTranslationMatrix(translation);

        return translationMatrix * (rotationMatrix * scaleMatrix);
    }

    public static MyMatrix4x4 GetTransformationMatrixUsingQuat(MyVector3 scalar, MyQuat rotation, MyVector3 translation)
    {
        MyMatrix4x4 scaleMatrix = GetScaleMatrix(scalar);
        MyMatrix4x4 rotationMatrix = rotation.ConvertToRotationMatrix();
        MyMatrix4x4 translationMatrix = GetTranslationMatrix(translation);

        return translationMatrix * (rotationMatrix * scaleMatrix);
    }

    public static MyMatrix4x4 GetInverseTransformationMatrix(MyVector3 scalar, MyVector3 rotation, MyVector3 translation)
    {
        MyMatrix4x4 inverseScaleMatrix = GetScaleMatrix(scalar).GetScaleInverse();
        MyMatrix4x4 inverseRotationMatrix = GetRotationMatrix(rotation).GetRotationInverse();
        MyMatrix4x4 inverseTranslationMatrix = GetTranslationMatrix(translation).GetTranslationInverse();

        return inverseScaleMatrix * (inverseRotationMatrix * inverseTranslationMatrix);
    }

    #endregion // Get Transform Matrix functions

    #endregion // Static Matrix4x4 functions

    #region Static Quaternion functions

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new(0, 0, 0, 0)
        {
            w = (quatA.w * quatB.w) - GetDotProduct(quatA.vectorComponent, quatB.vectorComponent),

            vectorComponent = (quatA.vectorComponent * quatB.w) + (quatB.vectorComponent * quatA.w) +
                                    GetCrossProduct(quatA.vectorComponent, quatB.vectorComponent)
        };

        return returnQuat;
    }

    public static MyQuat SLERP(MyQuat quatA, MyQuat quatB, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        MyQuat slerpQuart = quatB * quatA.GetInverse();
        MyVector4 axisAngle = slerpQuart.GetAxisAngle();
        MyQuat slerpQuartT = new(axisAngle.w * t, new MyVector3(axisAngle.x, axisAngle.y, axisAngle.z));

        return slerpQuartT * quatA;
    }

    #endregion // Static Quaternion functions

    #region Static Bounding Intersect functions

    public static bool AxisIntersectsAABB(MyVector3 axis, MyAABBCollider box, MyVector3 startPoint, MyVector3 endPoint, ref float lowest, ref float highest)
    {
        // Calculate our Minimum and Maximum based on the current axis
        float minimum = 0.0f, maximum = 1.0f;
        if (axis == MyVector3.right)
        {
            minimum = (box.left - startPoint.x) / (endPoint.x - startPoint.x);
            maximum = (box.right - startPoint.x) / (endPoint.x - startPoint.x);
        }
        else if (axis == MyVector3.up)
        {
            minimum = (box.bottom - startPoint.y) / (endPoint.y - startPoint.y);
            maximum = (box.top - startPoint.y) / (endPoint.y - startPoint.y);
        }
        else if (axis == MyVector3.forward)
        {
            minimum = (box.back - startPoint.z) / (endPoint.z - startPoint.z);
            maximum = (box.front - startPoint.z) / (endPoint.z - startPoint.z);
        }

        if (maximum < minimum)
        {
            // Swapping values
            (minimum, maximum) = (maximum, minimum);
        }

        // Eliminate non-intersections early
        if (maximum < lowest)
            return false;

        if (minimum > highest)
            return false;

        // Check to ensure min and max are between 0-1, otherwise the optimisation would actually INCREASE the length of the vector, not decrease it
        if (minimum > 0)
        {
            lowest = Mathf.Max(lowest, minimum);
        }
        if (maximum < 1)
        {
            highest = Mathf.Min(highest, maximum);
        }

        if (lowest > highest)
            return false;

        return true;
    }

    #endregion // Static Bounding Intersect functions
}