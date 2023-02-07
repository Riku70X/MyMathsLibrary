using UnityEngine;

public class MyTransformComponent : MonoBehaviour
{
    [SerializeField] MyVector3 position;
    [SerializeField] MyVector3 rotation;
    [SerializeField] MyVector3 scale;

    MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;

    MyVector3[] globalStartVerticesCoordinates;
    MyVector3[] globalCurrentVerticesCoordinates;

    public MyTransformComponent()
    {
        position = MyVector3.zero;
        rotation = MyVector3.zero;
        scale = MyVector3.one;

        transformMatrix = MyMatrix4x4.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        globalStartVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        globalCurrentVerticesCoordinates = new MyVector3[globalStartVerticesCoordinates.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // POSITION CALCULATIONS

        transformMatrix = MyMatrix4x4.GetTransformationMatrix(scale, rotation, position);

        for (int i = 0; i < globalStartVerticesCoordinates.Length; i++)
        {
            globalCurrentVerticesCoordinates[i] = (transformMatrix * globalStartVerticesCoordinates[i].ConvertToMyVector4()).ConvertToMyVector3();
        }

        meshFilter.mesh.vertices = MyVector3.ConvertToUnityVectorArray(globalCurrentVerticesCoordinates);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}