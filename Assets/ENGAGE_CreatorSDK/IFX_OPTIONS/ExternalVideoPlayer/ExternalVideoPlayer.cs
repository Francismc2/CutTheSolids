using System.Collections.Generic;
using UnityEngine;

namespace Engage.Media.Video
{
    internal class ExternalVideoPlayer : MonoBehaviour
    {
        [SerializeField] private string url;
        [SerializeField] private float startTimeOffsetSeconds;
        [SerializeField] private bool playOnEnableNoSync;
        [SerializeField] private float volume;
        [SerializeField] private bool isLoop;
        [SerializeField] private VideoResolution youtubeResolution;
        [SerializeField] private bool is3d;
        [SerializeField] private List<Renderer> screenRenderers;

        [Header("For 360 YouTube support only - 0=Standard, 1=Equiangular 2D, 2=Equiangular 3D")]
        [SerializeField] private GameObject[] screens360;

        [Header("Aspect Ratio Correction - Disable for 360 video")]
        [SerializeField] private bool useAspectRatioCorrection = true;
        [SerializeField] private float targetAspectRatio = 1.7778f;

        [Header("Chroma")]
        [SerializeField] private bool useChroma;
        [SerializeField] private bool useCustomChromaKeyColor;
        [SerializeField] private Color customChromaKeyColor = Color.green;

        [Header("Spatial Audio")]
        [SerializeField] private SpatialAudioOption spatialAudioOption;
        [SerializeField] private List<Transform> additionalAudioPoints;

        [Header("Uncommon - 2d only")]
        [SerializeField] private List<CanvasRenderer> canvasRenderers;

        private void Awake() { }

        void OnEnable() { }

        public void SetUrl(string url) { }

        public void SetCustomChromaKey(Color color) { }
    }
}
