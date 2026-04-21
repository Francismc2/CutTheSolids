using UnityEngine;

namespace Engage.AFX.v1.TransformNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.TransformSet + "Transform Set Position")]
    public class SetPosition : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Transform transform;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 vector3;

        void SetPositions()
        {
            transform = GetInputValue(nameof(transform), transform);
            vector3 = GetInputValue(nameof(vector3), vector3);
            transform.position = vector3;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetPositions();
            base.ExecuteNode(exit);
        }
    }
}