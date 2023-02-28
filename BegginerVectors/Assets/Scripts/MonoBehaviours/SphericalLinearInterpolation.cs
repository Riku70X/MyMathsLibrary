using UnityEngine;

public class SphericalLinearInterpolation : MonoBehaviour
{
    MeshFilter meshFilter;

    MyVector3[] startingVerticesCoordinates;
    MyVector3[] currentVerticesCoordinates;

    MyQuat rotationQuaternion;
    float angle;

    SphericalLinearInterpolation()
    {
        angle = 0;
        rotationQuaternion = new(0, new Vector3(0, 1, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        startingVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        currentVerticesCoordinates = new MyVector3[startingVerticesCoordinates.Length];
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime;
        rotationQuaternion = new(angle, MyVector3.up);

        for (int i = 0; i < startingVerticesCoordinates.Length; i++)
        {
            currentVerticesCoordinates[i] = MyMathsLibrary.RotateVectorUsingQuat(startingVerticesCoordinates[i], rotationQuaternion);
        }

        meshFilter.mesh.vertices = MyVector3.ConvertToUnityVectorArray(currentVerticesCoordinates);
    }
}