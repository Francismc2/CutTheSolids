using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Max Distance")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioReverbZone + "AudioReverbZone Set Max Distance")]
    public class AudioReverbZoneSetMaxDistance : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioReverbZone reverbZoneIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxDistance;

        private void SetMaxDistance()
        {
            reverbZoneIn = GetInputValue(nameof(reverbZoneIn), reverbZoneIn);
            maxDistance = GetInputValue(nameof(maxDistance), maxDistance);

            reverbZoneIn.maxDistance = maxDistance;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetMaxDistance();
            base.ExecuteNode(exit);
        }
    }
}