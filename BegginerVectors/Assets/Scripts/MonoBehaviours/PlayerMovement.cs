using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MyVector3 constantUpVector;
    private MyVector3 relativeForwardVector;
    private MyVector3 relativeForwardVelocity;
    private MyVector3 relativeRightVector;
    private MyVector3 relativeRightVelocity;
    private MyVector3 relativeUpVector;
    private MyVector3 relativeUpVelocity;
    private MyVector3 eulerAngles;

    float speed;

    PlayerMovement()
    {
        constantUpVector = new MyVector3(0, 1, 0);
        relativeForwardVector = new MyVector3(1, 0, 0);
        relativeRightVector = new MyVector3(0, 0, -1);
        relativeUpVector = new MyVector3(0, 1, 0);
        eulerAngles = new MyVector3(0, 0, 0);
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        speed *= Time.deltaTime;

        if (Input.GetAxis("Mouse X") > 0)
        {
            eulerAngles.y += 0.01221730476f;
            transform.Rotate(new MyVector3(0, 0.7f, 0));
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            eulerAngles.y -= 0.01221730476f;
            transform.Rotate(new MyVector3(0, -0.7f, 0));
        }

        // The Camera Yaw movement changes relative to Pitch, while the Euler angle always uses the global Y-axis, so they become desynced

        if (Input.GetAxis("Mouse Y") > 0)
        {
            eulerAngles.x += 0.01221730476f;
            //transform.Rotate(new MyVector3(-0.7f, 0, 0));
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            eulerAngles.x -= 0.01221730476f;
            //transform.Rotate(new MyVector3(0.7f, 0, 0));
        }

        relativeForwardVector = MyMathsLibrary.ConvertEulerToDirection(eulerAngles);
        relativeForwardVelocity = relativeForwardVector.GetNormalisedVector() * speed;

        Debug.DrawRay(transform.position, relativeForwardVector, Color.green, 0.0f);

        relativeRightVector = MyMathsLibrary.GetCrossProduct(constantUpVector, relativeForwardVector);
        relativeRightVelocity = relativeRightVector.GetNormalisedVector() * speed;

        relativeUpVector = MyMathsLibrary.GetCrossProduct(relativeForwardVector, relativeRightVector);
        relativeUpVelocity = relativeUpVector.GetNormalisedVector() * speed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .005f);

            // Using AddVector
            //MyVector3 x = new(0, 0, 0);
            //x = MyVector3.ConvertToCustomVector(transform.position) + new MyVector3(0, 0, 0.005f);
            //transform.position = x.ConvertToUnityVector();

            // Using Euler
            transform.position += relativeForwardVelocity;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .005f);
            
            // Using Euler
            transform.position -= relativeForwardVelocity;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x + .005f, transform.position.y, transform.position.z);
            
            // Using Euler
            transform.position += relativeRightVelocity;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x - .005f, transform.position.y, transform.position.z);

            // Using Euler
            transform.position -= relativeRightVelocity;
        }
        if (Input.GetKey(KeyCode.E))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y + .005f, transform.position.z);
            
            // Using Euler
            transform.position += relativeUpVelocity;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            // Basic movement
            //transform.position = new Vector3(transform.position.x, transform.position.y - .005f, transform.position.z);
            
            // Using Euler
            transform.position -= relativeUpVelocity;
        }

        speed /= Time.deltaTime;
    }
}