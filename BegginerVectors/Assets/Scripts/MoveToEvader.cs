using UnityEngine;

public class MoveToEvader : MonoBehaviour
{
    private GameObject evader;

    private MyVector3 currentPersuerPosition;
    private MyVector3 currentEvaderPosition;
    private MyVector3 currentEvaderDirection;
    private MyVector3 normalisedEvaderDirection;

    [SerializeField] private float speed;

    private MyVector3 previousEvaderPosition;
    private MyVector3 currentEvaderVelocity;

    MoveToEvader()
    {
        speed = 0.003f;
    }

    void MoveTowardsEvader()
    {
        currentPersuerPosition = MyVector3.ConvertToCustomVector(transform.position);
        currentEvaderPosition = MyVector3.ConvertToCustomVector(evader.transform.position);
        currentEvaderDirection = MyVector3.SubtractVector(currentEvaderPosition, currentPersuerPosition);
        currentEvaderVelocity = MyVector3.SubtractVector(currentEvaderPosition, previousEvaderPosition);

        if (MyVector3.GetDotProduct(currentEvaderDirection, currentEvaderVelocity) > 0)
        {
            normalisedEvaderDirection = currentEvaderDirection.NormaliseVector();
            currentPersuerPosition = MyVector3.AddVector(currentPersuerPosition, MyVector3.MultiplyVector(normalisedEvaderDirection, speed));
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
        //if (Input.GetKey(KeyCode.Space))
        {
            MoveTowardsEvader();
        }
    }
}