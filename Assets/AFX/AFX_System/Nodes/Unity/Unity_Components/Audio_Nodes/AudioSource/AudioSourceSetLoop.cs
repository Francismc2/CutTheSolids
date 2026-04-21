using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Loop")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Loop")]
    public class AudioSourceSetLoop : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool loop;

        private void SetLoop()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            loop = GetInputValue(nameof(loop), loop);

            audioSourceIn.loop = loop;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetLoop();
            base.ExecuteNode(exit);
        }
    }
}