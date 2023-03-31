using UnityEngine;

public interface IMyCollider
{
    public bool IsOverlappingWith(MyAABBCollider otherBox);
    public bool IsOverlappingWith(MySphereCollider otherSphere);
    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule);
}