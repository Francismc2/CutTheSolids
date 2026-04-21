using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingLightSet + "Set Light Range")]
    public class SetLightRange : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Light lightIn;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float rangeIn;

        public void LightUpdate()
        {
            lightIn = GetInputValue(nameof(lightIn), lightIn);
            rangeIn = GetInputValue(nameof(rangeIn), rangeIn);
            lightIn.range = rangeIn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LightUpdate();
        }
    }
}