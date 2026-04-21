using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Vector3 Variable")]
    [CreateNodeMenu(AFXMenuTree.Vector3 + "Vector3 Variable")]
    public class Vector3Variable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected, typeConstraint = TypeConstraint.Strict)] private Vector3 input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Vector3 output;

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