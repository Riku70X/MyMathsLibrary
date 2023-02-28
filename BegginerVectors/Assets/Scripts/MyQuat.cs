using UnityEngine;

public class MyQuat
{
    public float w;
    public MyVector3 vectorComponent;

    public MyQuat(float angle, MyVector3 axis)
    {
        axis = axis.GetNormalisedVector();
        float halfAngle = angle / 2;
        w = Mathf.Cos(halfAngle);

        vectorComponent = axis * Mathf.Sin(halfAngle);
    }

    public MyQuat(float w, float x, float y, float z)
    {
        this.w = w;
        vectorComponent = new MyVector3(x, y, z);
    }

    public MyQuat(MyVector3 vertex)
    {
        w = 0;
        vectorComponent = new MyVector3(vertex.x, vertex.y, vertex.z);
    }

    public override string ToString() => ($"({w}, {vectorComponent})");

    public static MyQuat operator *(MyQuat lhs, MyQuat rhs) => MyMathsLibrary.MultiplyQuaternions(lhs, rhs);

    public MyVector3 GetAxis()
    {
        float halfAngle = Mathf.Acos(w);

        MyVector3 axis;
        if (Mathf.Sin(halfAngle) != 0)
        {
            axis = vectorComponent / Mathf.Sin(halfAngle);
        }
        else
        {
            Debug.LogError("QUATERNION HAS ANGLE 0, AXIS UNKNOWN");
            axis = vectorComponent;
        }

        return new MyVector3(axis.x, axis.y, axis.z);
    }

    public float GetAngle()
    {
        return 2 * Mathf.Acos(w);
    }

    public MyVector4 GetAxisAngle()
    {
        MyVector3 axis = GetAxis();

        float angle = GetAngle();

        return new MyVector4(axis.x, axis.y, axis.z, angle);
    }

    public MyQuat GetInverse() => new(w, -vectorComponent.x, -vectorComponent.y, -vectorComponent.z);

    public MyMatrix4x4 ConvertToRotationMatrix()
    {
        float x = vectorComponent.x; float y = vectorComponent.y; float z = vectorComponent.z;

        MyMatrix4x4 rotationMatrix = new(new MyVector3(2 * (w * w + x * x) - 1, 2 * (x * y + w * z), 2 * (x * z - w * y)),
                                        new MyVector3(2 * (x * y - w * z), 2 * (w * w + y * y) - 1, 2 * (y * z + w * x)),
                                        new MyVector3(2 * (x * z + w * y), 2 * (y * z - w * x), 2 * (w * w + y * y) - 1),
                                        new MyVector3(0, 0, 0));
        return rotationMatrix;
    }
}