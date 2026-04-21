namespace Engage.AFX.v1
{
    [NodeTitle("StopSitting")]
    [CreateNodeMenu(AFXMenuTree.Seat + "SitTrigger Stop Sitting")]
    public class SitTriggerStopSitting : AFXActiveNode
    {
        public override void ExecuteNode(AFXFlow afxFlow)
        {
            base.ExecuteNode(exit);
        }
    }
}