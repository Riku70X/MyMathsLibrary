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
    }
}