using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow+ "Flow If")]
    public class If : AFXFlowNode
    {
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow ifTrue;

        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow ifFalse;


        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool input;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            if (input)
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