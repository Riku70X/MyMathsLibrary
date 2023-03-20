using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 intersectionPoint;

    GameObject Cube;
    MyTransformComponent cubeTransform;
    MyAABB localBox;

    MyMatrix4x4 scaleMatrix;
    MyMatrix4x4 rotationMatrix;
    MyMatrix4x4 translationMatrix;

    MyMatrix4x4 inverseTransformMatrix;

    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    GameObject Sphere;
    MyTransformComponent sphereTransform;
    MyBoundingSphere boundingSphere;

    float sphereLineDistanceSq;
    float radiusSq;

    MyVector3 sphereProjection;
    float projectionToIntersectionLengthSq;
    MyVector3 projectionToStart;
    float scalar;

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

        scaleMatrix = MyMathsLibrary.GetScaleMatrix(cubeTransform.scale);
        rotationMatrix = MyMathsLibrary.GetRotationMatrix(cubeTransform.rotation);
        translationMatrix = MyMathsLibrary.GetTranslationMatrix(cubeTransform.position);

        inverseTransformMatrix = scaleMatrix.ScaleInverse() * rotationMatrix.RotationInverse() * translationMatrix.TranslationInverse();

        localStartPosition = inverseTransformMatrix * globalStartPosition;
        localEndPosition = inverseTransformMatrix * globalEndPosition;
        
        if (MyMathsLibrary.LineIntersectsAABB(localBox, localStartPosition, localEndPosition, out intersectionPoint))
        {
            print($"Line/Box Intersection! Local point : {intersectionPoint}, Global point : {new MyVector3(cubeTransform.getTransformMatrix * intersectionPoint)}");
        }

        #endregion //Line/Box Test

        #region Line/Sphere Test

        boundingSphere = new MyBoundingSphere(sphereTransform);

        sphereLineDistanceSq = MyMathsLibrary.GetShortestDistanceSq(globalStartPosition, globalEndPosition, boundingSphere.getCentrepoint);
        radiusSq = boundingSphere.getRadius * boundingSphere.getRadius;

        Debug.LogWarning($"centre:ineDSq: {sphereLineDistanceSq}");
        Debug.LogWarning($"radiusSq: {radiusSq}");

        if (sphereLineDistanceSq < radiusSq)
        {
            sphereProjection = MyMathsLibrary.GetClosestPointOnLineSegment(boundingSphere.getCentrepoint, globalStartPosition, globalEndPosition);
            projectionToIntersectionLengthSq = radiusSq - sphereLineDistanceSq;
            projectionToStart = globalStartPosition - sphereProjection;
            scalar = projectionToIntersectionLengthSq / projectionToStart.GetVectorLengthSquared();
            scalar = Mathf.Sqrt(scalar);
            intersectionPoint = sphereProjection + (projectionToStart * scalar);

            print($"Line/Sphere Intersection! Global point : {intersectionPoint}");
        }

        #endregion //Line/Sphere Test
    }
}