using UnityEngine;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLightSet + "Set Light Type")]
    public class SetLightType : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private Light lightIn;
        [SerializeField][NodeEnum] private LightType typeIn;

        public void LightUpdate()
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            lightIn.type = typeIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LightUpdate();
            base.ExecuteNode(afxFlow);
        }
    }
}