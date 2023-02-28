using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    private MyVector3 currentPosition;
    private MyVector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = new MyVector3(5, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        currentPosition = MyMathsLibrary.GetLerp(currentPosition, destination, Time.deltaTime);
        transform.position = currentPosition;
    }
}