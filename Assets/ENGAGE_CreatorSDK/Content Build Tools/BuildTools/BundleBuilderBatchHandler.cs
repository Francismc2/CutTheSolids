using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public static partial class BundleBuilder
    {
        public class BatchHandler
        {
            /* 
                Unity CLI -buildTarget options
             
                • Standalone
                • Win
                • Win64
                • OSXUniversal
                • Linux64
                • iOS
                • Android
                • WebGL
                • WindowsStoreApps
                • tvOS
            */

            private static string GetBuildTargetParameter(BuildTarget buildTarget)
            {
                string param = "-buildTarget \"{0}\"";

                switch (buildTarget)
                {
                    case BuildTarget.StandaloneWindows: return string.Format(param, "Win64");
                    case BuildTarget.StandaloneOSX: return string.Format(param, "OSXUniversal");
                    case BuildTarget.iOS: return string.Format(param, "iOS");
                    case BuildTarget.Android: return string.Format(param, "Android");
                    default: return string.Empty;
                }
            }

            public void RunBatchFile(string path)
            {
                FileInfo info = new FileInfo(path);

                if (!info.Exists)
                {
                    return;
                }

                try
                {
                    var startInfo = new ProcessStartInfo(info.FullName);

                    using (var process = new Process())
                    {
                        process.EnableRaisingEvents = true;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[Bundle Builder] Batch Process Exception: {ex}");
                }
            }

            public List<string> GenerateUnityProjectSyncCommands(BuildTarget buildTarget)
            {
                var syncIncantation = "robocopy \"{0}\" \"{1}\" /mir /j";

                var projectDir = new DirectoryInfo(Application.dataPath).Parent;
                var cacheDir = new DirectoryInfo(BuildSettings.GetLocalProjectCache(buildTarget));

                var commands = new List<string>();

                if (buildTarget == BuildTarget.StandaloneOSX)
                    commands.Add($"rename \"{Path.Combine(BuildSettings.GetLocalProjectCache(), "Mac")}\" \"{buildTarget.ToEngagePlatform()}\"");

                commands.Add($"mkdir \"{cacheDir.FullName}\"");

                commands.Add(string.Format(syncIncantation, Path.Combine(projectDir.FullName, "Assets"), Path.Combine(cacheDir.FullName, "Assets")) + " /XD ENGAGE_Internal");
                commands.Add(string.Format(syncIncantation, Path.Combine(projectDir.FullName, "ProjectSettings"), Path.Combine(cacheDir.FullName, "ProjectSettings")));
                commands.Add(string.Format(syncIncantation, Path.Combine(projectDir.FullName, "Packages"), Path.Combine(cacheDir.FullName, "Packages")));

                return commands;
            }

            public string CreateBatchBuildFile(BundleBuildJob job, BuildTarget buildTarget)
            {
                return CreateBatchBuildFile(new BundleBuildJob[] { job }, buildTarget);
            }

            public string CreateBatchBuildFile(BundleBuildJob[] jobs, BuildTarget buildTarget)
            {
                var batchFileName = $"{buildTarget.ToEngagePlatform()}_{DateTime.Now.ToString("yyyy-MM-dd [HH'h'mm]")}.bat";
                var batchFile = new FileInfo(Path.Combine(BuildSettings.GetLocalJobCache(), batchFileName));

                if (!batchFile.Directory.Exists)
                {
                    batchFile.Directory.Create();
                }

                using (var writer = new StreamWriter(batchFile.FullName, false))
                {
                    writer.WriteLine("ECHO off\n");

                    foreach (var command in GenerateUnityProjectSyncCommands(buildTarget))
                    {
                        writer.WriteLine(command);
                    }

                    writer.WriteLine("ECHO Queuing for build:");

                    foreach(var job in jobs)
                    {
                        writer.WriteLine($"ECHO \t{job.BundleLabel}");
                    }

                    writer.WriteLine("ECHO.");
                    writer.WriteLine("ECHO Building...");

                    writer.WriteLine(GenerateUnityCLIBuildCommand(jobs.Select(job => job.BundleGuid).ToArray(), buildTarget));
                    writer.WriteLine("ECHO Build complete.");

                    writer.WriteLine("TIMEOUT 10");
                }

                return batchFile.FullName;
            }

            public string GenerateUnityCLIBuildCommand(string[] jobGuids, BuildTarget buildTarget, bool batchMode = true)
            {
                var command = new List<string>();
                command.Add("\n");
                command.Add($"cd \"{BuildSettings.GetLocalProjectCache()}\"");
                command.Add("\n");
                command.Add($"\"{EditorApplication.applicationPath}\"");
                command.Add("-quit");

                if (batchMode)
                {
                    command.Add("-batchmode");
                }

                command.Add(GetBuildTargetParameter(buildTarget));
                command.Add($"-projectPath \"{buildTarget.ToEngagePlatform()}\"");
                command.Add($"-executeMethod {typeof(BundleBuilder).FullName}.{nameof(BundleBuilder.QueueBuildJobCLI)}");
                command.Add($"-job {string.Join(" ", jobGuids)}");
                command.Add("\n");

                return string.Join(" ", command);
            }
        }
    }
}
