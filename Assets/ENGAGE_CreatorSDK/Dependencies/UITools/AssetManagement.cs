using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Engage.BuildTools
{
    public static class AssetManagementSystemSetupUtility
    {
        private const string NewtonsoftJsonPackage = "com.unity.nuget.newtonsoft-json";

        [MenuItem("ENGAGE/Add Newtonsoft Json Package", priority = 2101)]
        private async static Task AddNewtonsoftJsonPackage()
        {
            bool newtonsoftJsonInstalled = await CheckForDependencies();

            if (newtonsoftJsonInstalled)
            {
                EditorUtility.DisplayDialog("Newtonsoft Json", "Newtonsoft Json package for Unity is already installed", "OK");
                return;
            }

            if (EditorUtility.DisplayDialog("Newtonsoft Json", "Install Newtonsoft Json package for Unity?", "OK", "Cancel"))
            {
                AddRequest AddRequest = Client.Add(NewtonsoftJsonPackage);

                while (!AddRequest.IsCompleted)
                {
                    await Task.Yield();
                }

                if (AddRequest.Status == StatusCode.Success)
                {
                    Debug.Log("[Unity Package Manager] Installed: " + AddRequest.Result.packageId);
                    EditorUtility.DisplayDialog("Newtonsoft Json", "Newtonsoft Json package installed successfully.", "OK");
                }
                else if (AddRequest.Status >= StatusCode.Failure)
                {
                    EditorUtility.DisplayDialog("Newtonsoft Json", $"Newtonsoft Json package installation failed with status: {AddRequest.Status}", "OK");
                    Debug.Log("[Unity Package Manager] Installation failed: " + AddRequest.Error.message);
                }
            }
        }

        private static FileInfo RequestAssetManagementSystemPackage(DirectoryInfo currentDirectory = null)
        {
            if (currentDirectory == null)
            {
                currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            }

            return new FileInfo(EditorUtility.OpenFilePanel("Asset Management System Package", currentDirectory.FullName, "unitypackage"));
        }
        
        private static FileInfo RequestHostsDataFile(DirectoryInfo currentDirectory = null)
        {
            if (currentDirectory == null)
            {
                currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            }

            return new FileInfo(EditorUtility.OpenFilePanel("Hosts Data File", currentDirectory.FullName, "json"));
        }

        private async static Task<bool> ImportPackage(FileInfo packagePath)
        {
            if (packagePath.Exists)
            {
                await Task.Yield();

                AssetDatabase.ImportPackage(packagePath.FullName, true);
                Debug.Log($"[Asset Manager Installation] Asset Mangement System package imported.");
                return true;
            }
            else
            {
                Debug.Log($"[Asset Manager Installation] Asset Mangement System package path does not exist.");
                return false;
            }
        }

        [MenuItem("ENGAGE/Setup Asset Management System", priority = 2102)]
        private async static void SetupAssetManagementSystem()
        {
            if (EditorUtility.DisplayDialog("Engage Asset Management System", "Install the Engage Asset Management System?", "OK", "Cancel"))
            {
                await AddNewtonsoftJsonPackage();
                ExcludeEngageInternalFromGit();

                if (EditorUtility.DisplayDialog("Engage Asset Management System Pacakge", $"Please specify the location of the Asset Management System package", "OK"))
                {
                    var asmPackagePath = RequestAssetManagementSystemPackage();

                    if (asmPackagePath.Exists)
                    {
                        AssetDatabase.ImportPackage(asmPackagePath.FullName, true);
                    }
                }
            }
        }

        //[MenuItem("ENGAGE/Update .gitignore")]
        private static void ExcludeEngageInternalFromGit()
        {
            var dataPath = new DirectoryInfo(Application.dataPath);
            var gitignorePath = Path.Combine(dataPath.Parent.FullName, ".gitignore");

            if (!File.Exists(gitignorePath))
            {
                Debug.LogWarning($"Unable to locate '.gitignore' at '{gitignorePath}'");
                return;
            }

            try
            {
                var gitignore = File.ReadAllLines(gitignorePath);

                if (gitignore?.Any(line => line?.Contains("Assets/ENGAGE_Internal") ?? false) ?? false)
                {
                    return;
                }

                using (StreamWriter writer = File.AppendText(gitignorePath))
                {
                    writer.WriteLine("Assets/ENGAGE_Internal/");
                    writer.WriteLine("Assets/ENGAGE_Internal.meta");
                }

                Debug.Log($"Updated '.gitignore' at '{gitignorePath}':\n\tAssets/ENGAGE_Internal\n\tAssets/ENGAGE_Internal.meta");
            }
            catch(Exception ex)
            {
                Debug.LogError($"Unable to update '.gitignore' at '{gitignorePath}': {ex}");
            }
        }

        private async static Task<bool> CheckForDependencies()
        {
            bool packageInstalled = false;
            ListRequest ListRequest = Client.List();

            while (!ListRequest.IsCompleted)
            {
                await Task.Yield();
            }

            if (ListRequest.Status == StatusCode.Success)
            {
                packageInstalled = ListRequest.Result.Any(package => package.name == NewtonsoftJsonPackage);
            }
            else if (ListRequest.Status >= StatusCode.Failure)
            {
                Debug.Log("[Unity Package Manager] Package List request failed: " + ListRequest.Error.message);
            }

            if (packageInstalled)
            {
                Debug.Log($"[Asset Manager Installation] Newtonsoft Json package for Unity installed.");
            }

            return packageInstalled;
        }
    }
}
