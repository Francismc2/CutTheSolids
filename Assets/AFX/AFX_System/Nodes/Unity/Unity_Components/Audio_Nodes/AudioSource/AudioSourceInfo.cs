using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("AudioSource Info")]
    [CreateNodeMenu(AFXMenuTree.ComponentAudioSource + "AudioSource Info")]
    public class AudioSourceInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Strict)] private AudioSource audioSourceIn;

        [SerializeField]
        [Output] private bool isPlaying;
        [SerializeField]
        [Output] private float time;
        [SerializeField]
        [Output] private float volume;

        public override object GetValue(NodePort port)
        {
            audioSourceIn = GetInputValue(nameof(audioSourceIn), audioSourceIn);
            if (port.fieldName == nameof(isPlaying))
            {
                isPlaying = audioSourceIn.isPlaying;
                return isPlaying;
            }

            if (port.fieldName == nameof(time))
            {
                time = audioSourceIn.time;
                return time;
            }

            if (port.fieldName == nameof(volume))
            {
                volume = audioSourceIn.volume;
                return volume;
            }

            return null;
        }
    }
}