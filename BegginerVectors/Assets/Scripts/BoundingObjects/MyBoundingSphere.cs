using UnityEngine;

public class MyBoundingSphere
{
    MyVector3 centrepoint;
    float radius;

    public MyVector3 getCentrepoint => centrepoint;
    public float getRadius => radius;

    public MyBoundingSphere(MyTransformComponent transform, float radius)
    {
        centrepoint = transform.position;
        this.radius = radius;
    }

    public MyBoundingSphere(MyTransformComponent transform)
    {
        centrepoint = transform.position;
        radius = (centrepoint - transform.getGlobalVerticesCoordinates[0]).GetVectorLength();
    }

    public MyBoundingSphere(MyVector3 centrepoint, float radius)
    {
        this.centrepoint = centrepoint;
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