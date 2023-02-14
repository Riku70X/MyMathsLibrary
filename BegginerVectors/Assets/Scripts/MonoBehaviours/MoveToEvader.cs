using UnityEngine;

public class MoveToEvader : MonoBehaviour
{
    private GameObject evader;

    private MyVector3 currentPersuerPosition;
    private MyVector3 currentEvaderPosition;
    private MyVector3 currentEvaderDirection;
    private MyVector3 normalisedEvaderDirection;

    private float speed;

    private MyVector3 previousEvaderPosition;
    private MyVector3 currentEvaderVelocity;

    MoveToEvader()
    {
        speed = 5f;
    }

    void MoveTowardsEvader()
    {
        currentPersuerPosition = MyVector3.ConvertToCustomVector(transform.position);
        currentEvaderPosition = MyVector3.ConvertToCustomVector(evader.transform.position);
        currentEvaderDirection = currentEvaderPosition - currentPersuerPosition;
        currentEvaderVelocity = currentEvaderPosition - previousEvaderPosition;

        if (MyVector3.GetDotProduct(currentEvaderDirection, currentEvaderVelocity) > 0)
        {
            normalisedEvaderDirection = currentEvaderDirection.NormaliseVector();
            currentPersuerPosition += (normalisedEvaderDirection * speed);
            transform.position = currentPersuerPosition.ConvertToUnityVector();
        }

        previousEvaderPosition = currentEvaderPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        evader = GameObject.Find("Evader");
        previousEvaderPosition = MyVector3.ConvertToCustomVector(evader.transform.position);
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