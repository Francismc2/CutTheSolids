using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Pitch")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Pitch")]
    public class AudioSourceSetPitch : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float pitch;

        private void PitchUpdate()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            pitch = GetInputValue(nameof(pitch), pitch);

            audioSourceIn.pitch = pitch;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            PitchUpdate();
            base.ExecuteNode(exit);
        }
    }
}