using System;
using UnityEngine;

namespace Engage.Media.Video
{
    [Serializable]
    public class SpatialAudioOption
    {
        [SerializeField] private bool isSpatialAudio;
        [SerializeField] private float spatialAudioRange;

        public bool IsSpatialAudio { get { return isSpatialAudio; } }
        public float SpatialAudioRange { get { return spatialAudioRange; } }

        public SpatialAudioOption
            (
                bool isSpatialAudio,
                float spatialAudioRange = 15f
            )
        {
            this.isSpatialAudio = isSpatialAudio;
            this.spatialAudioRange = spatialAudioRange;
        }
    }
}
