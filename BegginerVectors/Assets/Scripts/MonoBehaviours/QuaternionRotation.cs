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
        angle = Mathf.PI/2;
        rotationQuat = new(angle, MyVector3.up);
        position = MyQuat.Rotate(position, rotationQuat);
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"axis: {rotationQuat.GetAxis()}"); // issue is that x y and z are calculated using *=sin(angle), so if angle is 0 then I can't /= xyz to find axis.
        //angle += Time.deltaTime;
        //rotationQuat = new(angle, MyVector3.up);
        //position = MyQuat.Rotate(position, rotationQuat);
        //transform.position = position;
    }
}
