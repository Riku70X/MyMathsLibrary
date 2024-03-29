using UnityEngine;

public class MyRigidBodyComponent : MonoBehaviour
{
    // An object with this component NEEDS a custom transform component attatched to it
    MyTransformComponent myTransform;

    bool hasCollision;
    IMyCollider myCollider;

    // Linear Motion
    MyVector3 force;
    MyVector3 acceleration;
    [SerializeField]
    MyVector3 velocity;
    public float mass = 1;

    // Angular Motion
    MyVector3 torque;
    MyVector3 angularAcceleration;
    [SerializeField]
    MyVector3 angularVelocity;
    public float inertia = 1;

    // External Forces
    MyVector3 externalForces;
    MyVector3 externalTorques;

    public bool isImmovable;

    public bool usingGravity;

    public bool usingAirResistance;
    public float dragCoefficient = 1; // Also contains the surface Area of the object
    const float airDensity = 1.293f; // 1.293 kg / m^3

    public float restitutionCoefficient = 1; // Used for ball/wall colisions. For ball/ball, the lower restitution is used. Should be between 0 and 1 inclusive.
    float restitutionToApply = 1;
    MyVector3 restitutionDirection = MyVector3.zero;

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

    public void CalculateCollisions()
    {
        // Get a list of all the collidable objects in the scene (VERY suboptimal, implement spacial partitioning later)
        GameObject[] objects = GameObject.FindGameObjectsWithTag("collidable");
        MyTransformComponent[] transforms = new MyTransformComponent[objects.Length];
        IMyCollider[] colliders = new IMyCollider[objects.Length];
        MyRigidBodyComponent[] rigidBodies = new MyRigidBodyComponent[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            transforms[i] = objects[i].GetComponent<MyTransformComponent>();
            colliders[i] = objects[i].GetComponent<IMyCollider>();
            rigidBodies[i] = objects[i].GetComponent<MyRigidBodyComponent>();
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].IsOverlappingWith(myCollider) && rigidBodies[i] != this)
            {
                bool separated = myCollider.SeparateFrom(colliders[i], velocity, rigidBodies[i].velocity);
                if (separated)
                {
                    MyVector3 toOther = transforms[i].position - myTransform.position;
                    MyVector3 pointOfImpact = myTransform.position + (0.5f * toOther);
                    MyVector3 impulseDirection = (-toOther).GetNormalisedVector();
                    if (rigidBodies[i].isImmovable)
                    {
                        float relativeSpeed = velocity.GetVectorLength() * MyMathsLibrary.GetDotProduct(velocity, toOther, true);
                        float impulseMagnitude = 2 * mass * relativeSpeed / Time.fixedDeltaTime;
                        MyVector3 impulse = impulseDirection * impulseMagnitude;
                        AddForceAtLocation(impulse, pointOfImpact);
                        restitutionToApply = restitutionCoefficient;
                        restitutionDirection = toOther.GetNormalisedVector();
                        Debug.Log($"Impulse: {impulse}");
                    }
                    else
                    {
                        float relativeSpeedA = velocity.GetVectorLength() * MyMathsLibrary.GetDotProduct(velocity, toOther, true);
                        float relativeSpeedB = rigidBodies[i].velocity.GetVectorLength() * MyMathsLibrary.GetDotProduct(rigidBodies[i].velocity, toOther, true);
                        float relativeSpeed = Mathf.Abs(relativeSpeedA - relativeSpeedB);
                        float impulseMagnitudeA = mass * relativeSpeed / Time.fixedDeltaTime;
                        float impulseMagnitudeB = rigidBodies[i].mass * relativeSpeed / Time.fixedDeltaTime;
                        MyVector3 impulseA = impulseDirection * impulseMagnitudeA;
                        MyVector3 impulseB = -impulseDirection * impulseMagnitudeB;
                        AddForceAtLocation(impulseA, pointOfImpact);
                        rigidBodies[i].AddForceAtLocation(impulseB, pointOfImpact);
                        restitutionToApply = rigidBodies[i].restitutionToApply = Mathf.Min(restitutionCoefficient, rigidBodies[i].restitutionCoefficient);
                        restitutionDirection = toOther.GetNormalisedVector();
                        rigidBodies[i].restitutionDirection = -restitutionDirection;
                        Debug.Log($"Impulse A: {impulseA}, Impulse B: {impulseB}");
                    }
                }
            }
        }
    }

    public void CalculateRestitution()
    {
        float restitutionAngle = Mathf.Acos(MyMathsLibrary.GetDotProduct(velocity, restitutionDirection, true));
        float speed = velocity.GetVectorLength();
        MyVector3 restitutionVelocity = -restitutionDirection * (speed * Mathf.Cos(restitutionAngle) * (1 - restitutionToApply));
        velocity += restitutionVelocity;
        if (restitutionToApply != 1)
            restitutionToApply = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        myCollider = GetComponent<IMyCollider>();
        if (myCollider == null)
        {
            hasCollision = false;
        }
        else
        {
            hasCollision = true;
        }
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        myCollider.ShowForSeconds(Time.fixedDeltaTime);

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

            Debug.LogWarning(dragForce);
        }

        if (hasCollision)
            CalculateCollisions();

        if (myTransform.position.y < 0)
        {
            myTransform.position.y = 0;
            float relativeSpeed = -velocity.y;
            float impulseMagnitude = 2 * mass * relativeSpeed / Time.fixedDeltaTime;
            MyVector3 impulse = MyVector3.up * impulseMagnitude;
            externalForces += impulse;
            restitutionToApply = restitutionCoefficient;
            restitutionDirection = MyVector3.down;
            //Debug.Log($"Ground Impulse {impulse}");
        }

        // Calculate external forces
        force += externalForces;
        torque += externalTorques;
        externalForces = MyVector3.zero;
        externalTorques = MyVector3.zero;

        // Linear Motion
        acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
        CalculateRestitution();
        if (!isImmovable)
        {
            myTransform.position += velocity * Time.fixedDeltaTime;
        }

        // Angular Motion
        angularAcceleration = torque / inertia;
        angularVelocity += angularAcceleration * Time.fixedDeltaTime;
        MyQuat quaternionVelocity = (angularVelocity * Time.fixedDeltaTime).ConvertEulerToQuaternion(); //quaternionVelocity moreso represents the vector, rather than the velocity.
        myTransform.newRotation = quaternionVelocity * myTransform.rotation;
        if (myTransform.newRotation != myTransform.rotation)
            myTransform.spinning = true;
    }
}
