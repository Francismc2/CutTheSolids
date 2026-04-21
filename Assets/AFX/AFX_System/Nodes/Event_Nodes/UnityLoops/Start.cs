using System;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "Start")]
    public class Start : AFXEventNode
    {
        private Action start;

        protected override void Init()
        {
            start = () => ExecuteNode(exit);
            Graph.AFXStart += start;
        }

        private void OnDestroy()
        {
            if (start == null) return;
            Graph.AFXStart -= start;
        }
    }
}