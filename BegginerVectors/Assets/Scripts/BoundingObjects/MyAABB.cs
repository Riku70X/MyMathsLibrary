using UnityEngine;

public class MyAABB // Axis Alligned Bounding Box
{
    MyVector3 minExtent;
    MyVector3 maxExtent;

    public MyVector3 getMinExtent => minExtent;
    public MyVector3 getMaxExtent => maxExtent;

    public MyAABB(MyVector3 min, MyVector3 max)
    {
        minExtent = min;
        maxExtent = max;
    }

    public MyAABB(MyTransformComponent transform)
    {
        minExtent = new MyVector3(transform.getGlobalVerticesCoordinates[0].x, transform.getGlobalVerticesCoordinates[0].y, transform.getGlobalVerticesCoordinates[0].z);
        maxExtent = new MyVector3(transform.getGlobalVerticesCoordinates[0].x, transform.getGlobalVerticesCoordinates[0].y, transform.getGlobalVerticesCoordinates[0].z);

        for (int i = 0; i < transform.getGlobalVerticesCoordinates.Length; i++)
        {
            if (transform.getGlobalVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = transform.getGlobalVerticesCoordinates[i].x;
            }
            else if (transform.getGlobalVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = transform.getGlobalVerticesCoordinates[i].x;
            }

            if (transform.getGlobalVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = transform.getGlobalVerticesCoordinates[i].y;
            }
            else if (transform.getGlobalVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = transform.getGlobalVerticesCoordinates[i].y;
            }

            if (transform.getGlobalVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = transform.getGlobalVerticesCoordinates[i].z;
            }
            else if (transform.getGlobalVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = transform.getGlobalVerticesCoordinates[i].z;
            }
        }
    }

    public float top => maxExtent.y;

    public float bottom => minExtent.y;

    public float left => minExtent.x;

    public float right => maxExtent.x;

    public float front => maxExtent.z;

    public float back => minExtent.z;

    public bool isOverlappingWith(MyAABB otherBox)
    {
        return !(otherBox.left > right
            || otherBox.right < left
            || otherBox.top < bottom
            || otherBox.bottom > top
            || otherBox.back > front
            || otherBox.front < back);
    }

    public bool isOverlappingWith(MyBoundingSphere sphere)
    {
        // Code adapted from Graphics Gems 1, V.8 "A simple method for box-sphere intersection testing" page 335-339 by James Arvo. Algorithm on page 336 (Fig. 1)
        // MAKE A PROPER ZOTERO REFERENCE FOR THIS

        float minDistanceSq = 0;

        if (sphere.getCentrepoint.x > maxExtent.x)
        {
            minDistanceSq += (sphere.getCentrepoint.x - maxExtent.x) * (sphere.getCentrepoint.x - maxExtent.x);
        }
        else if (sphere.getCentrepoint.x < minExtent.x)
        {
            minDistanceSq += (sphere.getCentrepoint.x - minExtent.x) * (sphere.getCentrepoint.x - minExtent.x);
        }

        if (sphere.getCentrepoint.y > maxExtent.y)
        {
            minDistanceSq += (sphere.getCentrepoint.y - maxExtent.y) * (sphere.getCentrepoint.y - maxExtent.y);
        }
        else if (sphere.getCentrepoint.y < minExtent.y)
        {
            minDistanceSq += (sphere.getCentrepoint.y - minExtent.y) * (sphere.getCentrepoint.y - minExtent.y);
        }

        if (sphere.getCentrepoint.z > maxExtent.z)
        {
            minDistanceSq += (sphere.getCentrepoint.z - maxExtent.z) * (sphere.getCentrepoint.z - maxExtent.z);
        }
        else if (sphere.getCentrepoint.z < minExtent.z)
        {
            minDistanceSq += (sphere.getCentrepoint.z - minExtent.z) * (sphere.getCentrepoint.z - minExtent.z);
        }

        if (minDistanceSq <= sphere.getRadius * sphere.getRadius) return true;
        return false;
    }
}