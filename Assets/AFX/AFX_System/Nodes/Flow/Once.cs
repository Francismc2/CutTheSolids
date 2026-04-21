using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow + "Flow Once")]
    public class Once : AFXFlowNode
    {
        [SerializeField] [Input(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow reset;

        [SerializeField] [Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow once;
        [SerializeField] [Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow after;

        private NodePort enterPortCache;
        private NodePort resetPortCache;

        private bool hasRunOnce = false;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (enterPortCache == null) enterPortCache = GetPort(nameof(enter));
            if (resetPortCache == null) resetPortCache = GetPort(nameof(reset));

            if (afxFlow.Port == enterPortCache.Connection)
            {
                if (hasRunOnce == true)
                {
                    after.ActivateNextNode(GetPort(nameof(after)));
                }
                else
                {
                    once.ActivateNextNode(GetPort(nameof(once)));
                    hasRunOnce = true;
                }
                return;
            }

            if (afxFlow.Port == resetPortCache.Connection)
            {
                hasRunOnce = false;
                return;
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(after) => after,
                nameof(once) => once,
                _ => null
            };
        }
    }
}