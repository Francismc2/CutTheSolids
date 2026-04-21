using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLightSet + "Set Light Color")]
    public class SetLightColor : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Light lightIn;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Color colorIn;

        public void LightUpdate()
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            colorIn = GetInputValue(nameof(colorIn), colorIn);
            lightIn.color = colorIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LightUpdate();
        }
    }
}