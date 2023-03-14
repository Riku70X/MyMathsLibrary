using UnityEngine;

public class BoundingTest : MonoBehaviour
{
    GameObject Cube1;
    MyTransformComponent cube1Transform;
    MyAABB box1;

    GameObject Cube2;
    MyTransformComponent cube2Transform;
    MyAABB box2;

    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        cube1Transform = Cube1.GetComponent<MyTransformComponent>();

        Cube2 = GameObject.Find("Cube2");
        cube2Transform = Cube2.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        box1 = new MyAABB(cube1Transform);
        box2 = new MyAABB(cube2Transform);

        if (box1.isOverlappingWith(box2))
        {
            Debug.Log($"Box Intersection!");
        }
    }
}