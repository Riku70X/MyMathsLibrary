using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    MyTransformComponent myTransform;
    MySphereCollider boundingSphere;
    MyCollider[] objects;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyTransformComponent>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
