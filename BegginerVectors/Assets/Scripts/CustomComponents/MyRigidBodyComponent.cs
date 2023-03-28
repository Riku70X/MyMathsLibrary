using UnityEngine;

public class MyRigidBodyComponent : MonoBehaviour
{
    // An object with this component NEEDS a custom transform component attatched to it
    MyTransformComponent myTransform;

    // Linear Motion
    public MyVector3 force;
    MyVector3 acceleration;
    MyVector3 velocity;
    public float mass = 1;

    // Angular Motion
    public MyVector3 torque;
    MyVector3 angularAcceleration;
    MyVector3 angularVelocity;
    public float inertia = 1;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        // Linear Motion
        acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
        myTransform.position += velocity * Time.fixedDeltaTime;

        // Angular Motion
        angularAcceleration = torque / inertia;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        MyQuat q = (angularVelocity * Time.fixedDeltaTime).ConvertEulerToQuaternion();
        MyQuat targetOrientation = q * myTransform.eulerAngles.ConvertEulerToQuaternion();

    }
}
