using UnityEngine;

public interface IMyCollider
{
    float type { get; }
    public MyVector3 GetClosestPointTo(MyVector3 point);
    public bool IsOverlappingWith(MyVector3 startPoint, MyVector3 endPoint, out MyVector3 intersectionPoint);
    public bool IsOverlappingWith(MyAABBCollider otherBox);
    public bool IsOverlappingWith(MySphereCollider otherSphere);
    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule);
}