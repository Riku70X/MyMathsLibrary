using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 intersectionPoint;

    GameObject Cube;
    MyTransformComponent cubeTransform;
    MyAABB localBox;
    MyAABB globalBox;

    MyMatrix4x4 inverseTransformMatrix;

    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    GameObject Sphere;
    MyTransformComponent sphereTransform;
    MyBoundingSphere boundingSphere;

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
        localBox = new MyAABB(cubeTransform);

        Sphere = GameObject.Find("Sphere1");
        sphereTransform = Sphere.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(globalStartPosition, globalEndPosition);

        #region Line/Box Test

        // Create a new AABB around the transformed Cube and check if the line intersects it
        globalBox = new MyAABB(cubeTransform);

        if (MyMathsLibrary.LineIntersectsAABB(globalBox, globalStartPosition, globalEndPosition, out intersectionPoint))
        {
            print($"Line/Box Intersection! Local point : {intersectionPoint}, Global point : {cubeTransform.getTransformMatrix * intersectionPoint}");

            // if line intersects the new AABB, it must be close to the object. Inverse transform the line and compare it with the true local AABB.
            inverseTransformMatrix = MyMathsLibrary.GetInverseTransformationMatrix(cubeTransform.scale, cubeTransform.eulerAngles, cubeTransform.position);

            localStartPosition = inverseTransformMatrix * globalStartPosition;
            localEndPosition = inverseTransformMatrix * globalEndPosition;

            if (MyMathsLibrary.LineIntersectsAABB(localBox, localStartPosition, localEndPosition, out intersectionPoint))
            {
                print($"Line/Cube Intersection! Local point : {intersectionPoint}, Global point : {cubeTransform.getTransformMatrix * intersectionPoint}");
            }
        }

        #endregion //Line/Box Test

        #region Line/Sphere Test

        boundingSphere = new MyBoundingSphere(sphereTransform);

        if (MyMathsLibrary.LineIntersectsBoundingSphere(boundingSphere, globalStartPosition, globalEndPosition, out intersectionPoint))
        {
            print($"Line/Sphere Intersection! Global point : {intersectionPoint}");
        }

        #endregion //Line/Sphere Test
    }
}