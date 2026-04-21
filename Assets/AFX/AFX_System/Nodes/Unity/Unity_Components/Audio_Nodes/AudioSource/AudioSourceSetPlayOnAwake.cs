using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set PlayOnAwake")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set PlayOnAwake")]
    public class AudioSourceSetPlayOnAwake : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool playOnAwake;

        private void SetPlayOnAwake()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            playOnAwake = GetInputValue(nameof(playOnAwake), playOnAwake);

            audioSourceIn.playOnAwake = playOnAwake;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetPlayOnAwake();
            base.ExecuteNode(exit);
        }
    }
}