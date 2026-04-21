using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set TargetTexture")]
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingCamera + "Set Camera TargetTexture")]
    public class SetCameraTargetTexture : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Camera cameraIn;
        [SerializeField]
        [Input] private RenderTexture renderTexture;

        public void SetRenderTexture()
        {
            cameraIn = GetInputValue(nameof(cameraIn), cameraIn);
            cameraIn.targetTexture = GetInputValue(nameof(renderTexture), renderTexture);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetRenderTexture();
            base.ExecuteNode(exit);
        }
    }
}