using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    MyVector3 startPosition;
    MyVector3 currentPosition;
    float angle;
    MyQuat rotationQuat;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        angle = 0;

        // Using Rodrigues Rotation Formula

        //position = MyMathsLibrary.RotateVertexAroundAxis(position, MyVector3.up, angle);

        // Using Quaternions

        //rotationQuat = new(angle, new MyVector3(1, 1, 0));
        //currentPosition = MyQuat.Rotate(startPosition, rotationQuat);
        //transform.position = currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;

        // Using Rodrigues Rotation Formula
        //currentPosition = MyMathsLibrary.RotateVertexAroundAxis(startPosition, new MyVector3(1, 1, 1), angle);

        // Using Quaternions
        rotationQuat = new(angle, new MyVector3(0, 2, 0));
        currentPosition = MyMathsLibrary.RotateVectorUsingQuat(startPosition, rotationQuat);

        transform.position = currentPosition;
    }
}