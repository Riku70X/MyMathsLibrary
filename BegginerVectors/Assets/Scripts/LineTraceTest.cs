using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    AABB box;
    MyVector3 minExtent;
    MyVector3 maxExtent;

    MyVector3 startPosition;
    MyVector3 endPosition;

    MyVector3 intersectionPoint;
    bool intersected;

    MeshFilter meshFilter;

    MyVector3[] objectGlobalVertices;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        startPosition = new MyVector3(2, 2, 2);
        endPosition = new MyVector3(2, -2, 2);

    }

    // Update is called once per frame
    void Update()
    {
        objectGlobalVertices = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);

        minExtent = new MyVector3(objectGlobalVertices[0].x, objectGlobalVertices[0].y, objectGlobalVertices[0].z);
        maxExtent = new MyVector3(objectGlobalVertices[0].x, objectGlobalVertices[0].y, objectGlobalVertices[0].z);

        for (int i = 0; i < objectGlobalVertices.Length; i++)
        {
            if (objectGlobalVertices[i].x < minExtent.x)
            {
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

        box = new AABB(minExtent, maxExtent);

        Debug.DrawLine(startPosition.ConvertToUnityVector(), endPosition.ConvertToUnityVector());

        intersected = AABB.LineIntersection(box, startPosition, endPosition, out intersectionPoint);

        Debug.Log($"intersected = {intersected}, point = ({intersectionPoint.x}, {intersectionPoint.y}, {intersectionPoint.z})");
    }
}