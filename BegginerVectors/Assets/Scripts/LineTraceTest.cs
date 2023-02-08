using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    GameObject Cube;
    MyTransformComponent cubeTransform;
    AABB box;

    MyMatrix4x4 scaleMatrix;
    MyMatrix4x4 rotationMatrix;
    MyMatrix4x4 translationMatrix;

    MyMatrix4x4 inverseTransformMatrix;

    [SerializeField] MyVector3 globalStartPosition;
    [SerializeField] MyVector3 globalEndPosition;

    MyVector3 localStartPosition;
    MyVector3 localEndPosition;

    MyVector3 intersectionPoint;
    bool intersected;

    public LineTraceTest()
    {
        globalStartPosition = MyVector3.zero;
        globalEndPosition = MyVector3.zero;

        inverseTransformMatrix = MyMatrix4x4.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.Find("Cube");
        cubeTransform = Cube.GetComponent<MyTransformComponent>();
        box = cubeTransform.boundingBox;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(globalStartPosition.ConvertToUnityVector(), globalEndPosition.ConvertToUnityVector());

        scaleMatrix = MyMatrix4x4.GetScaleMatrix(cubeTransform.scale);
        rotationMatrix = MyMatrix4x4.GetRotationMatrix(cubeTransform.rotation);
        translationMatrix = MyMatrix4x4.GetTranslationMatrix(cubeTransform.position);

        inverseTransformMatrix = scaleMatrix.ScaleInverse() * rotationMatrix.RotationInverse() * translationMatrix.TranslationInverse();

        localStartPosition = (inverseTransformMatrix * globalStartPosition.ConvertToMyVector4()).ConvertToMyVector3();
        localEndPosition = (inverseTransformMatrix * globalEndPosition.ConvertToMyVector4()).ConvertToMyVector3();

        Debug.LogWarning($"global: {globalStartPosition.x}, {globalStartPosition.y}, {globalStartPosition.z} to {globalEndPosition.x}, {globalEndPosition.y}, {globalEndPosition.z}");
        Debug.LogWarning($"local: {localStartPosition.x}, {localStartPosition.y}, {localStartPosition.z} to {localEndPosition.x}, {localEndPosition.y}, {localEndPosition.z}");


        intersected = AABB.LineIntersection(box, localStartPosition, localEndPosition, out intersectionPoint);

        Debug.Log($"intersected = {intersected}, point = ({intersectionPoint.x}, {intersectionPoint.y}, {intersectionPoint.z})");
    }
}