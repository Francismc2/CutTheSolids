using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Spread")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Spread")]
    public class AudioSourceSetSpread : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float spread;

        private void SetSpread()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            spread = GetInputValue(nameof(spread), spread);

            audioSourceIn.spread = spread;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetSpread();
            base.ExecuteNode(exit);
        }
    }
}