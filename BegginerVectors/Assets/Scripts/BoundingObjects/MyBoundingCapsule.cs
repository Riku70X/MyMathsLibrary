using UnityEngine;

public class MyBoundingCapsule
{
    MyVector3 topCentrepoint;
    MyVector3 bottomCentrepoint;
    float radius;

    public MyVector3 getTopCentrepoint => topCentrepoint;
    public MyVector3 getBottomCentrepoint => bottomCentrepoint;
    public float getRadius => radius;

    public MyBoundingCapsule(MyTransformComponent transform, float height, float radius)
    {
        MyVector3 forwardVector = transform.rotation.ConvertEulerToDirection();
        MyVector3 rightVector = MyMathsLibrary.GetCrossProduct(MyVector3.up, forwardVector);
        MyVector3 upVector = MyMathsLibrary.GetCrossProduct(forwardVector, rightVector);

        float scalar = (height - 2 * radius) / 2; // returns the distance between the centre of the object and the centre of the circles

        topCentrepoint = transform.position + scalar * upVector;
        bottomCentrepoint = transform.position - scalar * upVector;

        this.radius = radius;
    }

    public bool isOverlappingWith(MyBoundingSphere sphere)
    {
        MyVector3 bottomToTop = topCentrepoint - bottomCentrepoint;
        MyVector3 bottomToSphere = sphere.getCentrepoint - bottomCentrepoint;
        MyVector3 topToBottom = -bottomToTop;
        MyVector3 topToSphere = sphere.getCentrepoint - topCentrepoint;

        if (MyMathsLibrary.GetDotProduct(bottomToTop, bottomToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(bottomCentrepoint, radius);
            return closestSphere.isOverlappingWith(sphere);
        }
        else if (MyMathsLibrary.GetDotProduct(topToBottom, topToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(topCentrepoint, radius);
            return closestSphere.isOverlappingWith(sphere);
        }
        else
        {
            float closestDistanceSq = bottomToSphere.GetVectorLengthSquared() -
                                        Mathf.Pow(MyMathsLibrary.GetDotProduct(bottomToSphere, bottomToTop), 2) / bottomToTop.GetVectorLengthSquared();
            float radiusSumDistanceSq = Mathf.Pow(radius + sphere.getRadius, 2);
            return closestDistanceSq < radiusSumDistanceSq;
        }
    }
}