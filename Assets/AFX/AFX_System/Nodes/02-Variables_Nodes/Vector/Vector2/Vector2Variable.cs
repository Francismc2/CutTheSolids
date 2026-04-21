using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Vector2 Variable")]
    [CreateNodeMenu(AFXMenuTree.Vector2 + "Vector2 Variable")]
    public class Vector2Variable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected, typeConstraint = TypeConstraint.Strict)] private Vector2 input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Vector2 output;

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