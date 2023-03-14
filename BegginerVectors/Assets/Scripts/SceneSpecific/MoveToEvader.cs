using UnityEngine;

public class MoveToEvader : MonoBehaviour
{
    GameObject evader;

    MyVector3 currentPersuerPosition;
    MyVector3 currentEvaderPosition;
    MyVector3 currentEvaderDirection;
    MyVector3 normalisedEvaderDirection;

    float speed;

    MyVector3 previousEvaderPosition;
    MyVector3 currentEvaderVelocity;

    MoveToEvader()
    {
        speed = 3f;
    }

    void MoveTowardsEvader()
    {
        currentPersuerPosition = transform.position;
        currentEvaderPosition = evader.transform.position;
        currentEvaderDirection = currentEvaderPosition - currentPersuerPosition;
        currentEvaderVelocity = currentEvaderPosition - previousEvaderPosition;

        if (MyMathsLibrary.GetDotProduct(currentEvaderDirection, currentEvaderVelocity) > 0)
        {
            normalisedEvaderDirection = currentEvaderDirection.GetNormalisedVector();
            currentPersuerPosition += (normalisedEvaderDirection * speed);
            transform.position = currentPersuerPosition;
        }

        previousEvaderPosition = currentEvaderPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        evader = GameObject.Find("Evader");
        previousEvaderPosition = evader.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        speed *= Time.deltaTime;
        //if (Input.GetKey(KeyCode.Space))
        {
            MoveTowardsEvader();
        }
        speed /= Time.deltaTime;
    }
}