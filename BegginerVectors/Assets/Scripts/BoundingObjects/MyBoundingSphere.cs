using UnityEngine;

public class MyBoundingSphere
{
    MyVector3 centrepoint;
    float radius;

    public MyBoundingSphere(MyTransformComponent transform, float radius)
    {
        centrepoint = transform.position;
        this.radius = radius;
    }

    public bool isOverlappingWith(MyBoundingSphere otherSphere)
    {
        float radiusSumDistanceSq = radius + otherSphere.radius;
        radiusSumDistanceSq *= radiusSumDistanceSq;
        float centreDistanceSq = (centrepoint - otherSphere.centrepoint).GetVectorLengthSquared();
        return centreDistanceSq < radiusSumDistanceSq;
    }
}