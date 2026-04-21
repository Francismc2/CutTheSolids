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
namespace IFXTools{
public class IFXToolsBasicBundleBuilder : EditorWindow {

    [MenuItem("Assets/IFX Tools - basic")]
    [MenuItem("Creator SDK/IFX Tools Basic- Beta")]
    private static void ShowWindow() {
        var window = GetWindow<IFXToolsBasicBundleBuilder>();
        window.titleContent = new GUIContent("IFXTools Basic Bundle Builder");
        window.Show();
    }

     
    public bool windowsBuildYesNo{get; set;}
    public bool androidBuildYesNo {get; set;}
    public bool iOSBuildYesNo {get; set;}
    public bool macBuildYesNo {get; set;}
    public bool autoGitYesNo {get; set;}
    string unityEXELoc;
    string projectWinLoc;
    string projectAndroidLoc;
    string projectiOSLoc;
    string projectMacLoc;
    

    private void OnEnable() 
    {
        unityEXELoc= EditorApplication.applicationPath;

        projectWinLoc = Application.dataPath;
        projectWinLoc = projectWinLoc.Replace("/Assets", "");
        projectAndroidLoc = projectWinLoc+"/IFXBuildToolProjects/Android";
        projectiOSLoc = projectWinLoc+"/IFXBuildToolProjects/iOS";
        projectMacLoc = projectWinLoc+"/IFXBuildToolProjects/Mac";
    }
    private void OnGUI() 
    {

        EditorGUILayout.LabelField("How To Use:");
        EditorGUILayout.LabelField("Select The folders that you want made into a bundles In  the project window");
        EditorGUILayout.LabelField("Check the platforms you wish to build for");
        EditorGUILayout.LabelField("Click AssetBundle From Selection button");

        EditorGUILayout.LabelField("");

        EditorGUILayout.LabelField("The first time you build in a project can take several hours");
        EditorGUILayout.LabelField("Subsequent builds will be much faster");
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("");
        
        windowsBuildYesNo = EditorGUILayout.Toggle("Build for Windows?", windowsBuildYesNo);
        macBuildYesNo = EditorGUILayout.Toggle("Build for mac?", macBuildYesNo);
        androidBuildYesNo = EditorGUILayout.Toggle("Build for Android?", androidBuildYesNo);
        iOSBuildYesNo = EditorGUILayout.Toggle("Build for iOS?", iOSBuildYesNo);
        
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("");
        if (GUILayout.Button("AssetBundle From Selection"))
        {
            
            
            if(IFXToolSM.SelectionIsDirectoryBool(Selection.objects) == false)
            {
                EditorUtility.DisplayDialog("WARNING!", "Please Only Have FOLDERS Selected", "OK", "Cancel");
            }
            else
            {
                List<Object> selectedBundles = new List<Object>();
                foreach (var dir in Selection.objects)
                {
                    //check if the bundles has scene files and if they are the only file type present
                    if(IFXToolSM.DoesSelectedFolderContainOnlyScenes(dir)==false && IFXToolSM.DoesSelectedFolderContainFileType(dir,"*.unity") == true)
                    {
                        EditorUtility.DisplayDialog("WARNING!", "When Building scenes please ensure ONLY scene files are in the selected folder", "OK", "Cancel");
                        return;
                    }
                    //add the bundle so long as the above dosn't trigger
                    selectedBundles.Add(dir);                        
                }
                BasicBundleBuildSettings buildSettings = new BasicBundleBuildSettings();
                buildSettings.selectedBundles = selectedBundles;
                buildSettings.windowsBuildYesNo = windowsBuildYesNo;
                buildSettings.androidBuildYesNo = androidBuildYesNo;
                buildSettings.iOSBuildYesNo = iOSBuildYesNo;
                buildSettings.macBuildYesNo = macBuildYesNo;
                

                // Build the bundles!
                BuildSelectedBundle(buildSettings);                    
            }  
        }
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Thumbnail Tool is currently WIP");
        EditorGUILayout.LabelField("");
        if (GUILayout.Button("Thumbnail Tool"))
        {
            EditorWindow window = GetWindow(typeof(IFXThumbnailToolWindow));
            window.Show();
        }
    }
        
        List<Object> selectedBundles {get; set;}        
        
       
        void BuildSelectedBundle(BasicBundleBuildSettings buildSettings)
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
            


            
            /////////////////////////////////////////////////////////////^Set Up ^///////////////////////////
            
            if (buildSettings.selectedBundles !=null)
            {                            
                IFXToolSM.ClearAllAssetLabelsInProject();
                
                for (int i = 0; i < buildSettings.selectedBundles.Count; i++)
                {
                    //re add asset labels based on folders names to selected folders
                    SetAssetLabelToFolderName(buildSettings.selectedBundles[i]);                   
                }        
                buildBundlesBasic();
                
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

            
            

            void buildBundlesBasic ()
            {
                File.Delete("Bundle_Output_Log.txt");//delete the old log

                // if true build windows 
                if (buildSettings.windowsBuildYesNo)
                {
                    
                    IFXToolSM.DeleteFolderContents(projectWinLoc+ "/AssetBundles/Windows"); //clears out old bundles                
                    //Build the bundle
                    AssetBundles.BuildScript.BuildAssetBundles();            
                }
                
                
                
                
                // if true build android 
                if (buildSettings.androidBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(projectAndroidLoc + "/AssetBundles/Android"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(projectWinLoc+ "/AssetBundles/Android"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/Android_Temp.bat";
                    string androidBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("Android",projectWinLoc,true), CreateAndroidBatchFile(buildSettings));
                    //RunBuildFileAsync(androidBuildPath, "Android");
                    RunBatchFileAsync(androidBuildPath);
                    //Debug.Log(androidBuild.Output);
                }
                // if true build iOS 
                if (buildSettings.iOSBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(projectiOSLoc + "/AssetBundles/iOS"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(projectWinLoc+ "/AssetBundles/iOS"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/iOS_Temp.bat";
                    string iOSBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("iOS",projectWinLoc,true), CreateiOSBatchFile(buildSettings));
                    //RunBuildFileAsync(iOSBuildPath, "iOS");
                    RunBatchFileAsync(iOSBuildPath);
                    // Git stuff handled in batch file!
                }
                // if true build Mac 
                if (buildSettings.macBuildYesNo)
                {
                    IFXToolSM.DeleteFolderContents(projectMacLoc + "/AssetBundles/Mac"); //clears out old bundles
                    IFXToolSM.DeleteFolderContents(projectWinLoc+ "/AssetBundles/Mac"); //clears out old bundles
                    string commandFilePath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/Mac_Temp.bat";
                    string macBuildPath = IFXToolSM.CreateBatchCMDSFile(commandFilePath, IFXToolSM.SyncUnityProjects("Mac",projectWinLoc,true), CreateMacBatchFile(buildSettings));
                    //RunBuildFileAsync(iOSBuildPath, "iOS");
                    RunBatchFileAsync(macBuildPath);
                    // Git stuff handled in batch file!
                }
               
               
                
        
        void RunBatchFileAsync(string path)
        {
            FileInfo info = new FileInfo(path);
            ProcessStartInfo startInfo = new ProcessStartInfo(info.FullName);       
        
            
            var process = new Process();           
            process.EnableRaisingEvents=true;           
           
            
            process.StartInfo=startInfo;
            process.Start();
               
        }
        
        
        List<string> CreateAndroidBatchFile(BasicBundleBuildSettings buildSettingsIN)
        {
            List<string> commands = new List<string>();           
            
            string batchmode = "";
                             
            commands.Add("\""+unityEXELoc+"\" -quit "+batchmode+" -buildTarget \"Android\" -projectPath \""+projectWinLoc+"/IFXBuildToolProjects/Android"+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+projectWinLoc +"/IFXBuildToolProjects/Android/AssetBundles/Android"+"\""+" "+"\""+projectWinLoc+"/AssetBundles/Android"+"\"");
            return commands;
            
        }
        List<string> CreateiOSBatchFile(BasicBundleBuildSettings buildSettingsIN)
        {
            List<string> commands = new List<string>();           
            
            string batchmode = "";
                              
            commands.Add("\""+unityEXELoc+"\" -quit "+batchmode+" -buildTarget \"iOS\" -projectPath \""+projectWinLoc+"/IFXBuildToolProjects/iOS"+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+projectWinLoc +"/IFXBuildToolProjects/iOS/AssetBundles/iOS"+"\""+" "+"\""+projectWinLoc+"/AssetBundles/iOS"+"\"");
            return commands;
            
        }
        List<string> CreateMacBatchFile(BasicBundleBuildSettings buildSettingsIN)
        {
            List<string> commands = new List<string>();           
            
            string batchmode = "";
                              
            commands.Add("\""+unityEXELoc+"\" -quit "+batchmode+" -buildTarget \"OSXUniversal\" -projectPath \""+projectWinLoc+"/IFXBuildToolProjects/Mac"+"\" -executeMethod AssetBundles.BuildScript.BuildAssetBundles");
            commands.Add("robocopy "+"\""+projectWinLoc +"/IFXBuildToolProjects/Mac/AssetBundles/OSX"+"\""+" "+"\""+projectWinLoc+"/AssetBundles/Mac"+"\"");
            return commands;
            
        }
               
    }
 }
}
}




    

    public  class BasicBundleBuildSettings
     {
        public List<Object> selectedBundles {get; set;}
        public bool windowsBuildYesNo {get; set;}
        public bool androidBuildYesNo {get; set;}
        public bool iOSBuildYesNo {get; set;}
        public bool macBuildYesNo {get; set;}
        
        
     }
    
              
        