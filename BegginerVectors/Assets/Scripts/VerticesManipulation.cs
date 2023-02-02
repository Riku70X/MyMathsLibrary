using UnityEngine;

public class VerticesManipulation : MonoBehaviour
{
    MyVector3[] ModelSpaceVertices;

    // Start is called before the first frame update
    void Start()
    {
        // Mesh filter is a component which stores information about the current mesh
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // We get a copy of all the vertices
        ModelSpaceVertices = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
