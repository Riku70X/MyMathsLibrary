using UnityEngine;

public class MyAABB // Axis Alligned Bounding Box
{
    MyVector3 minExtent;
    MyVector3 maxExtent;

    public MyAABB(MyVector3 min, MyVector3 max)
    {
        minExtent = min;
        maxExtent = max;
    }

    public MyAABB(MyTransformComponent transform)
    {
        minExtent = new MyVector3(transform.globalVerticesCoordinates[0].x, transform.globalVerticesCoordinates[0].y, transform.globalVerticesCoordinates[0].z);
        maxExtent = new MyVector3(transform.globalVerticesCoordinates[0].x, transform.globalVerticesCoordinates[0].y, transform.globalVerticesCoordinates[0].z);

        for (int i = 0; i < transform.globalVerticesCoordinates.Length; i++)
        {
            if (transform.globalVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = transform.globalVerticesCoordinates[i].x;
            }
            else if (transform.globalVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = transform.globalVerticesCoordinates[i].x;
            }

            if (transform.globalVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = transform.globalVerticesCoordinates[i].y;
            }
            else if (transform.globalVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = transform.globalVerticesCoordinates[i].y;
            }

            if (transform.globalVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = transform.globalVerticesCoordinates[i].z;
            }
            else if (transform.globalVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = transform.globalVerticesCoordinates[i].z;
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
}