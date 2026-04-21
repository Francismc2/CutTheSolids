using System;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "Update")]
    public class Update : AFXEventNode
    {
        private Action update;

        protected override void Init()
        {
            update = () => ExecuteNode(exit);
            Graph.AFXUpdate += update;
        }

        private void OnDestroy()
        {
            if (update == null) return;
            Graph.AFXUpdate -= update;
        }
    }
}