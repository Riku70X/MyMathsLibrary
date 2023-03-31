using System.Collections;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    MyTransformComponent myTransform;
    MySphereCollider boundingSphere;

    GameObject[] objects;
    MyTransformComponent[] transforms;
    IMyCollider[] colliders;
    MyRigidBodyComponent[] rigidBodies;

    MyVector3 direction;
    MyVector3 force;
    [SerializeField] float explosivePower;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        boundingSphere = GetComponent<MySphereCollider>();

        boundingSphere.ShowForSeconds(5);

        objects = GameObject.FindGameObjectsWithTag("explodable");
        transforms = new MyTransformComponent[objects.Length];
        colliders = new IMyCollider[objects.Length];
        rigidBodies = new MyRigidBodyComponent[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            transforms[i] = objects[i].GetComponent<MyTransformComponent>();
            colliders[i] = objects[i].GetComponent<IMyCollider>();
            rigidBodies[i] = objects[i].GetComponent<MyRigidBodyComponent>();
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].IsOverlappingWith(boundingSphere))
            {
                direction = transforms[i].position - myTransform.position;
                force = explosivePower * direction / direction.GetVectorLength();
                rigidBodies[i].AddForce(force);
                rigidBodies[i].AddTorque(new MyVector3(1, 0, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        boundingSphere.ShowForSeconds(Time.deltaTime);
    }
}
