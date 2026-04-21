using UnityEngine;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [NodeTitle("Set LocalRotation")]
    [CreateNodeMenu(AFXMenuTree.TransformSet + "Transform Set LocalRotation")]
    public class SetLocalRotation : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private Transform transform;
        [SerializeField] [Input(ShowBackingValue.Never)] private Quaternion rotation;

        void RotationSet()
        {
            transform = GetInputValue(nameof(transform), transform);
            rotation = GetInputValue(nameof(rotation), rotation);

            transform.localRotation = rotation;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            RotationSet();
            base.ExecuteNode(exit);
        }
    }
}