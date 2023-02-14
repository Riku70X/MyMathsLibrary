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

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new((quatA.w * quatB.w) - MyVector3.GetDotProduct(quatA.axis, quatB.axis),
                                (quatA.axis * quatB.w) + (quatB.axis * quatA.w) + MyMathsLibrary.GetCrossProduct(quatA.axis, quatB.axis));
        return returnQuat;
    }

    public static MyQuat operator * (MyQuat lhs, MyQuat rhs)
    {
        return MultiplyQuaternions(lhs, rhs);
    }
}