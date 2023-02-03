using UnityEngine;

public class VerticesManipulation : MonoBehaviour
{
    MyVector3[] modelSpaceVertices;

    // Mesh filter is a component which stores information about the current mesh
    MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        // We get a copy of all the vertices
        modelSpaceVertices = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
    }

    // Update is called once per frame
    void Update()
    {
        // Define a new array with the correct size
        MyVector3[] transformedVertices = new MyVector3[modelSpaceVertices.Length];

        // Transform each individual vertex
        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = MyMatrix4x4.ScaleVector(modelSpaceVertices[i], new MyVector3(1, 1, 1));
        }

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = MyMatrix4x4.RotateVector(modelSpaceVertices[i], 0, 0, 0);
        }

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = MyMatrix4x4.TranslateVector(modelSpaceVertices[i], new MyVector3(0, 0, 0));
        }

        // Assign our new vertices
        meshFilter.mesh.vertices = MyVector3.ConvertToUnityVectorArray(transformedVertices);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}