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
    MyVector3 pointOfImpact;
    MyVector3 force;
    [SerializeField] float explosivePower;

    void Explode()
    {
        boundingSphere.ShowForSeconds(5);

        // Get a list of all the collidable objects in the scene
        objects = GameObject.FindGameObjectsWithTag("collidable");
        transforms = new MyTransformComponent[objects.Length];
        colliders = new IMyCollider[objects.Length];
        rigidBodies = new MyRigidBodyComponent[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            print(i);
            transforms[i] = objects[i].GetComponent<MyTransformComponent>();
            colliders[i] = objects[i].GetComponent<IMyCollider>();
            rigidBodies[i] = objects[i].GetComponent<MyRigidBodyComponent>();
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].IsOverlappingWith(boundingSphere))
            {
                pointOfImpact = colliders[i].GetClosestPointTo(myTransform.position);
                direction = transforms[i].position - myTransform.position;
                force = direction * explosivePower / direction.GetVectorLength();
                rigidBodies[i].AddForceAtLocation(force, pointOfImpact);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
        boundingSphere = GetComponent<MySphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }
}
