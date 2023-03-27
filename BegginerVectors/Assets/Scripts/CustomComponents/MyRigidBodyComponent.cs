using UnityEngine;

public class MyRigidBodyComponent : MonoBehaviour
{
    public float mass;
    public float drag;
    public float angularDrag;
    public bool useGravity;
    public bool isKinematic;

    float speed;
    MyVector3 velocity;
    MyVector3 angularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Fixed Update is called once per physics frame (default .02 seconds)
    void FixedUpdate()
    {
        
    }
}
