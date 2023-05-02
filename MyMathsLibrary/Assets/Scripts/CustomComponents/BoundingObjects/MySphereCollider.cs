using UnityEngine;

public class MySphereCollider : MonoBehaviour, IMyCollider // Bounding Sphere
{
    MyTransformComponent myTransform;

    MyVector3 centrepoint;
    float radius;

    float startingRadius;
    float transformScale;
    [SerializeField] float scale;
    
    public MyVector3 getCentrepoint => centrepoint;
    public float getRadius => radius;

    public float type => 0;

    MySphereCollider()
    {
        centrepoint = Vector3.zero;
        transformScale = 1;
        scale = 1;
        radius = 0;
    }

    void CalculateTransform()
    {
        transformScale = Mathf.Max(myTransform.scale.x, Mathf.Max(myTransform.scale.y, myTransform.scale.z));

        centrepoint = myTransform.position;
        radius = startingRadius * transformScale * scale;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        startingRadius = (MyVector3.zero - myTransform.getLocalVerticesCoordinates[0]).GetVectorLength();
        radius = startingRadius * transformScale * scale;

        CalculateTransform();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        CalculateTransform();
    }

    public MyVector3 GetClosestPointTo(MyVector3 point)
    {
        MyVector3 nearestPoint;

        IsOverlappingWith(point, centrepoint, out nearestPoint);
        return nearestPoint;
    }

    public bool IsOverlappingWith(MyVector3 startPoint, MyVector3 endPoint, out MyVector3 intersectionPoint)
    {
        // Default value for intersection point is needed
        intersectionPoint = MyVector3.zero;

        // Check if the line starts from within the sphere
        if ((startPoint - getCentrepoint).GetVectorLength() < getRadius)
        {
            intersectionPoint = startPoint;
            return true;
        }
        else
        {
            float sphereLineDistanceSq = MyMathsLibrary.GetShortestDistanceSq(startPoint, endPoint, centrepoint);
            float radiusSq = radius * radius;

            if (sphereLineDistanceSq < radiusSq)
            {
                MyVector3 sphereProjection = MyMathsLibrary.GetClosestPointOnLineSegment(centrepoint, startPoint, endPoint);
                float projectionToIntersectionLengthSq = radiusSq - sphereLineDistanceSq;
                MyVector3 projectionToStart = startPoint - sphereProjection;
                float scalar = projectionToIntersectionLengthSq / projectionToStart.GetVectorLengthSquared();
                scalar = Mathf.Sqrt(scalar);

                intersectionPoint = sphereProjection + (projectionToStart * scalar);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsOverlappingWith(MyAABBCollider box)
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

    public bool IsOverlappingWith(MySphereCollider otherSphere)
    {
        float radiusSumDistanceSq = radius + otherSphere.radius;
        radiusSumDistanceSq *= radiusSumDistanceSq;

        float centreDistanceSq = (centrepoint - otherSphere.centrepoint).GetVectorLengthSquared();

        return centreDistanceSq < radiusSumDistanceSq;
    }

    public bool IsOverlappingWith(MyCapsuleCollider capsule)
    {
        float closestDistanceSq = MyMathsLibrary.GetShortestDistanceSq(capsule.getBottomCentrepoint, capsule.getTopCentrepoint, centrepoint);

        float radiusSumDistanceSq = (capsule.getRadius + radius) * (capsule.getRadius + radius);

        return closestDistanceSq < radiusSumDistanceSq;
    }

    //public bool IsOverlappingWith(IMyCollider otherCollider)
    //{
    //    if (otherCollider.type == 0)
    //    {

    //    }
    //}

    public void ShowForSeconds(float seconds)
    {
        Debug.DrawLine(new MyVector3(centrepoint.x - radius, centrepoint.y, centrepoint.z), new MyVector3(centrepoint.x + radius, centrepoint.y, centrepoint.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(centrepoint.x, centrepoint.y - radius, centrepoint.z), new MyVector3(centrepoint.x, centrepoint.y + radius, centrepoint.z), Color.green, seconds);
        Debug.DrawLine(new MyVector3(centrepoint.x, centrepoint.y, centrepoint.z - radius), new MyVector3(centrepoint.x, centrepoint.y, centrepoint.z + radius), Color.green, seconds);
    }
}