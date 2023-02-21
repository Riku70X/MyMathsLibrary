using UnityEngine;

public class MyQuat
{
    float w, x, y, z;

    public MyQuat(float angle, MyVector3 axis)
    {
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

    public override string ToString() => ($"({w}, {x}, {y}, {z})");

    public MyVector4 GetAxisAngle()
    {
        float halfAngle = Mathf.Acos(w);

        float angle = halfAngle * 2;

        MyVector3 axis;
        if (halfAngle != 0)
        {
            axis = new(x / Mathf.Sin(halfAngle), y / Mathf.Sin(halfAngle), z / Mathf.Sin(halfAngle));
        }
        else
        {
            Debug.LogError("QUATERNION HAS ANGLE 0, AXIS UNKNOWN");
            axis = MyVector3.zero;
        }

        return new MyVector4(axis.x, axis.y, axis.z, angle);
    }


    public MyVector3 GetAxis()
    {
        float halfAngle = Mathf.Acos(w);

        float angle = halfAngle * 2;

        MyVector3 axis;
        if (halfAngle != 0)
        {
            axis = new(x / Mathf.Sin(halfAngle), y / Mathf.Sin(halfAngle), z / Mathf.Sin(halfAngle));
        }
        else
        {
            Debug.LogError("QUATERNION HAS ANGLE 0, AXIS UNKNOWN");
            axis = MyVector3.zero;
        }

        return new MyVector3(axis.x, axis.y, axis.z);
    }

    public MyQuat GetInverse() => new(w, -x, -y, -z);

    public static MyQuat MultiplyQuaternions(MyQuat quatA, MyQuat quatB)
    {
        MyQuat returnQuat = new(0, 0, 0, 0);

        returnQuat.w = ((quatA.w * quatB.w) - MyVector3.GetDotProduct(quatA.GetAxis(), quatB.GetAxis()));

        MyVector3 vector = new((new MyVector3(quatA.x, quatA.y, quatA.z) * quatB.w) + (new MyVector3(quatB.x, quatB.y, quatB.z) * quatA.w) +
                            MyMathsLibrary.GetCrossProduct(new MyVector3(quatA.x, quatA.y, quatA.z), new MyVector3(quatB.x, quatB.y, quatB.z)));

        returnQuat.x = vector.x;
        returnQuat.y = vector.y;
        returnQuat.z = vector.z;

        Debug.LogError($"A: {quatA.w}, B: {quatB.w}, axis: {quatA.GetAxis()}, {quatB.GetAxis()}, product: {MyVector3.GetDotProduct(quatA.GetAxis(), quatB.GetAxis(), false)}");
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
        MyQuat startVertexQuat = new(vertex);

        Debug.Log($"Vertex: {startVertexQuat.GetAxis()}");

        Debug.LogWarning($"rotationQuat: {rotationQuat}");

        Debug.LogError($"rotateAxis: {rotationQuat.GetAxis()}");

        Debug.LogWarning($"startVertexQuat: {startVertexQuat}");

        MyQuat halfRotatedVertexQuat = rotationQuat * startVertexQuat;

        Debug.LogWarning($"halfRotatedQuat: {halfRotatedVertexQuat}");
        Debug.LogError($"halfRotatedAxis: {halfRotatedVertexQuat.GetAxis()}");

        Debug.Log($"Vertex: {halfRotatedVertexQuat.GetAxis()}");

        Debug.LogWarning($"rotationQuatInverse: {rotationQuat.GetInverse()}");

        MyQuat fullRotatedVertexQuat = halfRotatedVertexQuat * rotationQuat.GetInverse();

        Debug.LogWarning($"fullRotatedQuat: {fullRotatedVertexQuat}");

        Debug.Log($"Vertex: {fullRotatedVertexQuat.GetAxis()}");

        return fullRotatedVertexQuat.GetAxis();
    }
}