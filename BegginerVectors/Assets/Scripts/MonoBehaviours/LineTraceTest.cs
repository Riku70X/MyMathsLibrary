using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    GameObject Cube;
    MyTransformComponent cubeTransform;
    MyAABB localBox;
    MyAABB globalBox;

    MyMatrix4x4 scaleMatrix;
    MyMatrix4x4 rotationMatrix;
    MyMatrix4x4 translationMatrix;

    MyMatrix4x4 inverseTransformMatrix;

    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    MyVector3 intersectionPoint;

    GameObject OtherCube;
    MyTransformComponent OtherCubeTransform;
    MyAABB otherBox;

    public LineTraceTest()
    {
        globalStartPosition = MyVector3.zero;
        globalEndPosition = MyVector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube");
        cubeTransform = Cube.GetComponent<MyTransformComponent>();
        localBox = cubeTransform.localBoundingBox;

        OtherCube = GameObject.Find("OtherCube");
        OtherCubeTransform = OtherCube.GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        globalBox = cubeTransform.globalBoundingBox;
        otherBox = OtherCubeTransform.globalBoundingBox;

        Debug.DrawLine(globalStartPosition, globalEndPosition);

        scaleMatrix = MyMathsLibrary.GetScaleMatrix(cubeTransform.scale);
        rotationMatrix = MyMathsLibrary.GetRotationMatrix(cubeTransform.rotation);
        translationMatrix = MyMathsLibrary.GetTranslationMatrix(cubeTransform.position);

        inverseTransformMatrix = scaleMatrix.ScaleInverse() * rotationMatrix.RotationInverse() * translationMatrix.TranslationInverse();

        localStartPosition = inverseTransformMatrix * globalStartPosition;
        localEndPosition = inverseTransformMatrix * globalEndPosition;
        
        if (MyAABB.LineIntersection(localBox, localStartPosition, localEndPosition, out intersectionPoint))
        {
            Debug.Log($"Line Intersection! Local point : {intersectionPoint}, Global point : {cubeTransform.transformMatrix * intersectionPoint}");
        }

        if (MyAABB.Intersects(globalBox, otherBox))
        {
            Debug.Log($"Box Intersection!");
        }
    }
}