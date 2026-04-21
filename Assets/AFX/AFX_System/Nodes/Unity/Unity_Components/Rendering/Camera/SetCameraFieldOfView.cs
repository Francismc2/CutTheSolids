using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set FieldOfView")]
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingCamera + "Set Camera FieldOfView")]
    public class SetCameraFieldOfView : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private Camera cameraIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float fieldOfView;

        public void FieldOfViewUpdate()
        {
            cameraIn = GetInputValue(nameof(cameraIn), cameraIn);
            cameraIn.fieldOfView = GetInputValue(nameof(fieldOfView), fieldOfView);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            FieldOfViewUpdate();
            base.ExecuteNode(exit);
        }
    }
}