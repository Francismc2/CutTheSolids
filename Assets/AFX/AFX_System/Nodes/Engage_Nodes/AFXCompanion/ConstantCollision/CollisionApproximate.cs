using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ConstantCollision+ "Collision Approximate")]
    public class CollisionApproximate : AFXNode
    {
        [SerializeField] [Input] private Collider colliderA;
        [SerializeField] [Input] private Collider colliderB;

        [SerializeField] [Output] private bool output;

        public override object GetValue(NodePort port)
        {
            colliderA = GetInputValue(nameof(colliderA), colliderA);
            colliderB = GetInputValue(nameof(colliderB), colliderB);

            return AFXColliders.ColliderContainsCollider(colliderA, colliderB);
        }
    }
}