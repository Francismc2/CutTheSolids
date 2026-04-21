using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Light RenderMode")]
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLightSet + "Set Light Render Mode")]
    public class SetLightRenderMode : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private Light lightIn;
        [SerializeField][NodeEnum] private LightRenderMode renderModeIn;

        public void LightUpdate()
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            lightIn.renderMode = renderModeIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LightUpdate();
        }
    }
}