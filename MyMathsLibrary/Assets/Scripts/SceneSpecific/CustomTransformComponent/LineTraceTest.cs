using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 intersectionPoint;

    GameObject Cube;
    MyTransformComponent cubeTransform;
    //MyAABB localBox; The code pertaining to this variable can be restored once OBBs are implemented
    MyAABBCollider globalBox;

    MyMatrix4x4 inverseTransformMatrix;
    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    GameObject Sphere;
    MySphereCollider sphereCollider;

    GameObject Capsule;
    MyCapsuleCollider capsuleCollider;

    LineTraceTest()
    {
        globalStartPosition = MyVector3.zero;
        globalEndPosition = MyVector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube1");
        cubeTransform = Cube.GetComponent<MyTransformComponent>();
        //localBox = new MyAABB(cubeTransform);
        globalBox = Cube.GetComponent<MyAABBCollider>();

        Sphere = GameObject.Find("Sphere1");
        sphereCollider = Sphere.GetComponent<MySphereCollider>();

        Capsule = GameObject.Find("Capsule1");
        capsuleCollider = Capsule.GetComponent<MyCapsuleCollider>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        Debug.DrawLine(globalStartPosition, globalEndPosition);

        // Line/Box Test

        //// Create a new AABB around the transformed Cube and check if the line intersects it
        //globalBox = new MyAABB(cubeTransform);

        if (globalBox.IsOverlappingWith(globalStartPosition, globalEndPosition, out intersectionPoint))
        {
            print($"Line/Box Intersection! Global point : {intersectionPoint}");

            // if line intersects the new AABB, it must be close to the object. Inverse transform the line and compare it with the true local AABB.
            //inverseTransformMatrix = MyMathsLibrary.GetInverseTransformationMatrix(cubeTransform.scale, cubeTransform.eulerAngles, cubeTransform.position);

            //localStartPosition = inverseTransformMatrix * globalStartPosition;
            //localEndPosition = inverseTransformMatrix * globalEndPosition;

            //if (MyMathsLibrary.LineIntersectsAABB(localBox, localStartPosition, localEndPosition, out intersectionPoint))
            //{
            //    print($"Line/Cube Intersection! Local point : {intersectionPoint}, Global point : {cubeTransform.getTransformMatrix * intersectionPoint}");
            //}
        }

        // Line/Sphere Test

        if (sphereCollider.IsOverlappingWith(globalStartPosition, globalEndPosition, out intersectionPoint))
        {
            print($"Line/Sphere Intersection! Global point : {intersectionPoint}");
        }

        // Line/Capsule Test

        if (capsuleCollider.IsOverlappingWith(globalStartPosition, globalEndPosition, out intersectionPoint))
        {
            print($"Capsule/Sphere Intersection! Global point : {intersectionPoint}");
        }
    }
}