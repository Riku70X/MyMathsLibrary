using UnityEngine;

public class MyCapsuleCollider : MonoBehaviour // Bounding Capsule
{
    MyTransformComponent myTransform;

    MyVector3 localTopCentrepoint;
    MyVector3 localBottomCentrepoint;

    MyVector3 globalTopCentrepoint;
    MyVector3 globalBottomCentrepoint;

    public float height;
    public float radius;

    public MyVector3 getTopCentrepoint => globalTopCentrepoint;
    public MyVector3 getBottomCentrepoint => globalBottomCentrepoint;
    public float getRadius => radius;

    MyCapsuleCollider()
    {
        localTopCentrepoint = MyVector3.zero;
        localBottomCentrepoint = MyVector3.zero;

        globalTopCentrepoint = MyVector3.zero;
        globalBottomCentrepoint = MyVector3.zero;

        height = 0;
        radius = 0;
    }

    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();

        float scalar = (height - 2 * radius) / 2; // returns the distance between the centre of the object and the centre of the circles

        localTopCentrepoint = scalar * MyVector3.up;
        localBottomCentrepoint = -scalar * MyVector3.up;
    }

    void FixedUpdate()
    {
        globalTopCentrepoint = myTransform.getTransformMatrix * localTopCentrepoint;
        globalBottomCentrepoint = myTransform.getTransformMatrix * localBottomCentrepoint;
    }

    public bool IsOverlappingWith(MySphereCollider sphere)
    {
        float closestDistanceSq = MyMathsLibrary.GetShortestDistanceSq(globalBottomCentrepoint, globalTopCentrepoint, sphere.getCentrepoint);

        float radiusSumDistanceSq = (radius + sphere.getRadius) * (radius + sphere.getRadius);

        return closestDistanceSq < radiusSumDistanceSq;
    }

    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule)
    {
        // code adapted from a formula found at https://wickedengine.net/2020/04/26/capsule-collision-detection/

        MyVector3 bottomBottomVector = otherCapsule.getBottomCentrepoint - globalBottomCentrepoint;
        MyVector3 bottomTopVector = otherCapsule.getTopCentrepoint - globalBottomCentrepoint;
        MyVector3 topBottomVector = otherCapsule.getBottomCentrepoint - globalTopCentrepoint;
        MyVector3 topTopVector = otherCapsule.getTopCentrepoint - globalTopCentrepoint;

        float bottomBottomDistanceSq = MyMathsLibrary.GetDotProduct(bottomBottomVector, bottomBottomVector);
        float bottomTopDistanceSq = MyMathsLibrary.GetDotProduct(bottomTopVector, bottomTopVector);
        float topBottomDistanceSq = MyMathsLibrary.GetDotProduct(topBottomVector, topBottomVector);
        float topTopDistanceSq = MyMathsLibrary.GetDotProduct(topTopVector, topTopVector);

        MyVector3 closestPointOnThisCapsule;
        if (topBottomDistanceSq < bottomBottomDistanceSq || topBottomDistanceSq < bottomTopDistanceSq || topTopDistanceSq < bottomBottomDistanceSq || topTopDistanceSq < bottomTopDistanceSq)
        {
            closestPointOnThisCapsule = globalTopCentrepoint;
        }
        else
        {
            closestPointOnThisCapsule = globalBottomCentrepoint;
        }

        MyVector3 closestPointOnOtherCapsule = MyMathsLibrary.GetClosestPointOnLineSegment(closestPointOnThisCapsule, otherCapsule.getBottomCentrepoint, otherCapsule.getTopCentrepoint);

        closestPointOnThisCapsule = MyMathsLibrary.GetClosestPointOnLineSegment(closestPointOnOtherCapsule, globalBottomCentrepoint, globalTopCentrepoint);

        float radiusSumDistanceSq = radius + otherCapsule.radius;
        radiusSumDistanceSq *= radiusSumDistanceSq;

        float closestDistanceSq = (closestPointOnThisCapsule - closestPointOnOtherCapsule).GetVectorLengthSquared();

        return closestDistanceSq < radiusSumDistanceSq;
    }

    public void ShowForSeconds(float seconds)
    {
        Debug.DrawLine(globalBottomCentrepoint, globalTopCentrepoint, Color.green, seconds);

        Debug.DrawRay(globalBottomCentrepoint, new MyVector3(radius, 0, 0), Color.green, seconds);
        Debug.DrawRay(globalBottomCentrepoint, new MyVector3(-radius, 0, 0), Color.green, seconds);
        Debug.DrawRay(globalBottomCentrepoint, new MyVector3(0, -radius, 0), Color.green, seconds);
        Debug.DrawRay(globalBottomCentrepoint, new MyVector3(0, 0, radius), Color.green, seconds);
        Debug.DrawRay(globalBottomCentrepoint, new MyVector3(0, 0, -radius), Color.green, seconds);

        Debug.DrawRay(globalTopCentrepoint, new MyVector3(radius, 0, 0), Color.green, seconds);
        Debug.DrawRay(globalTopCentrepoint, new MyVector3(-radius, 0, 0), Color.green, seconds);
        Debug.DrawRay(globalTopCentrepoint, new MyVector3(0, radius, 0), Color.green, seconds);
        Debug.DrawRay(globalTopCentrepoint, new MyVector3(0, 0, radius), Color.green, seconds);
        Debug.DrawRay(globalTopCentrepoint, new MyVector3(0, 0, -radius), Color.green, seconds);
    }
}