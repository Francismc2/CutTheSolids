using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Quaternion Variable")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Variable")]
    public class QuaternionVariable : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.Strict)] private Quaternion input;

        [SerializeField][Output(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Quaternion output;

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