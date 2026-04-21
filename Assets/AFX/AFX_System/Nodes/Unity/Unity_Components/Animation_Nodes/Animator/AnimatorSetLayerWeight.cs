using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("SetLayerWeight")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimation + "Animator SetLayerWeight")]
    public class AnimatorSetLayerWeight : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Animator animatorIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int layerIndex = 0;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float weight;

        public void SetLayerWeight()
        {
            animatorIn = GetInputValue(nameof(animatorIn), animatorIn);
            layerIndex = GetInputValue(nameof(layerIndex), layerIndex);
            weight = GetInputValue(nameof(weight), weight);

            animatorIn.SetLayerWeight(layerIndex, weight);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetLayerWeight();
            base.ExecuteNode(exit);
        }
    }
}