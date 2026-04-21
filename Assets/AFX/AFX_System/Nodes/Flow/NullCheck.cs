using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow+ "Flow Null Check")]
    public class NullCheck : AFXFlowNode
    {

        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow isNull;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow isNotNull;

        [SerializeField][Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Object input;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            if(input == null)
            {
                isNull.ActivateNextNode(GetPort(nameof(isNull)));
            }
            else
            {
                isNotNull.ActivateNextNode(GetPort(nameof(isNotNull)));
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(isNull) => isNull,
                nameof(isNotNull) => isNotNull,
                _ => null
            };
        }
    }
}