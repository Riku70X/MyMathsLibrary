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

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        boundingSphere = GetComponent<MySphereCollider>();

        objects = GameObject.FindGameObjectsWithTag("explodable");
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
                force = 1 / direction;
                rigidBodies[i].AddForce(force);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
