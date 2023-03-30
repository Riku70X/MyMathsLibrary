using UnityEngine;

[ExecuteInEditMode]
public class MyTransformComponent : MonoBehaviour
{
    public MyVector3 position;
    public MyVector3 eulerAngles;
    public MyVector3 scale;

    [HideInInspector] public MyQuat rotation;
    MyMatrix4x4 transformMatrix;

    [HideInInspector] public bool spinning;
    [HideInInspector] public MyQuat newRotation;

    MeshFilter meshFilter;
    public Mesh mesh; // NEEDS TO BE PUBLIC

    MyVector3[] localVerticesCoordinates;
    MyVector3[] globalVerticesCoordinates;

    public MyMatrix4x4 getTransformMatrix => transformMatrix;
    public MyVector3[] getGlobalVerticesCoordinates => globalVerticesCoordinates;

    MyTransformComponent()
    {
        position = MyVector3.zero;
        eulerAngles = MyVector3.zero;
        scale = MyVector3.one;

        rotation = MyQuat.identity;
        transformMatrix = MyMatrix4x4.identity;

        spinning = false;
    }

    void CalculateAngularVelocity()
    {
        if (spinning)
        {
            rotation = newRotation;
            spinning = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        // make a copy of the shared Mesh to prevent corrupting Unity's pre-made meshes
        meshFilter.sharedMesh = Instantiate(mesh);

        localVerticesCoordinates = MyMathsLibrary.ConvertToCustomVectorArray(meshFilter.sharedMesh.vertices);
        globalVerticesCoordinates = MyMathsLibrary.ConvertToCustomVectorArray(meshFilter.sharedMesh.vertices);
    }

    // Update is called once per frame
    void Update()
    {
        rotation = eulerAngles.ConvertEulerToQuaternion();

        // The angularVelocity will only increase on fixed frame updates
        // put rotation + angularforce here
        CalculateAngularVelocity();

        transformMatrix = MyMathsLibrary.GetTransformationMatrixUsingQuat(scale, rotation, position);

        for (int i = 0; i < localVerticesCoordinates.Length; i++)
        {
            globalVerticesCoordinates[i] = transformMatrix * localVerticesCoordinates[i];
        }

        meshFilter.sharedMesh.vertices = MyMathsLibrary.ConvertToUnityVectorArray(globalVerticesCoordinates);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();

        // fix dirty variables (currently prevents manually rotating on the y-axis)
        eulerAngles = rotation.ConvertToEulerAngles();
    }
}