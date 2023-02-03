using UnityEngine;

public class AABB
{
    MyVector3 minExtent;
    MyVector3 maxExtent;
    public AABB(MyVector3 min, MyVector3 max)
    {
        minExtent = min;
        maxExtent = max;
    }

    public float top
    { get { return maxExtent.y; } }

    public float bottom
    { get { return minExtent.y; } }

    public float left
    { get { return minExtent.x; } }

    public float right
    { get { return maxExtent.x; } }

    public float front
    { get { return maxExtent.z; } }

    public float back
    { get { return minExtent.z; } }

    public static bool Intersects(AABB box1, AABB box2)
    {
        return !(box2.left > box1.right
            || box2.right < box1.left
            || box2.top < box1.bottom
            || box2.bottom > box1.top
            || box2.back > box1.front
            || box2.front < box1.back);
    }

    public static bool IntersectingAxis(MyVector3 axis, AABB box, MyVector3 startPoint, MyVector3 endPoint, ref float lowest, ref float highest)
    {
        // Calculate our Minimum and Maximum based on the current axis
        float minimum = 0.0f, maximum = 1.0f;
        if (axis == MyVector3.right)
        {
            minimum = (box.left - startPoint.x) / (endPoint.x - startPoint.x);
            maximum = (box.right - startPoint.x) / (endPoint.x - startPoint.x);
        }
        else if (axis == MyVector3.up)
        {
            minimum = (box.bottom - startPoint.y) / (endPoint.y - startPoint.y);
            maximum = (box.top - startPoint.y) / (endPoint.y - startPoint.y);
        }
        else if (axis == MyVector3.forward)
        {
            minimum = (box.back - startPoint.z) / (endPoint.z - startPoint.z);
            maximum = (box.front - startPoint.z) / (endPoint.z - startPoint.z);
        }

        if (maximum < minimum)
        {
            // Swapping values
            (minimum, maximum) = (maximum, minimum);
        }

        // Eliminate non-intersections early
        if (maximum < lowest)
            return false;

        if (minimum > highest)
            return false;

        lowest = Mathf.Max(lowest, minimum);
        highest = Mathf.Max(highest, maximum);

        if (lowest > highest)
            return false;

        return true;
    }

    public static bool LineIntersection(AABB box, MyVector3 startPoint, MyVector3 endPoint, out MyVector3 intersectionPoint)
    {
        // Define our initial lowest and highest
        float lowest = 0.0f;
        float highest = 0.0f;

        // Default value for intersection point is needed
        intersectionPoint = MyVector3.zero;

        // We do an intersection check on every axis (we're reusing the IntersectingAxis function)
        if (!IntersectingAxis(MyVector3.right, box, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!IntersectingAxis(MyVector3.up, box, startPoint, endPoint, ref lowest, ref highest))
            return false;

        if (!IntersectingAxis(MyVector3.forward, box, startPoint, endPoint, ref lowest, ref highest))
            return false;

        // Calculate our intersection point through interpolation
        intersectionPoint = MyMathsLibrary.GetLerp(startPoint, endPoint, lowest);

        return true;
    }
}