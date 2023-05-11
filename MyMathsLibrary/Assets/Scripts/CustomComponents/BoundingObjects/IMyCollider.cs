using UnityEngine;

public interface IMyCollider
{
    float type { get; }
    public MyVector3 GetClosestPointTo(MyVector3 point);
    public bool IsOverlappingWith(MyVector3 startPoint, MyVector3 endPoint, out MyVector3 intersectionPoint);
    public bool IsOverlappingWith(MySphereCollider otherSphere);
    public bool IsOverlappingWith(MyAABBCollider otherBox);
    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule);
    public bool IsOverlappingWith(IMyCollider otherCollider)
    {
        switch (otherCollider.type)
        {
            case 0:
                MySphereCollider otherSphere = otherCollider as MySphereCollider;
                return IsOverlappingWith(otherSphere);
            case 1:
                MyAABBCollider otherBox = otherCollider as MyAABBCollider;
                return IsOverlappingWith(otherBox);
            case 2:
                MyCapsuleCollider otherCapsule = otherCollider as MyCapsuleCollider;
                return IsOverlappingWith(otherCapsule);
            default:
                Debug.LogError("ERROR: Invalid Collider Type");
                return false;
        }
    }

    public bool SeparateFrom(MySphereCollider otherSphere, MyVector3 velocity, MyVector3 otherVelocity);
    public bool SeparateFrom(IMyCollider otherCollider, MyVector3 velocity, MyVector3 otherVelocity)
    {
        switch(otherCollider.type)
        {
            case 0:
                MySphereCollider otherSphere = otherCollider as MySphereCollider;
                return SeparateFrom(otherSphere, velocity, otherVelocity);
            case 1:
                Debug.LogWarning("Box separation not yet implemented");
                return false;
            case 2:
                Debug.LogWarning("Capsule separation not yet implemented");
                return false;
            default:
                Debug.LogError("ERROR: Invalid Collider Type");
                return false;
        }
    }
    public void ShowForSeconds(float seconds);
}