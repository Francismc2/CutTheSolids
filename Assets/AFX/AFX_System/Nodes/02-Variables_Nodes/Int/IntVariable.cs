using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Int Variable")]
    [CreateNodeMenu(AFXMenuTree.Int + "Int Variable")]
    public class IntVariable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected, typeConstraint = TypeConstraint.Strict)] private int input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private int output;

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