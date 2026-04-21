using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set AudioClip")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set AudioClip")]
    public class AudioSourceSetAudioClip : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private AudioClip audioClip;

        private void SetAudioClip()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            audioClip = GetInputValue(nameof(audioClip), audioClip);

            audioSourceIn.clip = audioClip;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetAudioClip();
            base.ExecuteNode(exit);
        }
    }
}