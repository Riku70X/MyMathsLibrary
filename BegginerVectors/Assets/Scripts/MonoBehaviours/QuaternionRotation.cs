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

        //rotationQuat = new(angle, new MyVector3(1, 1, 0).NormaliseVector());
        //currentPosition = MyQuat.Rotate(startPosition, rotationQuat);
        //transform.position = currentPosition;

        // Things to ask Jay:
        // - do they need to be normalised (I think quaternions are always length 1 though)
        // - in RS formula, is v the VECTOR or the AXIS - it is ALL the vector and I WILL complain
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;

        // Using Rodrigues Rotation Formula
        //currentPosition = MyMathsLibrary.RotateVertexAroundAxis(startPosition, MyVector3.up, angle);

        // Using Quaternions
        rotationQuat = new(angle, new MyVector3(0, 0, 3));
        currentPosition = MyQuat.Rotate(startPosition, rotationQuat);

        transform.position = currentPosition;
    }
}
