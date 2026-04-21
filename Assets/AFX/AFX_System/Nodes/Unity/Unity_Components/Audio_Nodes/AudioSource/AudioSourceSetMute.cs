using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Mute")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Mute")]
    public class AudioSourceSetMute : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool mute;

        private void SetMute()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            mute = GetInputValue(nameof(mute), mute);

            audioSourceIn.mute = mute;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMute();
            base.ExecuteNode(exit);
        }
    }
}