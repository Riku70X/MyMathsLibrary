using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    MyVector3 position;
    float angle;
    MyQuat rotationQuat;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        angle = 0;
        rotationQuat = new(angle, MyVector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime;
        position = MyQuat.Rotate(position, rotationQuat);
        transform.position = position;
    }
}
