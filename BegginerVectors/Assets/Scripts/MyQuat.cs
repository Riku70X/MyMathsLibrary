using UnityEngine;

public class MyQuat
{
    public float w, x, y, z;
    public float angle;
    public MyVector3 axis;

    public MyQuat(float angle, MyVector3 axis)
    {
        this.angle = angle;
        this.axis = axis;

        float halfAngle = angle / 2;

        w = Mathf.Cos(halfAngle);
        x = axis.x * Mathf.Sin(halfAngle);
        y = axis.y * Mathf.Sin(halfAngle);
        z = axis.z * Mathf.Sin(halfAngle);
    }

    public MyQuat(float w, float x, float y, float z)
    {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyQuat(MyVector3 vertex)
    {
        w = 0;
        x = vertex.x;
        y = vertex.y;
        z = vertex.z;
    }

    public MyVector4 GetAxisAngle()
    {
        float halfAngle = Mathf.Acos(w);

        MyVector4 returnVector = new(0, 0, 0, 0)
        {
            w = halfAngle * 2,
            x = x / Mathf.Sin(halfAngle),
            y = y / Mathf.Sin(halfAngle),
            z = z / Mathf.Sin(halfAngle)
        };

        return returnVector;
    }

    public MyQuat Inverse() => new(w, -x, -y, -z);

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new((quatA.w * quatB.w) - MyVector3.GetDotProduct(quatA.axis, quatB.axis),
                                (quatA.axis * quatB.w) + (quatB.axis * quatA.w) + MyMathsLibrary.GetCrossProduct(quatA.axis, quatB.axis));
        return returnQuat;
    }

    public static MyQuat operator * (MyQuat lhs, MyQuat rhs) => MultiplyQuaternions(lhs, rhs);

    public static MyQuat SLERP(MyQuat quatA, MyQuat quatB, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        MyQuat slerpQuart = quatB * quatA.Inverse();
        MyVector4 axisAngle = slerpQuart.GetAxisAngle();
        MyQuat slerpQuartT = new(axisAngle.w * t, new MyVector3(axisAngle.x, axisAngle.y, axisAngle.z));

        return slerpQuartT * quatA;
    }
}