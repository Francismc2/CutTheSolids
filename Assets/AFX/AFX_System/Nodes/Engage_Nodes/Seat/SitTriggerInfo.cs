using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SitTrigger Info")]
    [CreateNodeMenu(AFXMenuTree.Seat + "SitTrigger Info")]
    public class SitTriggerInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private LVR_SitTrigger sitTriggerIn;

        [SerializeField]
        [Output] private bool playerInSeatOut;

        public override object GetValue(NodePort port)
        {
            return false;
        }
    }
}