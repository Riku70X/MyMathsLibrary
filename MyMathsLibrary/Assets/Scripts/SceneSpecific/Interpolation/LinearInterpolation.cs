using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    MyVector3 startingPosition;
    MyVector3 currentPosition;
    MyVector3 destination;

    //float slerpTime;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        destination = new MyVector3(5, 2, 0);

        //slerpTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;

        // constant velocity
        //slerpTime += Time.deltaTime;
        //currentPosition = MyMathsLibrary.GetLerp(startingPosition, destination, slerpTime);

        // variable velocity
        currentPosition = MyMathsLibrary.GetLerp(currentPosition, destination, Time.deltaTime / 2);

        transform.position = currentPosition;
    }
}