using UnityEngine;

public interface IMyCollider
{
    float type { get; }
    public bool IsOverlappingWith(MyAABBCollider otherBox);
    public bool IsOverlappingWith(MySphereCollider otherSphere);
    public bool IsOverlappingWith(MyCapsuleCollider otherCapsule);
}