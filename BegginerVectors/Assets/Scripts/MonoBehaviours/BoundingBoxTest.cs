using UnityEngine;

public class BoundingBoxTest : MonoBehaviour
{
    GameObject Cube;
    MyTransformComponent cubeTransform;
    MyAABB Box;

    GameObject OtherCube;
    MyTransformComponent OtherCubeTransform;
    MyAABB otherBox;

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube");
        cubeTransform = Cube.GetComponent<MyTransformComponent>();

        OtherCube = GameObject.Find("OtherCube");
        OtherCubeTransform = OtherCube.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Box = new MyAABB(cubeTransform);
        otherBox = new MyAABB(OtherCubeTransform);

        if (Box.isOverlappingWith(otherBox))
        {
            Debug.Log($"Box Intersection!");
        }
    }
}