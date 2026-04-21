using System;
using UnityEngine;

namespace Engage.IFX.Utility
{
    public class FogSettingsHelper : MonoBehaviour
    {
        [Serializable]
        private class FogPreset
        {
            public string name;
            public bool fogEnabled = true;
            public FogMode fogMode = FogMode.ExponentialSquared;
            public Color fogColor = Color.grey;
            public float fogDensity = 0.01f;
            public float fogStartDistance = 0f;
            public float fogEndDistance = 300f;
        }

        [Header("Optional: Presets you can trigger via events")]
        [SerializeField] private FogPreset[] presets;

        public void ApplyPreset(int presetIndex)
        {
            if (presets == null) { return; }
            if (presetIndex < 0 || presetIndex >= presets.Length) { return; }

            ApplyFogPreset(presets[presetIndex]);
        }

        private void ApplyFogPreset(FogPreset preset)
        {
            if (preset == null) { return; }

            RenderSettings.fog = preset.fogEnabled;
            RenderSettings.fogMode = preset.fogMode;
            RenderSettings.fogColor = preset.fogColor;
            RenderSettings.fogDensity = preset.fogDensity;
            RenderSettings.fogStartDistance = preset.fogStartDistance;
            RenderSettings.fogEndDistance = preset.fogEndDistance;
        }

        public void SetFogEnabled(bool enabled)
        {
            RenderSettings.fog = enabled;
        }

        public void SetFogMode(int fogModeIndex)
        {
            RenderSettings.fogMode = (FogMode)fogModeIndex;
        }

        public void SetFogColor(Color color)
        {
            RenderSettings.fogColor = color;
        }

        public void SetFogDensity(float density)
        {
            RenderSettings.fogDensity = density;
        }

        public void SetFogStartDistance(float distance)
        {
            RenderSettings.fogStartDistance = distance;
        }

        public void SetFogEndDistance(float distance)
        {
            RenderSettings.fogEndDistance = distance;
        }

        public void SetFogColorHex(string hex)
        {
        }
    }
}