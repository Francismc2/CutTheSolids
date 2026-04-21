using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Volume")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Volume")]
    public class AudioSourceSetVolume : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float volume;

        private void VolumeUpdate()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            volume = GetInputValue(nameof(volume), volume);

            audioSourceIn.volume = volume;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            VolumeUpdate();
            base.ExecuteNode(exit);
        }
    }
}