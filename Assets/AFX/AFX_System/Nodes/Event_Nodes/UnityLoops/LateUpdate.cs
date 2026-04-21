using System;

namespace Engage.AFX.v1
{
    [NodeTitle("LateUpdate")]
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "LateUpdate")]
    public class LateUpdate : AFXEventNode
    {
        private Action lateUpdate;

        protected override void Init()
        {
            lateUpdate = () => ExecuteNode(exit);
            Graph.AFXLateUpdate += lateUpdate;
        }

        private void OnDestroy()
        {
            if (lateUpdate == null) return;
            Graph.AFXLateUpdate -= lateUpdate;
        }
    }
}