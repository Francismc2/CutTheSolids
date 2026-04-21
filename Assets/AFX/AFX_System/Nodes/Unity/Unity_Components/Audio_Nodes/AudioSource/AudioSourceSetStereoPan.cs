using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set StereoPan")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set StereoPan")]
    public class AudioSourceSetStereoPan : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float stereoPan;

        private void SetStereoPan()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            stereoPan = GetInputValue(nameof(stereoPan), stereoPan);

            audioSourceIn.panStereo = stereoPan;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetStereoPan();
            base.ExecuteNode(exit);
        }
    }
}