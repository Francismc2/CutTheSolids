using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Player + "Player Body Info")]
    public class PlayerBodyInfo : AFXNode, IDelayUntilReady
    {
        [SerializeField]
        [Output(ShowBackingValue.Never)] private GameObject playerGameObjectOut;
        [SerializeField]
        [Output] private Transform playerTransformOut;
        [SerializeField]
        [Output] private Collider playerColliderOut;

        public override object GetValue(NodePort port)
        {
            return null;
        }

        bool IDelayUntilReady.IsValueReady()
        {
            return true;
        }
    }
}