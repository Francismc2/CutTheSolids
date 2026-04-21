using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Engage.BuildTools
{
    public enum BundleBuildQueue { Parallel, Serial }

    public class BundleBuildJobManager : ViewModel
    {
        private readonly BundleBuilder.ModuleManager moduleManager = new BundleBuilder.ModuleManager();
        private readonly BundleBuilder.BatchHandler handler = new BundleBuilder.BatchHandler();
        private readonly HashSet<BuildTarget> installedBuildModules = new HashSet<BuildTarget>();

        public string LocalBuildPath { get => BuildSettings.LocalBuildPath; set => BuildSettings.LocalBuildPath = value; }
        public List<BundleBuildJob> JobList { get; } = new List<BundleBuildJob>();

        public bool WindowsModuleInstalled => installedBuildModules.Contains(BuildTarget.StandaloneWindows);
        public bool AndroidModuleInstalled => installedBuildModules.Contains(BuildTarget.Android);
        public bool IosModuleInstalled => installedBuildModules.Contains(BuildTarget.iOS);
        public bool OsxModuleInstalled => installedBuildModules.Contains(BuildTarget.StandaloneOSX);

        public bool BuildForWindows
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.Windows);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.Windows;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.Windows;
                }
            }
        }
        public bool BuildForAndroid
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.Android);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.Android;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.Android;
                }
            }
        }
        public bool BuildForIOS
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.iOS);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.iOS;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.iOS;
                }
            }
        }
        public bool BuildForMacOS
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.OSX);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.OSX;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.OSX;
                }
            }
        }
        public string BuildTargetString
        {
            get
            {
                var buildTargets = new List<string>();

                foreach (EngagePlatform target in System.Enum.GetValues(typeof(EngagePlatform)))
                {
                    if (target > EngagePlatform.None && BuildSettings.BuildTargets.HasFlag(target))
                    {
                        buildTargets.Add(target.ToString());
                    }
                }

                return string.Join(", ", buildTargets);
            }
        }

        public bool BatchBuild { get => BuildSettings.BatchBuildMode; set => BuildSettings.BatchBuildMode = value; }

        //IFXToolsQualityCheckTool qaTool;

        //private IFXThumbnailTool thumbnailToolInstance;
        //public IFXThumbnailTool ThumbnailToolInstance
        //{
        //    get
        //    {
        //        if (thumbnailToolInstance == null)
        //        {
        //            thumbnailToolInstance = new IFXThumbnailTool();
        //        }
        //        return thumbnailToolInstance;
        //    }
        //}

        protected override void Initialize()
        {
            foreach (var target in BundleBuilder.SupportedBuildTargets)
            {
                if (moduleManager.IsModuleInstalled(target))
                {
                    installedBuildModules.Add(target);
                    BuildSettings.BuildTargets |= target.ToEngagePlatform() & BuildSettings.BuildTargets;
                }
            }
        }

        public override void Refresh()
        {
            JobList.Clear();

            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                Debug.Log(path);

                if (AssetDatabase.IsValidFolder(path))
                {
                    var job = new BundleBuildJob(guid);
                    JobList.Add(job);
                }
            }

            NotifyPropertyChange(nameof(JobList));
        }

        public void BuildBundles()
        {
            if (BatchBuild)
            {
                StartBatchBuild();
            }
            else
            {
                StartBuild();
            }
        }

        public void StartBuild()
        {
            foreach (var job in JobList)
            {
                job.BuildTargets = BuildSettings.BuildTargets;

                if (AssetDatabase.AssetPathToGUID(job.BundlePath) == job.BundleGuid)
                {
                    if (job.IsQAPassed)
                    {
                        BundleBuilder.QueueBuildJob(job);
                    }
                }
                else
                {
                    Debug.Log("Nothing Selected - Select the folder you want built first");
                }
            }

            BundleBuilder.BuildAssetBundlesSerial();
        }

        private void StartBatchBuild()
        {
            foreach (BuildTarget target in installedBuildModules)
            {
                if (BuildSettings.BuildTargets.HasFlag(target.ToEngagePlatform()))
                {
                    string batchPath = handler.CreateBatchBuildFile(JobList.ToArray(), target);
                    handler.RunBatchFile(batchPath);
                }
            }
        }
    }
}