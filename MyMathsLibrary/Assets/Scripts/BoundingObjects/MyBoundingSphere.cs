using UnityEngine;

public class MyBoundingSphere : MyBoundingObject
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

    public bool isOverlappingWith(MyAABB box)
    {
        // Code adapted from Graphics Gems 1, V.8 "A simple method for box-sphere intersection testing" page 335-339 by James Arvo. Algorithm on page 336 (Fig. 1)
        // MAKE A PROPER ZOTERO REFERENCE FOR THIS

        float minDistanceSq = 0;

        if (centrepoint.x > box.getMaxExtent.x)
        {
            minDistanceSq += (centrepoint.x - box.getMaxExtent.x) * (centrepoint.x - box.getMaxExtent.x);
        }
        else if (centrepoint.x < box.getMinExtent.x)
        {
            minDistanceSq += (centrepoint.x - box.getMinExtent.x) * (centrepoint.x - box.getMinExtent.x);
        }

        if (centrepoint.y > box.getMaxExtent.y)
        {
            minDistanceSq += (centrepoint.y - box.getMaxExtent.y) * (centrepoint.y - box.getMaxExtent.y);
        }
        else if (centrepoint.y < box.getMinExtent.y)
        {
            minDistanceSq += (centrepoint.y - box.getMinExtent.y) * (centrepoint.y - box.getMinExtent.y);
        }

        if (centrepoint.z > box.getMaxExtent.z)
        {
            minDistanceSq += (centrepoint.z - box.getMaxExtent.z) * (centrepoint.z - box.getMaxExtent.z);
        }
        else if (centrepoint.z < box.getMinExtent.z)
        {
            minDistanceSq += (centrepoint.z - box.getMinExtent.z) * (centrepoint.z - box.getMinExtent.z);
        }

        if (minDistanceSq <= radius * radius) return true;
        return false;
    }

    public bool isOverlappingWith(MyBoundingSphere otherSphere)
    {
        float radiusSumDistanceSq = radius + otherSphere.radius;
        radiusSumDistanceSq *= radiusSumDistanceSq;

        float centreDistanceSq = (centrepoint - otherSphere.centrepoint).GetVectorLengthSquared();

        return centreDistanceSq < radiusSumDistanceSq;
    }

    public bool isOverlappingWith(MyBoundingCapsule capsule)
    {
        float closestDistanceSq = MyMathsLibrary.GetShortestDistanceSq(capsule.getBottomCentrepoint, capsule.getTopCentrepoint, centrepoint);

        float radiusSumDistanceSq = (capsule.getRadius + radius) * (capsule.getRadius + radius);

        return closestDistanceSq < radiusSumDistanceSq;
    }
}