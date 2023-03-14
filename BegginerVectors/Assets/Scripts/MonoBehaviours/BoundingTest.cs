using UnityEngine;

public class BoundingTest : MonoBehaviour
{
    GameObject Cube1;
    MyTransformComponent cube1Transform;
    MyAABB boundingBox1;

    GameObject Cube2;
    MyTransformComponent cube2Transform;
    MyAABB boundingBox2;

    GameObject Sphere1;
    MyTransformComponent sphere1Transform;
    MyBoundingSphere boundingSphere1;

    GameObject Sphere2;
    MyTransformComponent sphere2Transform;
    MyBoundingSphere boundingSphere2;

    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        cube1Transform = Cube1.GetComponent<MyTransformComponent>();

        Cube2 = GameObject.Find("Cube2");
        cube2Transform = Cube2.GetComponent<MyTransformComponent>();

        Sphere1 = GameObject.Find("Sphere1");
        sphere1Transform = Sphere1.GetComponent<MyTransformComponent>();

        Sphere2 = GameObject.Find("Sphere2");
        sphere2Transform = Sphere2.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boundingBox1 = new MyAABB(cube1Transform);
        boundingBox2 = new MyAABB(cube2Transform);

        boundingSphere1 = new MyBoundingSphere(sphere1Transform, .5f);
        boundingSphere2 = new MyBoundingSphere(sphere2Transform, .5f);

        if (boundingBox1.isOverlappingWith(boundingBox2))
        {
            print("Box Intersection!");
        }

        if (boundingSphere1.isOverlappingWith(boundingSphere2))
        {
            print("Sphere Intersection!");
        }
    }
}