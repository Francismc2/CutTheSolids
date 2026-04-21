using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    [Flags]
    public enum EngagePlatform
    {
        None = 0x00,
        Android = 0x01,
        iOS = 0x02,
        OSX = 0x04,
        WebGL = 0x08,
        Windows = 0x10
    }

    public static partial class BundleBuilder
    {
        public static Queue<BundleBuildJob> JobQueue { get; } = new Queue<BundleBuildJob>();
        public static readonly Queue<BundleBuildJob> processingJobQueue = new Queue<BundleBuildJob>();

        public static BuildTarget[] SupportedBuildTargets = new BuildTarget[]
        {
            BuildTarget.StandaloneWindows, BuildTarget.Android, BuildTarget.iOS, BuildTarget.StandaloneOSX
        };

        public static EngagePlatform ToEngagePlatform(ICollection<BuildTarget> targets)
        {
            EngagePlatform engagePlatform = EngagePlatform.None;

            foreach (var target in targets)
            {
                engagePlatform |= target.ToEngagePlatform();
            }

            return engagePlatform;
        }

        public static void QueueBuildJob(BundleBuildJob job)
        {
            JobQueue.Enqueue(job);
        }

        /// <summary>
        /// CLI implementation of method for queueing up build jobs by passing the asset GUID.
        /// </summary>
        public static void QueueBuildJobCLI()
        {
            var args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-job")
                {
                    for (int j = ++i; j < args.Length; j++, i++)
                    {
                        var guid = args[j];

                        if (string.IsNullOrEmpty(guid))
                        {
                            continue;
                        }

                        if (guid.StartsWith("-"))
                        {
                            break;
                        }

                        var job = new BundleBuildJob(guid);
                        job.BuildTargets |= EditorUserBuildSettings.activeBuildTarget.ToEngagePlatform();
                        QueueBuildJob(job);
                    }
                }
            }

            BuildAssetBundles();
        }

        public static void BuildAssetBundlesSerial()
        {
            var moduleManager = new ModuleManager();
            var buildTargets = new List<BuildTarget>(moduleManager.GetInstalledModules());

            if (buildTargets.Remove(EditorUserBuildSettings.activeBuildTarget))
            {
                buildTargets.Insert(0, EditorUserBuildSettings.activeBuildTarget);
            }

            foreach(var target in buildTargets)
            {
                BuildAssetBundles(target);

                while(processingJobQueue.Count > 0)
                {
                    JobQueue.Enqueue(processingJobQueue.Dequeue());
                }
            }
        }

        public static void BuildAssetBundles() => BuildAssetBundles(EditorUserBuildSettings.activeBuildTarget);

        public static void BuildAssetBundles(BuildTarget buildTarget)
        {
            var outputPath = new DirectoryInfo(BuildSettings.GetLocalBuildPath(buildTarget));
            var buildSet = new List<AssetBundleBuild>();

            while (JobQueue.Count > 0)
            {
                var job = JobQueue.Dequeue();

                if (job.BuildTargets.HasFlag(buildTarget.ToEngagePlatform()))
                {
                    buildSet.Add(
                        new AssetBundleBuild()
                        {
                            assetBundleName = job.BundleLabel.ToLower().Replace(' ', '_'),
                            assetNames = job.GetAssetList()
                        });

                    try
                    {
                        File.Delete($"{Path.Combine(outputPath.FullName, job.BundleLabel)}.engageBundle");
                        File.Delete($"{Path.Combine(outputPath.FullName, job.BundleLabel)}.manifest");
                    }
                    catch (IOException ex)
                    {
                        Debug.Log($"[BundleBuilder] Unable to delete {Path.Combine(outputPath.FullName, job.BundleLabel)}: {ex}");
                    }

                    job.BuildTargets &= ~(buildTarget.ToEngagePlatform());
                }

                if (job.BuildTargets > EngagePlatform.None)
                {
                    processingJobQueue.Enqueue(job);
                }
            }

            Debug.Log($"[BundleBuilder] Building {buildSet.Count} {(buildSet.Count == 1 ? "bundle" : "bundles")} for platform ({buildTarget})");

            if (buildSet.Count == 0)
            {
                return;
            }

            if (!outputPath.Exists)
            {
                outputPath.Create();
            }

            AssetBundleManifest buildManifest = null;

            try
            {
                buildManifest = BuildPipeline.BuildAssetBundles(
                    outputPath.FullName,
                    buildSet.ToArray(),
                    BuildAssetBundleOptions.ChunkBasedCompression,
                    buildTarget);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BundleBuilder] Build error: {e}");
            }

            if (buildManifest == null)
            {
                Debug.LogError($"[BundleBuilder] Build manifest was null. There may have been a problem with the build job");
                return;
            }

            var validationList = buildSet.Select(bundle => bundle.assetBundleName).ToList();

            foreach (string bundleName in buildManifest.GetAllAssetBundles())
            {
                var engageBundle = new FileInfo(Path.Combine(outputPath.FullName, bundleName));
                var destinationPath = $"{engageBundle.FullName}.engagebundle";

                if (!validationList.Remove(bundleName))
                {
                    Debug.LogWarning($"[BundleBuilder] Manifest bundle {bundleName} was not found in submitted build jobs. There may have been a problem with the build job");
                }

                try
                {
                    if (engageBundle.Exists)
                    {
                        File.Delete(destinationPath);
                        engageBundle.MoveTo(destinationPath);
                        Debug.Log($"[BundleBuilder] {bundleName} saved to {destinationPath}.");
                    }
                    else
                    {
                        Debug.LogError($"[BundleBuilder] The new bundle file cannot be located at {engageBundle.FullName}");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[BundleBuilder] There was a problem moving {engageBundle.FullName}" + e);
                }
            }

            foreach(var bundle in validationList)
            {
                Debug.LogWarning($"[BundleBuilder] Build job {bundle} was not found in build Manifest. There may have been a problem with the build job");
            }
        }

        public static void Start(this BundleBuildJob job, BuildTarget buildTarget)
        {
            job.Clear(buildTarget.ToEngagePlatform());

            EngagePlatform platform = buildTarget.ToEngagePlatform();

            if (!InstalledBuildModules.Contains(buildTarget))
            {
                Debug.LogWarning($"[BundleBuildJob] Build Module for {buildTarget} is not available.");
                job.Fail(platform);
                return;
            }

            var buildJob = new AssetBundleBuild()
            {
                assetBundleName = job.BundleLabel,
                assetNames = job.GetAssetList()
            };

            var outputPath = new DirectoryInfo(BuildSettings.GetLocalBuildPath(buildTarget));

            if (!outputPath.Exists)
            {
                outputPath.Create();
            }

            AssetBundleManifest buildManifest = null;

            try
            {
                buildManifest = BuildPipeline.BuildAssetBundles(
                    outputPath.FullName,
                    new AssetBundleBuild[] { buildJob },
                    BuildAssetBundleOptions.ChunkBasedCompression,
                    buildTarget);
            }
            catch (Exception e)
            {
                Debug.LogError($"[BundleBuilder] Build error: {e}");
            }

            if (buildManifest == null)
            {
                Debug.LogError($"[BundleBuilder] Build manifest was null. Build job failed to build {job.BundlePath} for {buildTarget}");
                job.Fail(platform);
                return;
            }

            foreach (string bundleName in buildManifest.GetAllAssetBundles())
            {
                var engageBundle = new FileInfo(Path.Combine(outputPath.FullName, bundleName));
                var destinationPath = $"{engageBundle.FullName}.engagebundle";

                try
                {
                    if (engageBundle.Exists)
                    {
                        File.Delete(destinationPath);
                        engageBundle.MoveTo(destinationPath);
                        Debug.Log($"[BundleBuilder] {bundleName} saved to {destinationPath}.");
                    }
                    else
                    {
                        Debug.LogError($"[BundleBuilder] The new bundle file cannot be located at {engageBundle.FullName}");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[BundleBuilder] There was a problem moving {engageBundle.FullName}" + e);
                }
            }

            job.Complete(platform);
        }
    }
}
