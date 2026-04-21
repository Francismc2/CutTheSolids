using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Engage.BuildTools
{
    public enum AssetType
    {
        Undefined = 0,
        Location = 1,
        IFX = 2,
        Snapshot
    }

    public class BundleBuildJob
    {
        protected BundleBuildJob() { }

        public BundleBuildJob(string guid)
        {
            BundleGuid = guid;
            BundlePath = AssetDatabase.GUIDToAssetPath(guid);
            BundleLabel = Path.GetFileNameWithoutExtension(BundlePath);

            Initialize();
        }

        public string BundleGuid { get; }
        public string BundlePath { get; }
        public EngagePlatform BuildTargets { get; set; }
        public AssetType AssetType { get; set; }
        public string BundleLabel { get; set; }

        public bool IsQAPassed { get; protected set; }

        public EngagePlatform Completed { get; protected set; }
        public EngagePlatform Failed { get; protected set; }

        public bool buildQACheckOverride { get; set; }

        public Action<BundleBuildJob, EngagePlatform> OnComplete;
        public Action<BundleBuildJob, EngagePlatform> OnFail;

        public void Initialize()
        {
            UpdateAssetType();
            RunQATests();
        }

        public async void RunQATests()
        {
            //TODO: run QA tests for real
            //if (!buildQACheckOverride)
            //{
            //    var qaTool = UnityEngine.ScriptableObject.CreateInstance<IFXToolsQualityCheckTool>();

            //    // repaint

            //    IsQAPassed = await qaTool.BundleQualityCheck(this);

            //    // repaint
            //}

            IsQAPassed = true;
        }

        public void Fail(EngagePlatform platform)
        {
            Failed |= platform;
            OnFail?.Invoke(this, platform);
        }

        public void Complete(EngagePlatform platform)
        {
            Completed |= platform;
            OnComplete?.Invoke(this, platform);
        }

        public void Clear(EngagePlatform platform)
        {
            Failed &= ~platform;
            Completed &= ~platform;
        }

        public void Clear()
        {
            Failed = EngagePlatform.None;
            Completed = EngagePlatform.None;
        }

        public void UpdateAssetType()
        {
            if (AssetDatabase.IsValidFolder(BundlePath))
            {
                Debug.Log($"PATH: {BundlePath} (Valid Folder: {AssetDatabase.IsValidFolder(BundlePath)})");

                var bundlePath = new string[] { BundlePath };

                if (AssetDatabase.FindAssets("t:scene", bundlePath).Length > 0)
                {
                    AssetType = AssetType.Location;
                }
                else if (AssetDatabase.FindAssets("t:prefab", bundlePath).Length > 0)
                {
                    AssetType = AssetType.IFX;
                }
                else
                {
                    AssetType = AssetType.Undefined;
                }
            }
            else
            {
                var type = AssetDatabase.GetMainAssetTypeAtPath(BundlePath);

                if (type == typeof(SceneAsset))
                {
                    AssetType = AssetType.Location;
                }
                else if (type == typeof(GameObject))
                {
                    AssetType = AssetType.IFX;
                }
                else
                {
                    AssetType = AssetType.Undefined;
                }
            }
        }

        public string[] GetAssetList()
        {
            var assetList = new string[] { BundlePath };

            if (AssetDatabase.IsValidFolder(BundlePath))
            {
                if (AssetType == AssetType.Location)
                {
                    var guids = AssetDatabase.FindAssets("t:scene", assetList);
                    assetList = new string[guids.Length];

                    for (int i = 0; i < guids.Length; i++)
                    {
                        assetList[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                    }
                }
                else if (AssetType == AssetType.IFX)
                {
                    var guids = AssetDatabase.FindAssets("t:prefab", assetList);
                    assetList = new string[guids.Length];

                    for (int i = 0; i < guids.Length; i++)
                    {
                        assetList[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                    }
                }
            }

            return assetList;
        }

        /*
        public async void Start()
        {
            //State = JobState.UpdatingProject;

            try
            {
                await UpdateProjectCache();
            }
            catch (Exception ex)
            {
                //Logger.LogError($"[{Project.Name}-{Name}] project sync problem: {ex}");
                //State = JobState.Error;
                return;
            }

            //State = JobState.Building;

            try
            {
                await StartBuildQueue();
            }
            catch (Exception ex)
            {
                //Logger.LogError($"[{Project.Name}-{Name}] Unity build problem: {ex}");
                //State = JobState.Error;
                return;
            }

            //State = JobState.Complete;
        }

        public async Task UpdateProjectCache()
        {
            //if (State != JobState.UpdatingProject)
            //{
            //    Logger.LogError($"[{Project.Name}-{Name}] Job tried to start project sync from state: {State}");
            //    return;
            //}

            await Task.Run(() => SyncFolder(ProjectFolders.Assets));
            await Task.Run(() => SyncFolder(ProjectFolders.ProjectSettings));
            await Task.Run(() => SyncFolder(ProjectFolders.Packages));

            return;
        }

        public async Task StartBuildQueue()
        {
            //if (State != JobState.Building)
            //{
            //    Logger.LogError($"[{Project.Name}-{Name}] Job tried to start Unity build from state: {State}");
            //    return;
            //}

            await Task.Run(BuildBatch);
        }

        public List<string> GenerateProjectSyncArgs(ProjectFolders folder)
        {
            var commands = new List<string>()
            {
                Path.Combine(Project.Path, folder.ToString()),
                Path.Combine(Project.Path, Project.DefaultProjectFolder, Platform.ToString(), folder.ToString()),
                "/mir",
                "/j"
            };

            return commands;
        }

        protected string GetUnityCLIBuildTarget(EngagePlatform platform)
        {
            switch (platform)
            {
                case EngagePlatform.Windows: return "Win64";
                case EngagePlatform.OSX: return "OSXUniversal";
                case EngagePlatform.iOS: return "iOS";
                case EngagePlatform.Android: return "Android";
                default: return string.Empty;
            }
        }

        public void SyncFolder(ProjectFolders folder, params string[] arguments)
        {
            using (Process cmd = new Process())
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = "robocopy",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    RedirectStandardError = true
                };

                foreach (string arg in GenerateProjectSyncArgs(folder))
                {
                    info.ArgumentList.Add(arg);
                }

                if (folder == ProjectFolders.Assets)
                {
                    info.ArgumentList.Add("/XD");
                    info.ArgumentList.Add("ENGAGE_Internal");
                }

                cmd.StartInfo = info;
                cmd.Start();

                if (!cmd.WaitForExit(Settings.ProjectSyncTimeout))
                {
                    var error = cmd.StandardError.ReadToEnd();
                    cmd.Kill();
                    //var message = $"[{Project.Name}-{Name}] Job sync {folder} timed out{(!string.IsNullOrEmpty(error) ? ":\n" + error : "")}";
                    //Logger.LogError(message);
                    //throw new TimeoutException(message);
                }

                FoldersSynced |= folder;
                //Logger.Log($"[{Project.Name}-{Name}] Job synced {folder}");
            }
        }

        public void BuildBatch()
        {
            var command = new List<string>();

            command.Add("-quit");
            command.Add("-batchmode");
            command.Add("-projectPath");
            command.Add(Platform.ToString());
            command.Add("-buildTarget");
            command.Add(GetUnityCLIBuildTarget(Platform));
            command.Add("-executeMethod");
            command.Add(Settings.QueueBatchJobCommand);
            command.Add("-job");
            command.AddRange(Guids);

            using (var process = new Process())
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = Settings.UnityEditorPath,
                    WorkingDirectory = System.IO.Path.Combine(Project.Path, Project.DefaultProjectFolder),
                    Arguments = string.Join(" ", command),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                //Logger.Log($"[{Project.Name}-{Name}] Starting build process [{Platform}]:\n\t{info.FileName}\n\t{info.Arguments}");

                process.StartInfo = info;

                process.Start();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    //Logger.LogError(e.Data);
                };

                if (!process.WaitForExit(Settings.UnityBuildTimeout))
                {
                    var error = process.StandardError.ReadToEnd();
                    process.Kill();
                    //var message = $"[{Project.Name}-{Name}] Build process [{Platform}] timed out{(!string.IsNullOrEmpty(error) ? ":\n" + error : "")}";
                    //Logger.LogError(message);
                    //throw new TimeoutException(message);
                }

                var output = process.StandardOutput.ReadToEnd();

                //Logger.Log($"[{Project.Name}-{Name}] Build Completed [{Platform}]: {string.Join(", ", Guids)}");

                //if (!string.IsNullOrEmpty(output))
                //{
                //    Logger.Log(process.StandardOutput.ReadToEnd());
                //}
            }
        */
    }
}