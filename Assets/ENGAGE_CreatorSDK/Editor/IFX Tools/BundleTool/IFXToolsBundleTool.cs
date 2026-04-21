using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using systemDebug = System.Diagnostics;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
using IFXToolSM = IFXTools.IFXToolsStaticMethods;


 /////////////TO-DO////
 // if scenes are bing built change over all the auto git stuff paths to the scene versions

 namespace IFXTools{

     public  class BundleBuildSettings
     {
        public List<Object> selectedBundles {get; set;}
        public bool buildQACheckOverride {get; set;}
        public string gitCommitMessage {get; set;}
        public bool windowsBuildYesNo {get; set;}
        public bool androidBuildYesNo {get; set;}
        public bool iOSBuildYesNo {get; set;}
        public bool macBuildYesNo {get; set;}
        public bool fullSync {get; set;}
        
     }
    public class IFXBundleTools : EditorWindow
    {
              
        IFXToolsQualityCheckTool qaTool;
        IFXToolsUserSettings userSettings;
        List<Object> selectedBundles {get; set;}
        string bundleBuildLog {get; set;}
        string bundleBuildLogWindows {get; set;}
        //string bundleBuildLogAndroid {get; set;}
        //string bundleBuildLogiOS {get; set;}
        string bundleBuildLogGIT {get; set;}
        
        //System.Text.StringBuilder bundleBuildLog;
        public IFXBundleTools()
        {
  
        }
        public void Init(IFXToolsUserSettings userSettingsIN)
        {
            userSettings = userSettingsIN;
        }
        
               
        bool passedQualityCheck;       
       
        public void BuildSelectedBundle(BundleBuildSettings buildSettings)
        {   
            //check for module folders        
            if (buildSettings.androidBuildYesNo)
            {
                if (IFXToolSM.CheckBuildModuleInstalled("AndroidPlayer")==false)
                {
                    EditorUtility.DisplayDialog("WARNING!", "Android Build module not installed!", "OK");
                    return;                          
                }
                
            }
            if (buildSettings.iOSBuildYesNo)
            {
                if (IFXToolSM.CheckBuildModuleInstalled("iOSSupport")==false)
                {
                    EditorUtility.DisplayDialog("WARNING!", "iOS Build module not installed!", "OK");
                    return;                          
                }
            }
            if (buildSettings.macBuildYesNo)
            {
                if (IFXToolSM.CheckBuildModuleInstalled("MacStandaloneSupport")==false)
                {
                    EditorUtility.DisplayDialog("WARNING!", "Mac Build module not installed!", "OK");
                    return;                          
                }
            }
            passedQualityCheck = true;
            
            //local Cdn paths//To enable swaping from ifx to scenes
            string cdnLocalWinLoc = userSettings.cdnWinIFXLoc;
            string cdnLocalAndroidLoc = userSettings.cdnAndroidIFXLoc;
            string cdnLocaliOSLoc = userSettings.cdniOSIFXLoc;
            string cdnLocalMacLoc = userSettings.cdnMacIFXLoc;

            //is a scene being built
            bool SceneBuild = false;
            for (int i = 0; i < buildSettings.selectedBundles.Count; i++)
            {
                if(IFXToolSM.DoesSelectedFolderContainFileType(buildSettings.selectedBundles[i], "*.unity") == true)
                {
                    cdnLocalWinLoc = userSettings.cdnWinSceneLoc;
                    cdnLocalAndroidLoc = userSettings.cdnAndroidSceneLoc;
                    cdnLocaliOSLoc = userSettings.cdniOSSceneLoc;

                    SceneBuild = true;
                    passedQualityCheck=true;//quality check dosn';t have anything to check in scenes yet
                }          
            }  
            /////////////////////////////////////////////////////////////^Set Up ^///////////////////////////
            
            if (buildSettings.selectedBundles !=null)
            {                            
                IFXToolSM.ClearAllAssetLabelsInProject();
                
                for (int i = 0; i < buildSettings.selectedBundles.Count; i++)
                {
                    //re add asset labels based on folders names to selected folders
                    SetAssetLabelToFolderName(buildSettings.selectedBundles[i]);                   
                }                  
                
                //Checks for bad components
                if (!buildSettings.buildQACheckOverride)
                {
                    
                    qaTool = (IFXToolsQualityCheckTool)ScriptableObject.CreateInstance(typeof(IFXToolsQualityCheckTool));
                    qaTool.Init(this);
                    bool qaCheck = qaTool.BundleQualityCheck(buildSettings); //pass QA true or false
                    if (!qaCheck)
                    {
                        passedQualityCheck = false;
                    }
                    
                }
            ///////////////////////////////////////////////////////Passed QA Check - Build bundles////////////////////
                if (passedQualityCheck | buildSettings.buildQACheckOverride)
                {
                    buildSettings.buildQACheckOverride = false;                   
                    
                    buildBundles();
                }
            }
            else
            {
                Debug.Log("Nothing Selected - Select the folder you want built first");
            }


            ///////////////////////////////////////////Local Methods/////////////////////////////////////////////////
            void SetAssetLabelToFolderName(UnityEngine.Object item)
            {
                //This part changes the asset labelsS
                var itemPath = AssetDatabase.GetAssetPath(item);
                var itemDirectory = Path.GetDirectoryName(itemPath);
                var itemFolderName = Path.GetFileName(itemPath);
                AssetImporter assetImporterForSelection = AssetImporter.GetAtPath(itemPath);                                                                                            
                assetImporterForSelection.assetBundleName = itemFolderName;
            }

            
            
            void buildBundles()
            {            
                // if true build windows 
                if (buildSettings.windowsBuildYesNo)
                {
                    
                    IFXToolSM.DeleteFolderContents(userSettings.projectWinLoc+ "/AssetBundles/Windows"); //clears out old bundles                
                    //Build the bundle
                    AssetBundles.BuildScript.BuildAssetBundles();            
                }
                
                // if true build android 
                if (buildSettings.androidBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(userSettings.projectAndroidLoc + "/AssetBundles/Android"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(userSettings.projectWinLoc+ "/AssetBundles/Android"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/Android_Temp.bat";
                    bool guiEnable;
                    if (SceneBuild)
                    {
                        guiEnable = true;
                    }
                    else
                    {
                        guiEnable = userSettings.enableGuiOnBuilds;
                    }
                    string androidBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("Android",userSettings.projectWinLoc,buildSettings.fullSync), CreateAndroidBatchFile(buildSettings,cdnLocalAndroidLoc, guiEnable));
                    //RunBuildFileAsync(androidBuildPath, "Android");
                    RunBatchFile(androidBuildPath);
                    //Debug.Log(androidBuild.Output);
                }
                // if true build iOS 
                if (buildSettings.iOSBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(userSettings.projectiOSLoc + "/AssetBundles/iOS"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(userSettings.projectWinLoc+ "/AssetBundles/iOS"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/iOS_Temp.bat";
                    bool guiEnable;
                    if (SceneBuild)
                    {
                        guiEnable = true;
                    }
                    else
                    {
                        guiEnable = userSettings.enableGuiOnBuilds;
                    }
                    string iOSBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("iOS",userSettings.projectWinLoc,buildSettings.fullSync), CreateiOSBatchFile(buildSettings,cdnLocaliOSLoc, guiEnable));
                    //RunBuildFileAsync(iOSBuildPath, "iOS");
                    RunBatchFile(iOSBuildPath);
                    // Git stuff handled in batch file!
                }
                // if true build Mac 
                if (buildSettings.macBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(userSettings.projectMacLoc + "/AssetBundles/Mac"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(userSettings.projectWinLoc+ "/AssetBundles/Mac"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/Mac_Temp.bat";
                    bool guiEnable;
                    if (SceneBuild)
                    {
                        guiEnable = true;
                    }
                    else
                    {
                        guiEnable = userSettings.enableGuiOnBuilds;
                    }
                    string macBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("Mac",userSettings.projectWinLoc,buildSettings.fullSync), CreateMacBatchFile(buildSettings,cdnLocalMacLoc, guiEnable));
                    //RunBuildFileAsync(iOSBuildPath, "iOS");
                    RunBatchFile(macBuildPath);
                    // Git stuff handled in batch file!
                }
            }
        }
                
               
                 
               ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                

                
        
        void RunBatchFile(string path)
        {
            FileInfo info = new FileInfo(path);
            ProcessStartInfo startInfo = new ProcessStartInfo(info.FullName);       
        
            
            var process = new Process();           
            process.EnableRaisingEvents=true;           
           
            
            process.StartInfo=startInfo;
            process.Start();
               
        }
        
        
        public List<string> CreateAndroidBatchFile(BundleBuildSettings buildSettingsIN, string cdnLocalLoc,bool guiEnabled)
        {
            List<string> commands = new List<string>();
            if (userSettings.fullSyncProjectOnBuild == false)
            {
                foreach (var command in IFXToolSM.RoboCopyDependenciesFiles("Android",userSettings.projectWinLoc, buildSettingsIN.selectedBundles))
                {
                    commands.Add(command);
                }
            }
            
            string batchmode="-batchmode";
            if (guiEnabled ==true)
            {
               batchmode = "";
            }

            commands.Add("\""+EditorApplication.applicationPath+"\" -quit "+batchmode+" -buildTarget \"Android\" -projectPath \""+userSettings.projectAndroidLoc+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+userSettings.projectWinLoc +"/IFXBuildToolProjects/Android/AssetBundles/Android"+"\""+" "+"\""+userSettings.projectWinLoc+"/AssetBundles/Android"+"\"");
            if (cdnLocalLoc !=null)
            {
                if (!string.IsNullOrEmpty(userSettings.cdnAndroidIFXLoc))
                {
                    commands.Add("robocopy "+"\""+userSettings.projectAndroidLoc+"/AssetBundles/Android"+"\""+" "+"\""+cdnLocalLoc+"\"");
                }
            }
            return commands;
            
        }
        public List<string> CreateiOSBatchFile(BundleBuildSettings buildSettingsIN, string cdnLocalLoc,bool guiEnabled)
        {
            List<string> commands = new List<string>();            
            if (userSettings.fullSyncProjectOnBuild == false)
            {
                foreach (var command in IFXToolSM.RoboCopyDependenciesFiles("iOS",userSettings.projectWinLoc, buildSettingsIN.selectedBundles))
                {
                    commands.Add(command);
                }
            }
            string batchmode="-batchmode";
            if (guiEnabled ==true)
            {
               batchmode = "";
            }                      
            commands.Add("\""+EditorApplication.applicationPath+"\" -quit "+batchmode+" -buildTarget \"iOS\" -projectPath \""+userSettings.projectiOSLoc+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+userSettings.projectWinLoc +"/IFXBuildToolProjects/iOS/AssetBundles/iOS"+"\""+" "+"\""+userSettings.projectWinLoc+"/AssetBundles/iOS"+"\"");
            if (cdnLocalLoc !=null)
            {
                if (!string.IsNullOrEmpty(userSettings.cdniOSIFXLoc))
                {
                    commands.Add("robocopy "+"\""+userSettings.projectiOSLoc+"/AssetBundles/iOS"+"\""+" "+"\""+cdnLocalLoc+"\"");
                }
            }
            return commands;
            
        }
        List<string> CreateMacBatchFile(BundleBuildSettings buildSettingsIN, string cdnLocalLoc,bool guiEnabled)
        {
            List<string> commands = new List<string>();            
            if (userSettings.fullSyncProjectOnBuild == false)
            {
                foreach (var command in IFXToolSM.RoboCopyDependenciesFiles("Mac",userSettings.projectWinLoc, buildSettingsIN.selectedBundles))
                {
                    commands.Add(command);
                }
            }
            string batchmode="-batchmode";
            if (guiEnabled ==true)
            {
               batchmode = "";
            }                      
            commands.Add("\""+EditorApplication.applicationPath+"\" -quit "+batchmode+" -buildTarget \"OSXUniversal\" -projectPath \""+userSettings.projectMacLoc+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+userSettings.projectWinLoc +"/IFXBuildToolProjects/Mac/AssetBundles/OSX"+"\""+" "+"\""+userSettings.projectWinLoc+"/AssetBundles/Mac"+"\"");
            if (cdnLocalLoc !=null)
            {
                if (!string.IsNullOrEmpty(userSettings.cdnMacIFXLoc))
                {
                    commands.Add("robocopy "+"\""+userSettings.projectMacLoc+"/AssetBundles/OSX"+"\""+" "+"\""+cdnLocalLoc+"\"");
                }
            }
            return commands;
            
        }
         public void GitCommitChangesToRepo(List<Object> selectedBundleIN,string gitCommitM)
        {

            AssetDatabase.SaveAssets();//saves assets so all meta files show up
            List<string> commands = new List<string>();
            List<string> filesToAdd = new List<string>();
            filesToAdd.AddRange(IFXToolSM.GetFolderDependencies(selectedBundleIN));

            commands.Add("cd /D "+"\""+userSettings.projectWinLoc+"\"");
            commands.Add("git stash");
            commands.Add("git pull");
            commands.Add("git stash pop");
            commands.Add("git reset");
            foreach (var item in filesToAdd)
            {
                commands.Add("git add "+"\""+userSettings.projectWinLoc+"/"+item+"\"");
                commands.Add("git add "+"\""+userSettings.projectWinLoc+"/"+item+".meta"+"\"");
            }
            commands.Add("git status");
            commands.Add("git commit -m "+"\""+gitCommitM+"\"");
            commands.Add("git push");
            commands.Add("PAUSE");


            foreach (var item in commands)
            {
                Debug.Log(item);
            }
            string gitCommitBatch = IFXToolSM.CreateBatchCMDSFile("GitCommitToRepo",commands);
            RunBatchFile(gitCommitBatch);

        }
        
    }
 }