using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow+ "Flow And")]
    public class And : AFXFlowNode
    {

        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow ifTrue;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow ifFalse;

        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool input1;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool input2;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input1 = GetInputValue(nameof(input1), input1);
            input2 = GetInputValue(nameof(input2), input2);
            if (input1 && input2)
            {
                
                ifTrue.ActivateNextNode(GetPort(nameof(ifTrue)));
            }
            else
            {
                ifFalse.ActivateNextNode(GetPort(nameof(ifFalse)));
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(ifTrue) => ifTrue,
                nameof(ifFalse) => ifFalse,
                _ => null
            };
        }
    }
}