using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("SetParent")]
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform SetParent")]
    public class SetParent : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private Transform parentIn;
        [SerializeField][Input(ShowBackingValue.Never)] private Transform childIn;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool keepOffset = false;

        [SerializeField]
        [Output] private Transform oldParentOut;

        public override object GetValue(NodePort port)
        {
            oldParentOut = GetInputValue(nameof(oldParentOut), oldParentOut);
            return oldParentOut;
        }

        void Parent()
        {
            childIn = GetInputValue(nameof(childIn), childIn);
            parentIn = GetInputValue(nameof(parentIn), parentIn);
            if (childIn.parent != parentIn)
            {
                oldParentOut = childIn.parent;
                keepOffset = GetInputValue(nameof(keepOffset), keepOffset);
                childIn.SetParent(parentIn, keepOffset);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Parent();
            base.ExecuteNode(exit);
        }
    }
}