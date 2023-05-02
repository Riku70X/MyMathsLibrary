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

    // External Forces
    MyVector3 externa

    public bool usingGravity = true;
    public bool usingAirResistance = true;

    const float airDensity = 1.293f; // 1.293 kg / m^3

    MyRigidBodyComponent()
    {
        force = MyVector3.zero;
        acceleration = MyVector3.zero;
        velocity = MyVector3.zero;

        torque = MyVector3.zero;
        angularAcceleration = MyVector3.zero;
        angularVelocity = MyVector3.zero;
    }

    public void AddForce(MyVector3 force)
    {
        this.force += force;
    }

    public void AddTorque(MyVector3 torque)
    {
        this.torque += torque;
    }

    public void AddForceAtLocation(MyVector3 force, MyVector3 pointOfImpact)
    {
        MyVector3 centreOfMass = myTransform.position;
        MyVector3 impactToCentre = centreOfMass - pointOfImpact;
        MyVector3 torque = MyMathsLibrary.GetCrossProduct(force, impactToCentre);

        this.force += force;
        this.torque += torque;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        if (usingGravity && force.y > -9.81f)
            force.y -= 9.81f * mass * Time.fixedDeltaTime;

        if (usingAirResistance)
        {
            float speedSquared = velocity.GetVectorLengthSquared();
            MyVector3 dragDirection = -velocity.GetNormalisedVector();

            MyVector3 dragForce = 0.5f * airDensity * speedSquared * dragDirection;
            MyVector3 newVelocity = velocity + dragForce / mass;

            if (dragForce.x * newVelocity.x < 0)
                dragForce.x = 0;
            if (dragForce.y * newVelocity.y < 0)
                dragForce.y = 0;
            if (dragForce.z * newVelocity.z < 0)
                dragForce.z = 0;

            Debug.LogWarning($"{dragForce.x} * {newVelocity.x}, {dragForce}");
            Debug.Log(dragForce);

            force += dragForce /* * Time.fixedDeltaTime*/;
        }

        Debug.LogError(force);

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
