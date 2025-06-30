using UnityEngine;

namespace Code.Core.Common.Physics
{
    public interface IPhysicsResolver
    {
        int Raycast2D(Vector2 origin,
            Vector2 direction,
            ContactFilter2D contactFilter,
            RaycastHit2D[] results);
    
        int Raycast2D(Vector2 origin,
            Vector2 direction,
            ContactFilter2D contactFilter,
            RaycastHit2D[] results,
            float distance);

        int OverlapPoint(Vector2 point,
            ContactFilter2D contactFilter,
            Collider2D[] results);

        int OverlapCircle(
            Vector2 point,
            float radius,
            ContactFilter2D contactFilter,
            Collider2D[] results);

        int OverlapSphereNonAlloc(Vector3 position, float radius, Collider2D[] results);

        bool ComputePenetration(
            Collider colliderA,
            Vector3 positionA,
            Quaternion rotationA,
            Collider colliderB,
            Vector3 positionB,
            Quaternion rotationB,
            out Vector3 direction,
            out float distance);
        
        bool ComputePenetration(
            Collider2D colliderA,
            Vector3 positionA,
            Quaternion rotationA,
            Collider2D colliderB,
            Vector3 positionB,
            Quaternion rotationB,
            out Vector3 direction,
            out float distance);
    }
}