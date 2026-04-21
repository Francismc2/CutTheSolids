using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Locked In Seat")]
    [CreateNodeMenu(AFXMenuTree.Seat + "SitTrigger Set Locked in Seat")]
    public class SitTriggerSetLockedInSeat : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private LVR_SitTrigger sitTriggerIn;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool lockedInSeat;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            sitTriggerIn = GetInputValue(nameof(sitTriggerIn), sitTriggerIn);
            lockedInSeat = GetInputValue(nameof(lockedInSeat), lockedInSeat);
            sitTriggerIn.lockedInSeat = lockedInSeat;

            base.ExecuteNode(exit);
        }
    }
}