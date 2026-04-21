using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLightSet + "Set Light Intensity")]
    public class SetLightIntensity : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Light lightIn;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float intensityIn;

        public void LightUpdate()
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            intensityIn = GetInputValue(nameof(intensityIn), intensityIn);
            lightIn.intensity = intensityIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LightUpdate();
        }
    }
}