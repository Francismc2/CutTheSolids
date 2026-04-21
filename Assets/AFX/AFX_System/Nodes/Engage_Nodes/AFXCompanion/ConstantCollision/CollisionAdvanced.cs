using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ConstantCollision+ "Collision Advanced")]
    public class CollisionAdvanced : AFXNode
    {
        [SerializeField]
        [Input] private Collider colliderA;
        [SerializeField]
        [Input] private Collider colliderB;

        [SerializeField]
        [Output] private bool output;

        private AFXColliderObserver colliderObserver;

        public override object GetValue(NodePort port)
        {
            colliderA = GetInputValue(nameof(colliderA), colliderA);
            colliderB = GetInputValue(nameof(colliderB), colliderB);
            if (colliderObserver == null)
            {
                colliderObserver = colliderA.gameObject.AddComponent<AFXColliderObserver>();
                colliderObserver.AddInformant(colliderA);
            }
            return GetCollisionAdvanced(colliderA, colliderB, colliderObserver);
        }

        public static bool GetCollisionAdvanced(Collider colliderA, Collider colliderB, AFXColliderObserver colliderObserver)
        {
            foreach (var collider in colliderObserver.currentlyCollidingWith)
            {
                if (collider == colliderB)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
