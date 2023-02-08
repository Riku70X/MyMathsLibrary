using UnityEngine;

public class MyTransformComponent : MonoBehaviour
{
    public MyVector3 position;
    public MyVector3 rotation;
    public MyVector3 scale;

    public MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;

    MyVector3[] globalStartVerticesCoordinates;
    MyVector3[] globalCurrentVerticesCoordinates;

    public AABB boundingBox;
    MyVector3 minExtent;
    MyVector3 maxExtent;

    public MyTransformComponent()
    {
        position = MyVector3.zero;
        rotation = MyVector3.zero;
        scale = MyVector3.one;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        globalStartVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        globalCurrentVerticesCoordinates = new MyVector3[globalStartVerticesCoordinates.Length];

        minExtent = new MyVector3(globalStartVerticesCoordinates[0].x, globalStartVerticesCoordinates[0].y, globalStartVerticesCoordinates[0].z);
        maxExtent = new MyVector3(globalStartVerticesCoordinates[0].x, globalStartVerticesCoordinates[0].y, globalStartVerticesCoordinates[0].z);

        for (int i = 0; i < globalStartVerticesCoordinates.Length; i++)
        {
            if (globalStartVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = globalStartVerticesCoordinates[i].x;
            }
            else if (globalStartVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = globalStartVerticesCoordinates[i].x;
            }

            if (globalStartVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = globalStartVerticesCoordinates[i].y;
            }
            else if (globalStartVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = globalStartVerticesCoordinates[i].y;
            }

            if (globalStartVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = globalStartVerticesCoordinates[i].z;
            }
            else if (globalStartVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = globalStartVerticesCoordinates[i].z;
            }
        }

        boundingBox = new AABB(minExtent, maxExtent);
    }

    // Update is called once per frame
    void Update()
    {
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