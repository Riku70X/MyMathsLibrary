using UnityEngine;

[ExecuteInEditMode]
public class MyTransformComponent : MonoBehaviour
{
    public MyVector3 position;
    public MyVector3 rotation;
    public MyVector3 scale;

    public MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;
    public Mesh mesh; // NEEDS TO BE PUBLIC

    MyVector3[] localVerticesCoordinates;
    public MyVector3[] globalVerticesCoordinates; // MyAABB needs to access this

    MyTransformComponent()
    {
        position = MyVector3.zero;
        rotation = MyVector3.zero;
        scale = MyVector3.one;
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
        transformMatrix = MyMathsLibrary.GetTransformationMatrixUsingQuat(scale, rotation, position);

        for (int i = 0; i < localVerticesCoordinates.Length; i++)
        {
            globalVerticesCoordinates[i] = transformMatrix * localVerticesCoordinates[i];
        }

        meshFilter.sharedMesh.vertices = MyMathsLibrary.ConvertToUnityVectorArray(globalVerticesCoordinates);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();
    }
}