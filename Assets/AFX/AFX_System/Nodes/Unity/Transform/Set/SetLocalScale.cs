using UnityEngine;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [NodeTitle("Set LocalScale")]
    [CreateNodeMenu(AFXMenuTree.TransformSet + "Transform Set LocalScale")]
    public class SetLocalScale : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Transform transform;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 vector3;

        void SetScales()
        {
            transform = GetInputValue(nameof(transform), transform);
            vector3 = GetInputValue(nameof(vector3), vector3);

            transform.localScale = vector3;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetScales();
            base.ExecuteNode(exit);
        }
    }
}