using UnityEngine;

namespace Code.Core.Common.Physics
{
    public class UnityPhysicsResolver : IPhysicsResolver
    {
        public int Raycast2D(Vector2 origin,
            Vector2 direction,
            ContactFilter2D contactFilter,
            RaycastHit2D[] results) =>
            Physics2D.Raycast(origin, direction, contactFilter, results);

        public int Raycast2D(Vector2 origin,
            Vector2 direction,
            ContactFilter2D contactFilter,
            RaycastHit2D[] results,
            [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance) =>
            Physics2D.Raycast(origin, direction, contactFilter, results, distance);

        public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results) => 
            Physics2D.OverlapPoint(point, contactFilter, results);

        public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results) => 
            Physics2D.OverlapCircle(point, radius, contactFilter, results);

        public int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results) => 
            UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, results);

        public bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB,
            Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance) =>
            UnityEngine.Physics.ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB,
                out direction, out distance);
    }
}