using UnityEngine;

public class BoundingTest : MonoBehaviour
{
    GameObject Cube1;
    MyAABBCollider boundingBox1;

    GameObject Cube2;
    MyAABBCollider boundingBox2;

    GameObject Sphere1;
    MySphereCollider boundingSphere1;

    GameObject Sphere2;
    MySphereCollider boundingSphere2;

    GameObject Capsule1;
    MyCapsuleCollider boundingCapsule1;

    GameObject Capsule2;
    MyCapsuleCollider boundingCapsule2;

    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        boundingBox1 = Cube1.GetComponent<MyAABBCollider>();

        Cube2 = GameObject.Find("Cube2");
        boundingBox2 = Cube2.GetComponent<MyAABBCollider>();

        Sphere1 = GameObject.Find("Sphere1");
        boundingSphere1 = Sphere1.GetComponent<MySphereCollider>();

        Sphere2 = GameObject.Find("Sphere2");
        boundingSphere2 = Sphere2.GetComponent<MySphereCollider>();

        Capsule1 = GameObject.Find("Capsule1");
        boundingCapsule1 = Capsule1.GetComponent<MyCapsuleCollider>();

        Capsule2 = GameObject.Find("Capsule2");
        boundingCapsule2 = Capsule2.GetComponent<MyCapsuleCollider>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        boundingBox1.ShowForSeconds(Time.fixedDeltaTime);
        boundingSphere1.ShowForSeconds(Time.fixedDeltaTime);
        boundingCapsule1.ShowForSeconds(Time.fixedDeltaTime);

        if (boundingBox1.IsOverlappingWith(boundingBox2))
        {
            print("Box/Box Intersection!");
        }

        if (boundingBox1.IsOverlappingWith(boundingSphere1))
        {
            print("Box/Sphere Intersection!");
        }

        if (boundingSphere1.IsOverlappingWith(boundingBox1))
        {
            print("Sphere/Box Intersection!");
        }

        if (boundingSphere1.IsOverlappingWith(boundingSphere2))
        {
            print("Sphere/Sphere Intersection!");
        }

        if (boundingSphere1.IsOverlappingWith(boundingCapsule1))
        {
            print("Sphere/Capsule Intersection!");
        }

        if (boundingCapsule1.IsOverlappingWith(boundingSphere1))
        {
            print("Capsule/Sphere Intersection!");
        }

        if (boundingCapsule1.IsOverlappingWith(boundingCapsule2))
        {
            print("Capsule/Capsule Intersection!");
        }
    }
}