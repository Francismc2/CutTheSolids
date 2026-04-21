using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MinDistance")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set MinDistance")]
    public class AudioSourceSetMinDistance : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float minDistance;

        private void SetMinDistance()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            minDistance = GetInputValue(nameof(minDistance), minDistance);

            audioSourceIn.minDistance = minDistance;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMinDistance();
            base.ExecuteNode(exit);
        }
    }
}