using UnityEngine;

public class MyQuat
{
    float w, x, y, z;
    float angle;
    MyVector3 axis;

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

        float halfAngle = Mathf.Acos(w);

        angle = halfAngle * 2;
        if (halfAngle != 0)
        {
            axis = new MyVector3(x / Mathf.Sin(halfAngle), y / Mathf.Sin(halfAngle), z / Mathf.Sin(halfAngle));
        }
        else
        {
            Debug.LogWarning("QUATERNION WITH ANGLE 0 GENERATED, AXIS UNKNOWN");
        }
    }

    public MyQuat(MyVector3 vertex)
    {
        this.angle = Mathf.PI/2;
        this.axis = vertex;

        float halfAngle = angle / 2;

        w = Mathf.Cos(halfAngle);
        x = vertex.x * Mathf.Sin(halfAngle);
        y = vertex.y * Mathf.Sin(halfAngle);
        z = vertex.z * Mathf.Sin(halfAngle);
    }

    public override string ToString() => ($"({w}, {x}, {y}, {z})");

    public MyVector4 GetAxisAngle()
    {
        return new(axis.x, axis.y, axis.z, angle);
    }

    public MyVector3 GetAxis()
    {
        return axis;
    }

    public MyQuat GetInverse() => new(angle, -1.0f * axis);

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new((quatA.w * quatB.w) - MyVector3.GetDotProduct(quatA.GetAxis(), quatB.GetAxis(), false),
                                (quatA.GetAxis() * quatB.w) + (quatB.GetAxis() * quatA.w) + MyMathsLibrary.GetCrossProduct(quatA.GetAxis(), quatB.GetAxis()));
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
        MyQuat vertexQuat = new(vertex);
        Debug.LogWarning($"vertex: {vertexQuat.GetAxis()}");
        Debug.LogWarning($"quat: {(rotationQuat)}");
        Debug.LogWarning($"axis: {rotationQuat.GetAxis()}");
        Debug.LogWarning($"reverseaxis: {(rotationQuat.GetInverse()).GetAxis()}");
        Debug.LogWarning($"reversequat: {(rotationQuat.GetInverse())}");
        vertexQuat = rotationQuat * vertexQuat * rotationQuat.GetInverse();
        return vertexQuat.GetAxis();
    }
}