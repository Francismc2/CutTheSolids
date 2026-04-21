using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class QuickBatchBuild : EditorWindow
{
    [MenuItem("Assets/QuickBatchBuild")]
    // Start is called before the first frame update
    
    static void Init()
    {
            
        EditorWindow window = GetWindow(typeof(QuickBatchBuild));
        //window.minSize=new Vector2(500,600);

        window.Show();

    }
    
    void OnGUI()
    {
        if (GUILayout.Button("Quick Batch build Selected Bundles - "))
        {
            ClearAllAssetLabelsInProject();
            foreach (UnityEngine.Object item in Selection.objects)
            {
                //Debug.Log("Render Components test");
                SetAssetLabelToFolderName(item);
            }
            
            AssetBundles.BuildScript.BuildAssetBundles();
            //BuildAssetBundlesByTargetBatch(0);
        }
    //     // EditorGUILayout.LabelField(" ");
    //     // EditorGUILayout.LabelField(" ");
    //     // EditorGUILayout.LabelField(" ");
    //     // EditorGUILayout.LabelField(" ");
    //     if (GUILayout.Button("Quick Batch build Selected Bundles - Android"))
    //     {
    //         ClearAllAssetLabelsInProject();
    //         foreach (UnityEngine.Object item in Selection.objects)
    //         {
    //             //Debug.Log("Render Components test");
    //             SetAssetLabelToFolderName(item);
    //         }
    //         BuildAssetBundlesByTargetBatch(1);
    //     }
    //     EditorGUILayout.LabelField(" ");
    //     EditorGUILayout.LabelField(" ");
    //     EditorGUILayout.LabelField(" ");
    //     EditorGUILayout.LabelField(" ");
    //     if (GUILayout.Button("Quick Batch build Selected Bundles - iOS"))
    //     {
    //         ClearAllAssetLabelsInProject();
    //         foreach (UnityEngine.Object item in Selection.objects)
    //         {
    //             //Debug.Log("Render Components test");
    //             SetAssetLabelToFolderName(item);
    //         }
    //         BuildAssetBundlesByTargetBatch(2);
    //     }          
    // }

    // private static void BuildAssetBundlesByTargetBatch (int BuildTargetType)
    //     {

            
    //         string path = Application.dataPath.Replace("/Assets", "") +"/AssetBundles";
    //         if (!Directory.Exists(path))
    //         {
    //             Directory.CreateDirectory(path);
    //         }
            
    //         AssetBundleManifest manifest;
    //         switch(BuildTargetType) 
    //         {
                
    //         case 0 :
    //             path = path + "/Windows";
    //             if (!Directory.Exists(path))
    //             {
    //                 Directory.CreateDirectory(path);
    //             }
    //             manifest = BuildPipeline.BuildAssetBundles (path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    //             break;
    //         case 1 :
    //             path = path + "/Android";
    //             if (!Directory.Exists(path))
    //             {
    //                 Directory.CreateDirectory(path);
    //             }
    //             manifest = BuildPipeline.BuildAssetBundles (path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
    //             break;
                
    //         case 2 :
    //             path = path + "/iOS";
    //             if (!Directory.Exists(path))
    //             {
    //                 Directory.CreateDirectory(path);
    //             }
    //             manifest = BuildPipeline.BuildAssetBundles (path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
    //             break;

    //         default:
    //             manifest=null;
    //             Debug.Log("Build target not recognized"); 
    //             break; 
    //         }
    //         if (manifest!=null)
    //         {
    //             //renames to add .engagebundle
    //             foreach (string bund in manifest.GetAllAssetBundles())
    //             {
    //                 Debug.Log("Saving " + bund);

    //                 if (File.Exists(Path.Combine(path, bund + ".engagebundle")))
    //                 {
    //                     File.Delete(Path.Combine(path, bund + ".engagebundle"));
    //                 }

    //                 File.Move(Path.Combine(path, bund), Path.Combine(path, bund + ".engagebundle"));
    //                 Debug.Log("Saved " + bund + " successfully");
    //             }
    //         }
            
            

                
            
        }

    static void SetAssetLabelToFolderName(UnityEngine.Object item)
    {
        
        
        //This part changes the asset labelsS
        var itemPath = AssetDatabase.GetAssetPath(item);
        var itemDirectory = Path.GetDirectoryName(itemPath);
        var itemFolderName = Path.GetFileName(itemPath);//was itemDirectory, this is what it should be when intergrated into DB tool

        AssetImporter assetImporterForSelection = AssetImporter.GetAtPath(itemPath);//was itemDirectory, this is what it should be when intergrated into DB tool
                                                                                    //Debug.Log(assetImporterForSelection.assetBundleName);
                                                                                    //Debug.Log("this is the item dir:" + itemFolderName);
        assetImporterForSelection.assetBundleName = itemFolderName;

        //Debug.Log(assetImporterForSelection.assetBundleName);
    }

    public static void ClearAllAssetLabelsInProject()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (var name in names)
            {
                AssetDatabase.RemoveAssetBundleName(name,true);
            }
        }
}
