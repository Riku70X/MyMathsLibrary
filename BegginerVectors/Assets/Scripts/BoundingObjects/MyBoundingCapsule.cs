using UnityEngine;

public class MyBoundingCapsule
{
    MyVector3 topCentrePoint;
    MyVector3 bottomCentrePoint;
    float radius;

    public MyBoundingCapsule(MyTransformComponent transform, float height, float radius)
    {
        MyVector3 forwardVector = transform.rotation.ConvertEulerToDirection();
        MyVector3 rightVector = MyMathsLibrary.GetCrossProduct(MyVector3.up, forwardVector);
        MyVector3 upVector = MyMathsLibrary.GetCrossProduct(forwardVector, rightVector);

        float scalar = (height - 2 * radius) / 2; // returns the distance between the centre of the object and the centre of the circles

        topCentrePoint = transform.position + scalar * upVector;
        bottomCentrePoint = transform.position - scalar * upVector;

        this.radius = radius;
    }

    public bool isOverlappingWith(MyBoundingSphere sphere)
    {
        MyVector3 bottomToTop = topCentrePoint - bottomCentrePoint;
        MyVector3 bottomToSphere = sphere.getCentrepoint - bottomCentrePoint;
        MyVector3 topToBottom = -bottomToTop;
        MyVector3 topToSphere = sphere.getCentrepoint - topCentrePoint;

        if (MyMathsLibrary.GetDotProduct(bottomToTop, bottomToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(bottomCentrePoint, radius);
            return closestSphere.isOverlappingWith(sphere);
        }
        else if (MyMathsLibrary.GetDotProduct(topToBottom, topToSphere) <= 0)
        {
            MyBoundingSphere closestSphere = new(topCentrePoint, radius);
            return closestSphere.isOverlappingWith(sphere);
        }
        else
        {
            float closestDistanceSq = bottomToSphere.GetVectorLengthSquared() -
                                        Mathf.Pow(MyMathsLibrary.GetDotProduct(bottomToSphere, bottomToTop), 2) / bottomToTop.GetVectorLengthSquared();
        }


        return true;
    }
}