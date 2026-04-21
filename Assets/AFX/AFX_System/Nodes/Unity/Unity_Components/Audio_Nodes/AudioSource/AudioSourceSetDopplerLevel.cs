using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set DopplerLevel")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set DopplerLevel")]
    public class AudioSourceSetDopplerLevel : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float dopplerLevel;

        private void SetDopplerLevel()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            dopplerLevel = GetInputValue(nameof(dopplerLevel), dopplerLevel);

            audioSourceIn.dopplerLevel = dopplerLevel;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetDopplerLevel();
            base.ExecuteNode(exit);
        }
    }
}