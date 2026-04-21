using UnityEditor;
using UnityEngine;
using System;

namespace Engage.BuildTools
{
    public static partial class BundleBuilder
    {
        public static EngagePlatform ToEngagePlatform(this BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android: return EngagePlatform.Android;
                case BuildTarget.iOS: return EngagePlatform.iOS;
                case BuildTarget.StandaloneOSX: return EngagePlatform.OSX;
                case BuildTarget.WebGL: return EngagePlatform.WebGL;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64: return EngagePlatform.Windows;
                default: return EngagePlatform.None;
            }
        }

        public static BuildTarget ToBuildTarget(this EngagePlatform platform)
        {
            switch (platform)
            {
                case EngagePlatform.Android: return BuildTarget.Android;
                case EngagePlatform.iOS: return BuildTarget.iOS;
                case EngagePlatform.OSX: return BuildTarget.StandaloneOSX;
                default:
                case EngagePlatform.Windows: return BuildTarget.StandaloneWindows;
            }
        }

        public static BuildTarget GetUnityBuildTarget(string platformName)
        {
            if (Enum.TryParse(platformName, out EngagePlatform platform))
            {
                return platform.ToBuildTarget();
            }
            else
            {
                Debug.LogWarning($"[ApiClient - BuildTarget] Unrecognized platform name: {platformName}");
                return EngagePlatform.None.ToBuildTarget();
            }
        }
    }
}
