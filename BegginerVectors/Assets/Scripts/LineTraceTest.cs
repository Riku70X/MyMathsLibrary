using UnityEngine;

public class LineTraceTest : MonoBehaviour
{
    AABB box;

    MyVector3 startPosition;
    MyVector3 endPosition;

    MyVector3 intersectionPoint;
    bool intersected;

    [SerializeField] public GameObject Cube;

    public LineTraceTest()
    {
        startPosition = new MyVector3(2, -2, 0);
        endPosition = new MyVector3(2, 2, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.DrawLine(startPosition.ConvertToUnityVector(), endPosition.ConvertToUnityVector(), Color.white, 999.0f);
        Cube = GameObject.Find("Cube");
        box = Cube.GetComponent<AABB>();
    }

    // Update is called once per frame
    void Update()
    {
        intersected = AABB.LineIntersection(box, startPosition, endPosition, out intersectionPoint);

        Debug.Log($"intersected = {intersected}, point = ({intersectionPoint.x}, {intersectionPoint.y}, {intersectionPoint.z})");
    }
}