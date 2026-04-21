using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("SitHere")]
    [CreateNodeMenu(AFXMenuTree.Seat + "SitTrigger Sit Here")]
    public class SitTriggerSitHere : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private LVR_SitTrigger sitTrigger;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            sitTrigger = GetInputValue(nameof(sitTrigger), sitTrigger);

            base.ExecuteNode(exit);
        }
    }
}