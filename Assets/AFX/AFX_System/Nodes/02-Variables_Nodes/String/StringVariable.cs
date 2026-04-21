using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("String Variable")]
    [CreateNodeMenu(AFXMenuTree.String + "String Variable")]
    public class StringVariable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected, typeConstraint = TypeConstraint.Strict)] private string input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private string output;

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