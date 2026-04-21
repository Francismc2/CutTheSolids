using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Preset")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioReverbZone + "AudioReverbZone Set Preset")]
    public class AudioReverbZoneSetPreset : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioReverbZone reverbZoneIn;
        [SerializeField] [NodeEnum] private AudioReverbPreset preset;

        private void SetPreset()
        {
            reverbZoneIn = GetInputValue(nameof(reverbZoneIn), reverbZoneIn);
            preset = GetInputValue(nameof(preset), preset);

            reverbZoneIn.reverbPreset = preset;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetPreset();
            base.ExecuteNode(exit);
        }
    }
}