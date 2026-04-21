using System;

namespace Engage.AFX.v1
{
    [NodeTitle("OnDisable")]
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "On Disable")]
    public class OnDisable : AFXEventNode
    {
        private Action onDisable;

        protected override void Init()
        {
            onDisable = () => ExecuteNode(exit);
            Graph.AFXOnDisable += onDisable;
        }

        private void OnDestroy()
        {
            if (onDisable == null) return;
            Graph.AFXOnDisable -= onDisable;
        }
    }
}