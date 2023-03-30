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

    public bool usingGravity = true;

    MyRigidBodyComponent()
    {
        force = MyVector3.zero;
        acceleration = MyVector3.zero;
        velocity = MyVector3.zero;

        torque = MyVector3.zero;
        angularAcceleration = MyVector3.zero;
        angularVelocity = MyVector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        if (usingGravity)
            force.y = -9.81f * mass;

        // Linear Motion
        acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
        myTransform.position += velocity * Time.fixedDeltaTime;

        // Angular Motion
        angularAcceleration = torque / inertia;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        MyQuat quaternionVelocity = (angularVelocity * Time.fixedDeltaTime).ConvertEulerToQuaternion();
        myTransform.newRotation = quaternionVelocity * myTransform.rotation;
        if (myTransform.newRotation != myTransform.rotation)
            myTransform.spinning = true;
    }
}