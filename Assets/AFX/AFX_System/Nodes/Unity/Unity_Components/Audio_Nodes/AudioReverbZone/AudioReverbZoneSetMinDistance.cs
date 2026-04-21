using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Min Distance")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioReverbZone + "AudioReverbZone Set Min Distance")]
    public class AudioReverbZoneSetMinDistance : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioReverbZone reverbZoneIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float minDistance;

        private void SetMinDistance()
        {
            reverbZoneIn = GetInputValue(nameof(reverbZoneIn), reverbZoneIn);
            minDistance = GetInputValue(nameof(minDistance), minDistance);

            reverbZoneIn.minDistance = minDistance;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMinDistance();
            base.ExecuteNode(exit);
        }
    }
}