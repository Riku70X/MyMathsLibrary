using UnityEngine;

public class SphericalLinearInterpolation : MonoBehaviour
{
    MeshFilter meshFilter;

    MyVector3[] startingVerticesCoordinates;
    MyVector3[] currentVerticesCoordinates;

    MyQuat rotationQuaternion;

    MyQuat destinationQuaternion;

    MyVector3 startingOrientation;
    MyVector3 targetOrientation;

    SphericalLinearInterpolation()
    {
        startingOrientation = new(0, 0, 0);
        targetOrientation = new(3.14f/2, 3.14f/2, 0);

        rotationQuaternion = startingOrientation.ConvertEulerToQuaternion();
        destinationQuaternion = targetOrientation.ConvertEulerToQuaternion();
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        startingVerticesCoordinates = MyMathsLibrary.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
        currentVerticesCoordinates = new MyVector3[startingVerticesCoordinates.Length];
    }

    // Update is called once per frame
    void Update()
    {
        rotationQuaternion = MyMathsLibrary.SLERP(rotationQuaternion, destinationQuaternion, Time.deltaTime/10);

        for (int i = 0; i < startingVerticesCoordinates.Length; i++)
        {
            currentVerticesCoordinates[i] = MyMathsLibrary.RotateVectorUsingQuat(startingVerticesCoordinates[i], rotationQuaternion);
        }

        meshFilter.mesh.vertices = MyMathsLibrary.ConvertToUnityVectorArray(currentVerticesCoordinates);
    }
}