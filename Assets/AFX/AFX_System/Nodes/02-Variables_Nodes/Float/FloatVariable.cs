using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Float Variable")]
    [CreateNodeMenu(AFXMenuTree.Float + "Float Variable")]
    public class FloatVariable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected, typeConstraint = TypeConstraint.Strict)] private float input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private float output;

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