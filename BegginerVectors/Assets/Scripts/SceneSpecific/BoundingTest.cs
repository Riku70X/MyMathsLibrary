using UnityEngine;

public class BoundingTest : MonoBehaviour
{
    GameObject Cube1;
    MyAABBComponent boundingBox1;

    GameObject Cube2;
    MyAABBComponent boundingBox2;

    GameObject Sphere1;
    MyTransformComponent sphere1Transform;
    MyBoundingSphere boundingSphere1;

    GameObject Sphere2;
    MyTransformComponent sphere2Transform;
    MyBoundingSphere boundingSphere2;

    GameObject Capsule1;
    MyTransformComponent capsule1Transform;
    MyBoundingCapsule boundingCapsule1;

    GameObject Capsule2;
    MyTransformComponent capsule2Transform;
    MyBoundingCapsule boundingCapsule2;

    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        boundingBox1 = Cube1.GetComponent<MyAABBComponent>();

        Cube2 = GameObject.Find("Cube2");
        boundingBox2 = Cube2.GetComponent<MyAABBComponent>();

        Sphere1 = GameObject.Find("Sphere1");
        sphere1Transform = Sphere1.GetComponent<MyTransformComponent>();

        Sphere2 = GameObject.Find("Sphere2");
        sphere2Transform = Sphere2.GetComponent<MyTransformComponent>();

        Capsule1 = GameObject.Find("Capsule1");
        capsule1Transform = Capsule1.GetComponent<MyTransformComponent>();

        Capsule2 = GameObject.Find("Capsule2");
        capsule2Transform = Capsule2.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        boundingSphere1 = new MyBoundingSphere(sphere1Transform);
        boundingSphere2 = new MyBoundingSphere(sphere2Transform);

        boundingCapsule1 = new MyBoundingCapsule(capsule1Transform, 2, .5f);
        boundingCapsule2 = new MyBoundingCapsule(capsule2Transform, 2, .5f);

        if (boundingBox1.isOverlappingWith(boundingBox2))
        {
            print("Box/Box Intersection!");
        }

        if (boundingBox1.isOverlappingWith(boundingSphere1))
        {
            print("Box/Sphere Intersection!");
        }

        if (boundingSphere1.isOverlappingWith(boundingBox1))
        {
            print("Sphere/Box Intersection!");
        }

        if (boundingSphere1.isOverlappingWith(boundingSphere2))
        {
            print("Sphere/Sphere Intersection!");
        }

        if (boundingSphere1.isOverlappingWith(boundingCapsule1))
        {
            print("Sphere/Capsule Intersection!");
        }

        if (boundingCapsule1.isOverlappingWith(boundingSphere1))
        {
            print("Capsule/Sphere Intersection!");
        }

        if (boundingCapsule1.isOverlappingWith(boundingCapsule2))
        {
            print("Capsule/Capsule Intersection!");
        }
    }
}