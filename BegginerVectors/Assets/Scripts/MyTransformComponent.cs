using UnityEngine;

public class MyTransformComponent : MonoBehaviour
{
    MyVector3 position;
    MyVector3 rotation;
    MyVector3 scale;

    MyVector3 previousPosition;
    MyVector3 previousRotation;
    MyVector3 previousScale;

    MyVector3 velocityPosition;
    MyVector3 velocityRotation;
    MyVector3 velocityScale;

    MyMatrix4x4 transformMatrix;

    MeshFilter meshFilter;

    MyVector3[] globalVerticesCoordinates;

    float speed;

    public MyTransformComponent()
    {
        position = MyVector3.zero;
        rotation = MyVector3.zero;
        scale = MyVector3.one;

        previousPosition = MyVector3.zero;
        previousRotation = MyVector3.zero;
        previousScale = MyVector3.one;

        velocityPosition = MyVector3.zero;

        transformMatrix = MyMatrix4x4.identity;

        speed = 9;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        globalVerticesCoordinates = MyVector3.ConvertToCustomVectorArray(meshFilter.mesh.vertices);
    }

    // Update is called once per frame
    void Update()
    {
        //speed *= Time.deltaTime;

        // PLAYER INPUT

        Debug.Log("previousPositionX: " + position.x);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            position.z += speed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            position.z -= speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed;
            velocityPosition.x = speed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed;
        }

        Debug.Log("positionX: " + position.x);

        // POSITION CALCULATIONS

        //velocityPosition = position - previousPosition;
        velocityRotation = rotation - previousRotation;
        velocityScale = scale - previousScale;

        Debug.Log("velocityX: " + velocityPosition.x);
        
        

        transformMatrix = MyMatrix4x4.GetTransformationMatrix(velocityScale, velocityRotation, velocityPosition);

        for (int i = 0; i < globalVerticesCoordinates.Length; i++)
        {
            globalVerticesCoordinates[i] = (transformMatrix * globalVerticesCoordinates[i].ConvertToMyVector4()).ConvertToMyVector3();
        }

        // CYCLE CALCULATIONS

        if (previousPosition != position)
        {
            previousPosition = position;
        }
        previousRotation = rotation;
        previousScale = scale;

        //speed /= Time.deltaTime;
    }
}
