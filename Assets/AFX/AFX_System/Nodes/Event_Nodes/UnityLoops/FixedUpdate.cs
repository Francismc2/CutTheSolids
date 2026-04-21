using System;

namespace Engage.AFX.v1
{
    [NodeTitle("FixedUpdate")]
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "FixedUpdate")]
    public class FixedUpdate : AFXEventNode
    {
        private Action fixedUpdate;

        protected override void Init()
        {
            fixedUpdate = () => ExecuteNode(exit);
            Graph.AFXFixedUpdate += fixedUpdate;
        }

        private void OnDestroy()
        {
            if (fixedUpdate == null) return;
            Graph.AFXFixedUpdate -= fixedUpdate;
        }
    }
}