using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("LookAt")]
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform LookAt")]
    public class TransformLookAt : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform lookAtTransformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 worldUpIn;

        void LookAt()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            lookAtTransformIn = GetInputValue(nameof(lookAtTransformIn), lookAtTransformIn);
            worldUpIn = GetInputValue(nameof(worldUpIn), worldUpIn);

            transformIn.LookAt(lookAtTransformIn, worldUpIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LookAt();
            base.ExecuteNode(exit);
        }
    }
}