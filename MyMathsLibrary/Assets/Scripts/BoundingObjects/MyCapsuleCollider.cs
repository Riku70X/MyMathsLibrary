using UnityEngine;

public class MyCapsuleCollider : MyCollider // Bounding Capsule
{
    readonly MyVector3 topCentrepoint;
    readonly MyVector3 bottomCentrepoint;
    readonly float radius;

    public MyVector3 getTopCentrepoint => topCentrepoint;
    public MyVector3 getBottomCentrepoint => bottomCentrepoint;
    public float getRadius => radius;

    public MyCapsuleCollider(MyTransformComponent transform, float height, float radius)
    {
        // note: the capsule will not follow the orientation of the object if it is rolled

        MyVector3 forwardVector = transform.eulerAngles.ConvertEulerToDirection();
        MyVector3 rightVector = MyMathsLibrary.GetCrossProduct(MyVector3.up, forwardVector, true);
        MyVector3 upVector = MyMathsLibrary.GetCrossProduct(forwardVector, rightVector, true);

        float scalar = (height - 2 * radius) / 2; // returns the distance between the centre of the object and the centre of the circles

        topCentrepoint = transform.position + scalar * upVector;
        bottomCentrepoint = transform.position - scalar * upVector;

        this.radius = radius;
    }

    public bool IsOverlappingWith(MySphereCollider sphere)
    {
        float closestDistanceSq = MyMathsLibrary.GetShortestDistanceSq(bottomCentrepoint, topCentrepoint, sphere.getCentrepoint);

        float radiusSumDistanceSq = (radius + sphere.getRadius) * (radius + sphere.getRadius);

        return closestDistanceSq < radiusSumDistanceSq;
    }

    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule)
    {
        // code adapted from a formula found at https://wickedengine.net/2020/04/26/capsule-collision-detection/

        MyVector3 bottomBottomVector = otherCapsule.getBottomCentrepoint - bottomCentrepoint;
        MyVector3 bottomTopVector = otherCapsule.getTopCentrepoint - bottomCentrepoint;
        MyVector3 topBottomVector = otherCapsule.getBottomCentrepoint - topCentrepoint;
        MyVector3 topTopVector = otherCapsule.getTopCentrepoint - topCentrepoint;

        float bottomBottomDistanceSq = MyMathsLibrary.GetDotProduct(bottomBottomVector, bottomBottomVector);
        float bottomTopDistanceSq = MyMathsLibrary.GetDotProduct(bottomTopVector, bottomTopVector);
        float topBottomDistanceSq = MyMathsLibrary.GetDotProduct(topBottomVector, topBottomVector);
        float topTopDistanceSq = MyMathsLibrary.GetDotProduct(topTopVector, topTopVector);

        MyVector3 closestPointOnThisCapsule;
        if (topBottomDistanceSq < bottomBottomDistanceSq || topBottomDistanceSq < bottomTopDistanceSq || topTopDistanceSq < bottomBottomDistanceSq || topTopDistanceSq < bottomTopDistanceSq)
        {
            closestPointOnThisCapsule = topCentrepoint;
        }
        else
        {
            closestPointOnThisCapsule = bottomCentrepoint;
        }

        MyVector3 closestPointOnOtherCapsule = MyMathsLibrary.GetClosestPointOnLineSegment(closestPointOnThisCapsule, otherCapsule.getBottomCentrepoint, otherCapsule.getTopCentrepoint);

        closestPointOnThisCapsule = MyMathsLibrary.GetClosestPointOnLineSegment(closestPointOnOtherCapsule, bottomCentrepoint, topCentrepoint);

        float radiusSumDistanceSq = radius + otherCapsule.radius;
        radiusSumDistanceSq *= radiusSumDistanceSq;

        float closestDistanceSq = (closestPointOnThisCapsule - closestPointOnOtherCapsule).GetVectorLengthSquared();

        return closestDistanceSq < radiusSumDistanceSq;
    }
}