using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Bool Variable")]
    [CreateNodeMenu(AFXMenuTree.Bool + "Bool Variable")]
    public class BoolVariable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected,typeConstraint = TypeConstraint.Strict)] private bool input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private bool output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            output = input;
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}