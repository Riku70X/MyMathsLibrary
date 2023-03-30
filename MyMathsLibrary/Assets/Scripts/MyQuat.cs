using Unity.VisualScripting;
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

    public static MyQuat identity => new(1, 0, 0, 0);

    public override string ToString() => ($"({w}, {vectorComponent})");

    public static implicit operator MyVector3(MyQuat quat) => new(quat.vectorComponent.x, quat.vectorComponent.y, quat.vectorComponent.z);

    public static MyQuat operator *(MyQuat lhs, MyQuat rhs) => MyMathsLibrary.MultiplyQuaternions(lhs, rhs);

    public MyVector3 GetAxis()
    {
        return vectorComponent.GetNormalisedVector();
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

    public MyVector3 ConvertToEulerAngles()
    {
        // Quaternion to Euler Angle code adapted from here:
        // https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles

        MyVector3 eulerAngles = MyVector3.zero;

        float x = vectorComponent.x; float y = vectorComponent.y; float z = vectorComponent.z;

        // roll (x-axis rotation)
        float sinr_cosp = 2 * (w * x + y * z);
        float cosr_cosp = 1 - 2 * (x * x + y * y);
        eulerAngles.x = Mathf.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        float sinp = Mathf.Sqrt(1 + 2 * (w * y - x * z));
        float cosp = Mathf.Sqrt(1 - 2 * (w * y - x * z));
        eulerAngles.y = 2 * Mathf.Atan2(sinp, cosp) - Mathf.PI / 2;

        // yaw (z-axis rotation)
        float siny_cosp = 2 * (w * z + x * y);
        float cosy_cosp = 1 - 2 * (y * y + z * z);
        eulerAngles.z = Mathf.Atan2(siny_cosp, cosy_cosp);

        return eulerAngles;
    }

    public MyMatrix4x4 ConvertToRotationMatrix()
    {
        // Quaternion to Rotation code made using a formula found from here:
        // https://automaticaddison.com/how-to-convert-a-quaternion-to-a-rotation-matrix/

        float x = vectorComponent.x; float y = vectorComponent.y; float z = vectorComponent.z;

        MyMatrix4x4 rotationMatrix = new(new MyVector3(2 * (w * w + x * x) - 1, 2 * (x * y + w * z), 2 * (x * z - w * y)),
                                        new MyVector3(2 * (x * y - w * z), 2 * (w * w + y * y) - 1, 2 * (y * z + w * x)),
                                        new MyVector3(2 * (x * z + w * y), 2 * (y * z - w * x), 2 * (w * w + z * z) - 1),
                                        new MyVector3(0, 0, 0));

        return rotationMatrix;
    }
}