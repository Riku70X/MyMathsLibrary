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

        //MyMatrix4x4 transformMatrix = MyMatrix4x4.GetTransformationMatrix(new MyVector3(2, 1, 1), new MyVector3(0, 0, Mathf.PI / 2), new MyVector3(1, 0, 0));

        // Transform each individual vertex
        for (int i = 0; i < transformedVertices.Length; i++)
        {
            transformedVertices[i] = MyMatrix4x4.TransformVector(modelSpaceVertices[i], new MyVector3(3, 1, 1), new MyVector3(0, 0, Mathf.PI/2), new MyVector3(2, 0, 0));
            //transformedVertices[i] = (transformMatrix * modelSpaceVertices[i].ConvertToMyVector4()).ConvertToMyVector3();
        }

        // Assign our new vertices
        meshFilter.mesh.vertices = MyVector3.ConvertToUnityVectorArray(transformedVertices);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}