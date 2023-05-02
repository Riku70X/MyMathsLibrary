using UnityEngine;

public class MyAABBCollider : MonoBehaviour, IMyCollider // Axis Alligned Bounding Box
{
    MyTransformComponent myTransform;

    MyVector3 minExtent;
    MyVector3 maxExtent;

    public MyVector3 getMinExtent => minExtent;
    public MyVector3 getMaxExtent => maxExtent;

    public float type => 1;

    public float top => maxExtent.y;

    public float bottom => minExtent.y;

    public float left => minExtent.x;

    public float right => maxExtent.x;

    public float front => maxExtent.z;

    public float back => minExtent.z;

    void CalculateTransform()
    {
        minExtent = new MyVector3(myTransform.getGlobalVerticesCoordinates[0].x, myTransform.getGlobalVerticesCoordinates[0].y, myTransform.getGlobalVerticesCoordinates[0].z);
        maxExtent = new MyVector3(myTransform.getGlobalVerticesCoordinates[0].x, myTransform.getGlobalVerticesCoordinates[0].y, myTransform.getGlobalVerticesCoordinates[0].z);

        for (int i = 0; i < myTransform.getGlobalVerticesCoordinates.Length; i++)
        {
            if (myTransform.getGlobalVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = myTransform.getGlobalVerticesCoordinates[i].x;
            }
            else if (myTransform.getGlobalVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = myTransform.getGlobalVerticesCoordinates[i].x;
            }

            if (myTransform.getGlobalVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = myTransform.getGlobalVerticesCoordinates[i].y;
            }
            else if (myTransform.getGlobalVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = myTransform.getGlobalVerticesCoordinates[i].y;
            }

            if (myTransform.getGlobalVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = myTransform.getGlobalVerticesCoordinates[i].z;
            }
            else if (myTransform.getGlobalVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = myTransform.getGlobalVerticesCoordinates[i].z;
            }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        CalculateTransform();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        CalculateTransform();
    }

    public MyVector3 GetClosestPointTo(MyVector3 point)
    {
        MyVector3 nearestPoint = MyVector3.zero;

        MyVector3 vectorToPoint = point - myTransform.position;
        nearestPoint.x = Mathf.Clamp(vectorToPoint.x, left, right);
        nearestPoint.y = Mathf.Clamp(vectorToPoint.y, bottom, top);
        nearestPoint.z = Mathf.Clamp(vectorToPoint.z, back, front);

        return nearestPoint;
    }

    public bool IsOverlappingWith(MyVector3 startPoint, MyVector3 endPoint, out MyVector3 intersectionPoint)
    {
        // Define our initial lowest and highest
        float lowest = 0.0f;
        float highest = 1.0f;

        // Default value for intersection point is needed
        intersectionPoint = MyVector3.zero;

        // We do an intersection check on every axis (we're reusing the AABBIsIntersectingAxis function)
        if (!MyMathsLibrary.AxisIntersectsAABB(MyVector3.right, this, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!MyMathsLibrary.AxisIntersectsAABB(MyVector3.up, this, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!MyMathsLibrary.AxisIntersectsAABB(MyVector3.forward, this, startPoint, endPoint, ref lowest, ref highest))
            return false;

        // Calculate our intersection point through interpolation
        intersectionPoint = MyMathsLibrary.GetLerp(startPoint, endPoint, lowest);

        return true;
    }

    public bool IsOverlappingWith(MyAABBCollider otherBox)
    {
        return !(otherBox.left > right
            || otherBox.right < left
            || otherBox.top < bottom
            || otherBox.bottom > top
            || otherBox.back > front
            || otherBox.front < back);
    }

    public bool IsOverlappingWith(MySphereCollider sphere)
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

    public bool IsOverlappingWith(MyCapsuleCollider capsule)
    {
        // placeholder for IMyCollider
        return false;
    }

    public void ShowForSeconds(float seconds)
    {
        // Bottom Square
        Debug.DrawLine(minExtent, new MyVector3(maxExtent.x, minExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(minExtent, new MyVector3(minExtent.x, minExtent.y, maxExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(maxExtent.x, minExtent.y, maxExtent.z), new MyVector3(maxExtent.x, minExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(maxExtent.x, minExtent.y, maxExtent.z), new MyVector3(minExtent.x, minExtent.y, maxExtent.z), Color.green, seconds);

        // Four Pillars
        Debug.DrawLine(minExtent, new MyVector3(minExtent.x, maxExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(maxExtent.x, minExtent.y, minExtent.z), new MyVector3(maxExtent.x, maxExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(minExtent.x, minExtent.y, maxExtent.z), new MyVector3(minExtent.x, maxExtent.y, maxExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(maxExtent.x, minExtent.y, maxExtent.z), maxExtent, Color.green, seconds);

        // Top Square
        Debug.DrawLine(new MyVector3(minExtent.x, maxExtent.y, minExtent.z), new MyVector3(maxExtent.x, maxExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(minExtent.x, maxExtent.y, minExtent.z), new MyVector3(minExtent.x, maxExtent.y, maxExtent.z), Color.green, seconds);
        Debug.DrawLine(maxExtent, new MyVector3(maxExtent.x, maxExtent.y, minExtent.z), Color.green, seconds);
        Debug.DrawLine(maxExtent, new MyVector3(minExtent.x, maxExtent.y, maxExtent.z), Color.green, seconds);
    }
}