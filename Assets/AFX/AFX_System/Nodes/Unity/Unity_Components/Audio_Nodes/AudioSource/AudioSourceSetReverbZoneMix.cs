using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set ReverbZoneMix")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set ReverbZoneMix")]
    public class AudioSourceSetReverbZoneMix : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float reverbZoneMix;

        private void SetReverbZoneMix()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            reverbZoneMix = GetInputValue(nameof(reverbZoneMix), reverbZoneMix);

            audioSourceIn.reverbZoneMix = reverbZoneMix;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetReverbZoneMix();
            base.ExecuteNode(exit);
        }
    }
}