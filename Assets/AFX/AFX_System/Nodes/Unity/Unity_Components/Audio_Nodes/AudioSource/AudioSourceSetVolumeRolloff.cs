using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Volume RolloffMode")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Volume Rolloff")]
    public class AudioSourceSetVolumeRolloff : AFXActiveNode
    {

        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;

        [SerializeField] [NodeEnum] private AudioRolloffMode rolloff;

        private void SetRolloffMode()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            rolloff = GetInputValue(nameof(rolloff), rolloff);

            audioSourceIn.rolloffMode = rolloff;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetRolloffMode();
            base.ExecuteNode(exit);
        }
    }
}