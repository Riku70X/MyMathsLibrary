using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    GameObject Cube;
    MyTransformComponent cubeTransform;
    MyAABB localBox;

    MyMatrix4x4 scaleMatrix;
    MyMatrix4x4 rotationMatrix;
    MyMatrix4x4 translationMatrix;

    MyMatrix4x4 inverseTransformMatrix;

    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    MyVector3 intersectionPoint;

    LineTraceTest()
    {
        globalStartPosition = MyVector3.zero;
        globalEndPosition = MyVector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube");
        cubeTransform = Cube.GetComponent<MyTransformComponent>();
        localBox = new MyAABB(cubeTransform);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(globalStartPosition, globalEndPosition);

        scaleMatrix = MyMathsLibrary.GetScaleMatrix(cubeTransform.scale);
        rotationMatrix = MyMathsLibrary.GetRotationMatrix(cubeTransform.rotation);
        translationMatrix = MyMathsLibrary.GetTranslationMatrix(cubeTransform.position);

        inverseTransformMatrix = scaleMatrix.ScaleInverse() * rotationMatrix.RotationInverse() * translationMatrix.TranslationInverse();

        localStartPosition = inverseTransformMatrix * globalStartPosition;
        localEndPosition = inverseTransformMatrix * globalEndPosition;
        
        if (MyMathsLibrary.LineIntersectsAABB(localBox, localStartPosition, localEndPosition, out intersectionPoint))
        {
            Debug.Log($"Line Intersection! Local point : {intersectionPoint}, Global point : {new MyVector3(cubeTransform.transformMatrix * intersectionPoint)}");
        }
    }
}