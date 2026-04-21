using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Spacial Blend")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Set Spatial Blend")]
    public class AudioSourceSetSpatialBlend : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float spatialBlend;

        private void SetSpatialBlend()
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            spatialBlend = GetInputValue(nameof(spatialBlend), spatialBlend);

            audioSourceIn.spatialBlend = spatialBlend;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetSpatialBlend();
            base.ExecuteNode(exit);
        }
    }
}