using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    public abstract class AFXFlowNode : AFXSequentialNode
    {
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Strict)] protected AFXFlow enter;

        public override object GetField(NodePort port) => enter;
    }
}