using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    public abstract class AFXActiveNode : AFXSequentialNode
    {
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Strict)] protected AFXFlow enter;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Strict)] protected AFXFlow exit;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            exit.ActivateNextNode(GetPort(nameof(exit)));
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(exit) => exit,
                _ => null
            };
        }
    }
}