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

    public MyVector4 GetAxisAngle()
    {
        float halfAngle = Mathf.Acos(w);

        float angle = halfAngle * 2;

        MyVector3 axis;
        if (halfAngle != 0)
        {
            axis = vectorComponent / Mathf.Sin(halfAngle);
        }
        else
        {
            Debug.LogError("QUATERNION HAS ANGLE 0, AXIS UNKNOWN");
            axis = vectorComponent;
        }

        return new MyVector4(axis.x, axis.y, axis.z, angle);
    }


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

    public MyQuat GetInverse() => new(w, -vectorComponent.x, -vectorComponent.y, -vectorComponent.z);

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new(0, 0, 0, 0);

        returnQuat.w = (quatA.w * quatB.w) - MyVector3.GetDotProduct(quatA.GetAxis(), quatB.GetAxis());

        returnQuat.vectorComponent = (quatA.vectorComponent * quatB.w) + (quatB.vectorComponent * quatA.w) +
                            MyMathsLibrary.GetCrossProduct(quatA.vectorComponent, quatB.vectorComponent);

        return returnQuat;
    }

    public static MyQuat operator * (MyQuat lhs, MyQuat rhs) => MultiplyQuaternions(lhs, rhs);

    public static MyQuat SLERP(MyQuat quatA, MyQuat quatB, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        MyQuat slerpQuart = quatB * quatA.GetInverse();
        MyVector4 axisAngle = slerpQuart.GetAxisAngle();
        MyQuat slerpQuartT = new(axisAngle.w * t, new MyVector3(axisAngle.x, axisAngle.y, axisAngle.z));

        return slerpQuartT * quatA;
    }

    public static MyVector3 Rotate(MyVector3 vertex, MyQuat rotationQuat)
    {
        MyQuat startVertexQuat = new MyQuat(vertex);

        MyQuat halfRotatedVertexQuat = rotationQuat * startVertexQuat;

        MyQuat fullRotatedVertexQuat = halfRotatedVertexQuat * rotationQuat.GetInverse();

        return fullRotatedVertexQuat.GetAxis();
    }

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