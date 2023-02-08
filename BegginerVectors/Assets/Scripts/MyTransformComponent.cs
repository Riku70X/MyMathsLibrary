using UnityEngine;

//[ExecuteInEditMode]
public class MyTransformComponent : MonoBehaviour
{
    public MyVector3 position;
    public MyVector3 rotation;
    public MyVector3 scale;

    public MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;

    MyVector3[] localStartVerticesCoordinates;
    MyVector3[] globalStartVerticesCoordinates;
    MyVector3[] globalCurrentVerticesCoordinates;

    public AABB boundingBox;
    MyVector3 minExtent;
    MyVector3 maxExtent;

    MyMatrix4x4 unityTranslateMatrix;
    MyMatrix4x4 unityRotateMatrix;
    MyMatrix4x4 unityScaleMatrix;
    MyMatrix4x4 unityInverseTransformMatrix;

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

        localStartVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.sharedMesh.vertices);
        globalStartVerticesCoordinates = new MyVector3[localStartVerticesCoordinates.Length];
        globalCurrentVerticesCoordinates = new MyVector3[globalStartVerticesCoordinates.Length];

        unityTranslateMatrix = MyMatrix4x4.GetTranslationMatrix(MyVector3.ConvertToCustomVector(transform.position));
        unityRotateMatrix = MyMatrix4x4.GetRotationMatrix(MyVector3.ConvertToCustomVector(transform.rotation.eulerAngles));
        unityScaleMatrix = MyMatrix4x4.GetScaleMatrix(MyVector3.ConvertToCustomVector(transform.localScale));
        unityInverseTransformMatrix = unityScaleMatrix.ScaleInverse() * unityRotateMatrix.RotationInverse() * unityTranslateMatrix.TranslationInverse();

        for (int i = 0; i < localStartVerticesCoordinates.Length; i++) 
        {
            globalStartVerticesCoordinates[i] = (unityInverseTransformMatrix * localStartVerticesCoordinates[i].ConvertToMyVector4()).ConvertToMyVector3();
        }

        Debug.Log(globalStartVerticesCoordinates[12].ToString());

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

        meshFilter.sharedMesh.vertices = MyVector3.ConvertToUnityVectorArray(globalCurrentVerticesCoordinates);

        // These final steps are sometimes necessary to make the mesh look correct
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();
    }
}