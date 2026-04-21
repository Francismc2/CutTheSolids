using System;

namespace Engage.AFX.v1
{
    [NodeTitle("OnEnable")]
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "OnEnable")]
    public class OnEnable : AFXEventNode
    {
        private Action onEnable;

        protected override void Init()
        {
            onEnable = () => ExecuteNode(exit);
            Graph.AFXOnEnable += onEnable;
        }

        private void OnDestroy()
        {
            if (onEnable == null) return;
            Graph.AFXOnEnable -= onEnable;
        }
    }
}