using UnityEngine;

//[ExecuteInEditMode]
public class MyTransformComponent : MonoBehaviour
{
    public MyVector3 position;
    public MyVector3 rotation;
    public MyVector3 scale;

    public MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;

    MyVector3[] localVerticesCoordinates;
    MyVector3[] globalVerticesCoordinates;

    public AABB localBoundingBox;
    public AABB globalBoundingBox;
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
        Debug.LogWarning("Ask Jay about sharedMesh");

        meshFilter = GetComponent<MeshFilter>();

        localVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        globalVerticesCoordinates = new MyVector3[localVerticesCoordinates.Length];

        minExtent = new MyVector3(localVerticesCoordinates[0].x, localVerticesCoordinates[0].y, localVerticesCoordinates[0].z);
        maxExtent = new MyVector3(localVerticesCoordinates[0].x, localVerticesCoordinates[0].y, localVerticesCoordinates[0].z);

        for (int i = 0; i < localVerticesCoordinates.Length; i++)
        {
            if (localVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = localVerticesCoordinates[i].x;
            }
            else if (localVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = localVerticesCoordinates[i].x;
            }

            if (localVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = localVerticesCoordinates[i].y;
            }
            else if (localVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = localVerticesCoordinates[i].y;
            }

            if (localVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = localVerticesCoordinates[i].z;
            }
            else if (localVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = localVerticesCoordinates[i].z;
            }
        }

        localBoundingBox = new AABB(minExtent, maxExtent);
        globalBoundingBox = new AABB(minExtent, maxExtent);
    }

    // Update is called once per frame
    void Update()
    {
        transformMatrix = MyMatrix4x4.GetTransformationMatrix(scale, rotation, position);

        for (int i = 0; i < localVerticesCoordinates.Length; i++)
        {
            globalVerticesCoordinates[i] = (transformMatrix * localVerticesCoordinates[i].ConvertToMyVector4()).ConvertToMyVector3();
        }

        minExtent = new MyVector3(globalVerticesCoordinates[0].x, globalVerticesCoordinates[0].y, globalVerticesCoordinates[0].z);
        maxExtent = new MyVector3(globalVerticesCoordinates[0].x, globalVerticesCoordinates[0].y, globalVerticesCoordinates[0].z);

        for (int i = 0; i < globalVerticesCoordinates.Length; i++)
        {
            if (globalVerticesCoordinates[i].x < minExtent.x)
            {
                minExtent.x = globalVerticesCoordinates[i].x;
            }
            else if (globalVerticesCoordinates[i].x > maxExtent.x)
            {
                maxExtent.x = globalVerticesCoordinates[i].x;
            }

            if (globalVerticesCoordinates[i].y < minExtent.y)
            {
                minExtent.y = globalVerticesCoordinates[i].y;
            }
            else if (globalVerticesCoordinates[i].y > maxExtent.y)
            {
                maxExtent.y = globalVerticesCoordinates[i].y;
            }

            if (globalVerticesCoordinates[i].z < minExtent.z)
            {
                minExtent.z = globalVerticesCoordinates[i].z;
            }
            else if (globalVerticesCoordinates[i].z > maxExtent.z)
            {
                maxExtent.z = globalVerticesCoordinates[i].z;
            }
        }
        
        globalBoundingBox = new AABB(minExtent, maxExtent);

        meshFilter.mesh.vertices = MyVector3.ConvertToUnityVectorArray(globalVerticesCoordinates);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }
}