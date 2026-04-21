using System;
using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ConstantCollision+ "Collision Bounds")]
    [Serializable]
    public class CollisionBounds : AFXNode
    {
        [SerializeField]
        [Input] private Collider colliderA;
        [SerializeField]
        [Input] private Collider colliderB;

        [SerializeField]
        [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            colliderA = GetInputValue(nameof(colliderA), colliderA);
            colliderB = GetInputValue(nameof(colliderB), colliderB);
            return GetCollisionBounds(colliderA, colliderB);
        }

        private static bool GetCollisionBounds(Collider colliderA, Collider colliderB)
        {
            if (colliderB.bounds.Intersects(colliderA.bounds))
            {
                return true;
            }
            return false;
        }
    }
}