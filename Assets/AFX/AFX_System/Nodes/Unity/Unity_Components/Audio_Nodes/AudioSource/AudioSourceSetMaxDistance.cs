using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set MaxDistance")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set MaxDistance")]
    public class AudioSourceSetMaxDistance : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxDistance;

        private void SetMaxDistance()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            maxDistance = GetInputValue(nameof(maxDistance), maxDistance);

            audioSourceIn.maxDistance = maxDistance;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMaxDistance();
            base.ExecuteNode(exit);
        }
    }
}