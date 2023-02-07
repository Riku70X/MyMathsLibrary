using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    AABB box;
    MyVector3 startPosition;
    MyVector3 endPosition;
    MyVector3 intersectionPoint;
    bool intersected;

    MyVector3[] objectGlobalVertices;

    MyVector3 minExtent;
    MyVector3 maxExtent;

    MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        startPosition = MyVector3.zero;
        endPosition = MyVector3.one;
        intersectionPoint = MyVector3.zero;

        objectGlobalVertices = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        minExtent = objectGlobalVertices[0]; maxExtent = objectGlobalVertices[0];
    }

    // Update is called once per frame
    void Update()
    {
        objectGlobalVertices = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        

        for (int i = 0; i < objectGlobalVertices.Length; i++)
        {
            if (objectGlobalVertices[i].x < minExtent.x)
            {
                Debug.LogWarning(objectGlobalVertices[i].x + " < " + minExtent.x);
                minExtent.x = objectGlobalVertices[i].x;
            }
            else if (objectGlobalVertices[i].x > maxExtent.x)
            {
                maxExtent.x = objectGlobalVertices[i].x;
            }

            if (objectGlobalVertices[i].y < minExtent.y)
            {
                minExtent.y = objectGlobalVertices[i].y;
            }
            else if (objectGlobalVertices[i].y > maxExtent.y)
            {
                maxExtent.y = objectGlobalVertices[i].y;
            }

            if (objectGlobalVertices[i].z < minExtent.z)
            {
                minExtent.z = objectGlobalVertices[i].z;
            }
            else if (objectGlobalVertices[i].z > maxExtent.z)
            {
                maxExtent.z = objectGlobalVertices[i].z;
            }
        }

        Debug.LogError(minExtent.x);

        Debug.Log($"minimum: {minExtent.x}, {minExtent.y}, {minExtent.z}");
        Debug.Log($"maximum: {maxExtent.x}, {maxExtent.y}, {maxExtent.z}");
        box = new AABB(minExtent, maxExtent);

        intersected = AABB.LineIntersection(box, startPosition, endPosition, out intersectionPoint);
        Debug.Log(intersected);
        Debug.Log($"intersect: {intersectionPoint.x}, {intersectionPoint.y}, {intersectionPoint.z}");
    }
}
