using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Player + "Player Hands Info")]
    public class PlayerHandsInfo : AFXNode, IDelayUntilReady
    {
        [SerializeField]
        [Output] private Transform primaryHandTransformOut;
        [SerializeField]
        [Output] private Transform secondaryHandTransformOut;
        [SerializeField]
        [Output] private bool grippedPrimaryOut;
        [SerializeField]
        [Output] private bool grippedSecondaryOut;
        [SerializeField]
        [Output] private bool triggerPrimaryOut;
        [SerializeField]
        [Output] private bool triggerSecondaryOut;


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