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
    MyVector3 externalForces;
    MyVector3 externalTorques;

    public bool usingGravity = true;

    public bool usingAirResistance = true;
    public float dragCoefficient = 1; // Also contains the surface Area of the object
    const float airDensity = 1.293f; // 1.293 kg / m^3

    MyRigidBodyComponent()
    {
        force = MyVector3.zero;
        acceleration = MyVector3.zero;
        velocity = MyVector3.zero;

        torque = MyVector3.zero;
        angularAcceleration = MyVector3.zero;
        angularVelocity = MyVector3.zero;

        externalForces = MyVector3.zero;
        externalTorques = MyVector3.zero;
    }

    public void AddForce(MyVector3 force)
    {
        externalForces += force;
    }

    public void AddTorque(MyVector3 torque)
    {
        externalTorques += torque;
    }

    public void AddForceAtLocation(MyVector3 force, MyVector3 pointOfImpact)
    {
        MyVector3 centreOfMass = myTransform.position;
        MyVector3 impactToCentre = centreOfMass - pointOfImpact;
        MyVector3 torque = MyMathsLibrary.GetCrossProduct(force, impactToCentre);

        externalForces += force;
        externalTorques += torque;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        force = MyVector3.zero;
        torque = MyVector3.zero;

        if (usingGravity)
            force.y -= 9.81f * mass;

        if (usingAirResistance)
        {
            float speedSquared = velocity.GetVectorLengthSquared();
            MyVector3 dragDirection = -velocity.GetNormalisedVector();

            MyVector3 dragForce = 0.5f * airDensity * speedSquared * dragCoefficient * dragDirection;

            force += dragForce;
        }

        // Calculate external forces
        force += externalForces;
        torque += externalTorques;
        externalForces = MyVector3.zero;
        externalTorques = MyVector3.zero;

        // Linear Motion
        acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
        myTransform.position += velocity * Time.fixedDeltaTime;

        // Angular Motion
        angularAcceleration = torque / inertia;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        MyQuat quaternionVelocity = (angularVelocity * Time.fixedDeltaTime).ConvertEulerToQuaternion(); //quaternionVelocity moreso represents the vector, rather than the velocity.
        myTransform.newRotation = quaternionVelocity * myTransform.rotation;
        if (myTransform.newRotation != myTransform.rotation)
            myTransform.spinning = true;
    }
}
