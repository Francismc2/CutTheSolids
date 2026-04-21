using System.IO;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public class BuildSettings : Settings<BuildSettings>
    {
        private const string defaultLocalBuildPath = "AssetBundles";
        private const string defaultLabelProjectCache = "IFXBuildToolProjects";
        private const string defaultLabelJob = "jobs";

        private string localBuildPath;

        private EngagePlatform? buildTargets;
        public BuildTarget[] BuildTargetPriority { get; set; } = new BuildTarget[] { BuildTarget.StandaloneWindows, BuildTarget.Android, BuildTarget.iOS, BuildTarget.StandaloneOSX };
        public int MaxSimultaneousBuildJobs { get; set; } = 1;
        public BundleBuildQueue BundleBuildQueueing { get; set; } = BundleBuildQueue.Serial;

        public Setting<bool> batchBuildMode;

        public BuildSettings()
        {
            RegisterBool(GetKey(nameof(BatchBuildMode)), ref batchBuildMode, true);
        }

        #region Legacy Fields
        public static string PrefabPrefix { get; set; }
        public static string PrefabSuffix { get; set; }
        public static string currentIFXNum { get; set; }
        public static string LocalThumbnailPath => Path.Combine(LocalBuildPath, "Thumbnails");
        #endregion

        public static string LocalBuildPath
        {
            get
            {
                if (string.IsNullOrEmpty(Instance.localBuildPath))
                {
                    //EditorPrefs.DeleteKey(Keys.currentLocalBuildPathKey);
                    var buildPath = new DirectoryInfo(EditorPrefs.GetString(Keys.currentLocalBuildPathKey, defaultLocalBuildPath));

                    // Handle protected default batch project case
                    int batchProjectIndex = buildPath.FullName.IndexOf(defaultLabelProjectCache);
                    if (batchProjectIndex > -1)
                    {
                        buildPath = new DirectoryInfo(Path.Combine(buildPath.FullName.Substring(0, batchProjectIndex), defaultLocalBuildPath));
                    }

                    if (!buildPath.Exists)
                    {
                        buildPath.Create();
                    }

                    Instance.localBuildPath = buildPath.FullName;
                }

                return Instance.localBuildPath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Instance.localBuildPath = null;
                    EditorPrefs.DeleteKey(Keys.currentLocalBuildPathKey);
                }
                else if (Instance.localBuildPath != value)
                {
                    Instance.localBuildPath = new DirectoryInfo(value).FullName;
                    EditorPrefs.SetString(Keys.currentLocalBuildPathKey, Instance.localBuildPath);
                }

            }
        }
        public static EngagePlatform BuildTargets
        {
            get
            {
                if (!Instance.buildTargets.HasValue)
                {
                    Instance.buildTargets = (EngagePlatform)EditorPrefs.GetInt(Keys.currentBuildTargetsKey, (int)EditorUserBuildSettings.activeBuildTarget.ToEngagePlatform());
                }

                return Instance.buildTargets.Value;
            }
            set
            {
                if (value == EngagePlatform.None)
                {
                    Instance.buildTargets = null;
                    EditorPrefs.DeleteKey(Keys.currentBuildTargetsKey);
                }
                else if (Instance.buildTargets != value)
                {
                    Instance.buildTargets = value;
                    EditorPrefs.SetInt(Keys.currentBuildTargetsKey, (int)Instance.buildTargets.Value);
                }
            }
        }
        public static bool BatchBuildMode { get => Instance.batchBuildMode; set => Instance.batchBuildMode.Value = value; }

        public static void Reset()
        {
            LocalBuildPath = null;
            BuildTargets = EngagePlatform.None;

            Instance.Reset(GetKey(nameof(BatchBuildMode)), Instance.batchBuildMode);
        }

        public static string GetLocalBuildPath(BuildTarget buildTarget) => Path.Combine(LocalBuildPath, buildTarget.ToEngagePlatform().ToString());
        public static string GetLocalBundlePath(EngagePlatform platform) => Path.Combine(LocalBuildPath, platform.ToString());

        public static string GetLocalProjectPath() => new DirectoryInfo(Application.dataPath).Parent.FullName;

        public static string GetLocalProjectName() => new DirectoryInfo(Application.dataPath).Parent.Name;
        public static string GetLocalProjectCache() => Path.Combine(GetLocalProjectPath(), defaultLabelProjectCache);
        public static string GetLocalProjectCache(BuildTarget buildTarget) => Path.Combine(GetLocalProjectCache(), buildTarget.ToEngagePlatform().ToString());

        public static string GetLocalJobCache() => Path.Combine(GetLocalProjectCache(), defaultLabelJob);
    }
}