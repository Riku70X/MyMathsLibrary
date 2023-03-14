using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    MyVector3 startPosition;
    MyVector3 currentPosition;
    float angle;
    MyQuat rotationQuat;
    [SerializeField] float speed;

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
        //currentPosition = MyMathsLibrary.RotateVertexAroundAxis(startPosition, MyVector3.up, angle);

        // Using Quaternions
        rotationQuat = new(angle, new MyVector3(2, 3, 4));
        currentPosition = MyMathsLibrary.RotateVectorUsingQuat(startPosition, rotationQuat);

        transform.position = currentPosition;
    }
}