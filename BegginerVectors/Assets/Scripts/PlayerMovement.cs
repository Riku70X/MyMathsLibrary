using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MyVector3 constantUpVector;
    MyVector3 relativeForwardVector;
    MyVector3 relativeForwardVelocity;
    MyVector3 relativeRightVector;
    MyVector3 relativeRightVelocity;
    MyVector3 relativeUpVector;
    MyVector3 relativeUpVelocity;
    MyVector3 eulerAngles;

    float speed;

    PlayerMovement()
    {
        constantUpVector = new MyVector3(0, 1, 0);
        relativeForwardVector = new MyVector3(1, 0, 0);
        relativeRightVector = new MyVector3(0, 0, -1);
        relativeUpVector = new MyVector3(0, 1, 0);
        eulerAngles = new MyVector3(0, 0, 0);
        speed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") > 0)
        {
            eulerAngles.y -= .025f;
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            eulerAngles.y += .025f;
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            eulerAngles.x += .025f;
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            eulerAngles.x -= .025f;
        }

        relativeForwardVector = MathsLibrary.ConvertEulerToDirection(eulerAngles);
        relativeForwardVelocity = relativeForwardVector.NormaliseVector() * speed;

        relativeRightVector = MathsLibrary.GetCrossProduct(constantUpVector, relativeForwardVector);
        relativeRightVelocity = relativeRightVector.NormaliseVector() * speed;

        relativeUpVector = MathsLibrary.GetCrossProduct(relativeForwardVector, relativeRightVector);
        relativeUpVelocity = relativeUpVector.NormaliseVector() * speed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .005f);

            // Using AddVector
            //MyVector3 x = new MyVector3(0, 0, 0);
            //x = MyVector3.ConvertToCustomVector(transform.position) + new MyVector3(0, 0, 0.005f);
            //transform.position = x.ConvertToUnityVector();

            // Using Euler
            transform.position += relativeForwardVelocity.ConvertToUnityVector();
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .005f);
            
            // Using Euler
            transform.position -= relativeForwardVelocity.ConvertToUnityVector();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x + .005f, transform.position.y, transform.position.z);
            
            // Using Euler
            transform.position += relativeRightVelocity.ConvertToUnityVector();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x - .005f, transform.position.y, transform.position.z);
            
            // Using Euler
            transform.position -= relativeRightVelocity.ConvertToUnityVector();
        }
        if (Input.GetKey(KeyCode.E))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y + .005f, transform.position.z);
            
            // Using Euler
            transform.position += relativeUpVelocity.ConvertToUnityVector();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y - .005f, transform.position.z);
            
            // Using Euler
            transform.position -= relativeUpVelocity.ConvertToUnityVector();
        }
    }
}