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

    public bool isOverlappingWith(MyAABB box)
    {
        // Code adapted from Graphics Gems 1, V.8 "A simple method for box-sphere intersection testing" page 335-339 by James Arvo. Algorithm on page 336 (Fig. 1)
        // MAKE A PROPER ZOTERO REFERENCE FOR THIS

        float[] centrePoint = new float[3];
        float[] minExtent = new float[3];
        float[] maxExtent = new float[3];

        centrePoint[0] = centrepoint.x; centrePoint[1] = centrepoint.y; centrePoint[2] = centrepoint.z;
        minExtent[0] = box.getMinExtent.x; minExtent[1] = box.getMinExtent.y; minExtent[2] = box.getMinExtent.z;
        maxExtent[0] = box.getMaxExtent.x; maxExtent[1] = box.getMaxExtent.y; maxExtent[2] = box.getMaxExtent.z;

        float minDistanceSq = 0;
        for (int i = 0; i < 3; i++)
        {
            if (centrePoint[i] > maxExtent[i])
            {
                minDistanceSq += Mathf.Pow(centrePoint[i] - maxExtent[i], 2);
            }
            else if (centrePoint[i] < minExtent[i])
            {
                minDistanceSq += Mathf.Pow(centrePoint[i] - minExtent[i], 2);
            }
        }

        if (minDistanceSq <= Mathf.Pow(radius, 2)) return true;
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
        MyVector3 bottomToTop = capsule.getTopCentrepoint - capsule.getBottomCentrepoint;
        MyVector3 bottomToSphere = centrepoint - capsule.getBottomCentrepoint;
        MyVector3 topToBottom = -bottomToTop;
        MyVector3 topToSphere = centrepoint - capsule.getTopCentrepoint;

        if (MyMathsLibrary.GetDotProduct(bottomToTop, bottomToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(capsule.getBottomCentrepoint, capsule.getRadius);
            return closestSphere.isOverlappingWith(this);
        }
        else if (MyMathsLibrary.GetDotProduct(topToBottom, topToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(capsule.getTopCentrepoint, capsule.getRadius);
            return closestSphere.isOverlappingWith(this);
        }
        else
        {
            float closestDistanceSq = bottomToSphere.GetVectorLengthSquared() -
                                        Mathf.Pow(MyMathsLibrary.GetDotProduct(bottomToSphere, bottomToTop), 2) / bottomToTop.GetVectorLengthSquared();
            float radiusSumDistanceSq = Mathf.Pow(capsule.getRadius + radius, 2);
            return closestDistanceSq < radiusSumDistanceSq;
        }
    }
}