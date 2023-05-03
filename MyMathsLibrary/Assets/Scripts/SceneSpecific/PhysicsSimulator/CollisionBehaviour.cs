using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    GameObject Sphere1;
    GameObject Sphere2;

    MyRigidBodyComponent rigidBody1;
    MyRigidBodyComponent rigidBody2;
    // Start is called before the first frame update
    void Start()
    {
        Sphere1 = GameObject.Find("Sphere1");
        Sphere2 = GameObject.Find("Sphere2");

        rigidBody1 = Sphere1.GetComponent<MyRigidBodyComponent>();
        rigidBody2 = Sphere2.GetComponent<MyRigidBodyComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rigidBody1.AddForce(new(50, 0, 0));
            rigidBody2.AddForce(new(-10, 0, 0));
        }
    }
}
