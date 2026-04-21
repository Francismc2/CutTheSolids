using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using IFXToolSM = IFXTools.IFXToolsStaticMethods;
/////////////////////////////////////
//Check for asset labels on prefabs both in and out of selected bundle folder.
//mesh combine before build = less draw calls?
////////////////////////////////////
namespace IFXTools {
    public class IFXToolsQualityCheckTool : EditorWindow
    {
        IFXBundleTools bundleTool;
        public void Init(IFXBundleTools BundleToolIN)
        {
            bundleTool = BundleToolIN; 
        }
        List<QACheckListItem> checkList= new List<QACheckListItem>();
        BundleBuildSettings buildSettingsStored;
        
        //UI Varaiables
        Vector2 scrollPos;
        private void OnGUI() 
        {
            IFXToolsQualityCheckToolUI();
            this.Repaint();
        }
        void IFXToolsQualityCheckToolUI()
        {
            Rect groupRect=new Rect(5, 25, Screen.width-20, Screen.height-10);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(groupRect.width), GUILayout.Height(groupRect.height-175));                                                    
            foreach (QACheckListItem item in checkList)
            {                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Name: "+item.rootGameObject.name, EditorStyles.boldLabel);
                foreach (string error in item.errors)
                {
                   GUILayout.Label("Errors: "+error);
                }
                if (GUILayout.Button("open prefab"))
                {
                    AssetDatabase.OpenAsset(item.rootGameObject);                            
                    this.Close();                    
                }
                if (GUILayout.Button("Auto Fix prefab"))
                {
                    IFXToolSM.FixPrefabs(item.rootGameObject);                     
                }               
                GUILayout.EndVertical();
                GUILayout.Label(" ");
            }

            GUILayout.Label(" ");
            GUILayout.Label(" ");                    
            EditorGUILayout.EndScrollView(); 

            if (GUI.Button(new Rect(10, groupRect.height - 175, groupRect.width, 50), "Recheck Bundle"))
            {
                BundleQualityCheck(buildSettingsStored);
            }        

            if (GUI.Button(new Rect(10, groupRect.height - 125, groupRect.width, 50), "Ignore Remaining Issues"))
            {
                buildSettingsStored.buildQACheckOverride = true;
                bundleTool.BuildSelectedBundle(buildSettingsStored);                
                this.Close();
            }

            if (GUI.Button(new Rect(10, groupRect.height - 75, groupRect.width, 50), "Cancel"))
            {
                this.Close();
            }        
    
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////// -----Functions-----///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        public  void OpenQAErrorWindow()
        {
            this.minSize=new Vector2(500,200);
            this.Show();
        }               
        public bool BundleQualityCheck(BundleBuildSettings buildSettings)// Also clears asset labels from prefabs while it's at it.
        {
            buildSettingsStored = buildSettings;
            
            bool QualityCheck = true;
            checkList.Clear();
            foreach (UnityEngine.Object item in buildSettings.selectedBundles)
            {
                string path = AssetDatabase.GetAssetPath(item);                
                
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

                //loop through directory loading the game object and checking if it has the component you want
                if (fileInf!=null)
                {
                    foreach (FileInfo file in fileInf)
                    {                       
                        string fullPath = file.FullName.Replace(@"\","/");
                        string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
                        GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;                       
                        
                        if(prefab!= null)
                        {
                            List<string> errors = IFXToolSM.PrefabQualityCheck(prefab);
                            if (errors.Count>0)
                            {
                                checkList.Add(new QACheckListItem(prefab,errors));
                                QualityCheck = false;
                                OpenQAErrorWindow();
                            } 
                        }
                    }
                }
                else
                {
                    Debug.Log("Failed to find prefabs in bundle");
                }
            }                
            return QualityCheck;            
        }
        
}
  
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////// -----Check list Item class-----///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class QACheckListItem
    {
        public GameObject rootGameObject;
        public List<string> errors;
        public QACheckListItem(GameObject goIN,List<string> errorsIN)
        {
            rootGameObject = goIN;
            errors = errorsIN;
        }
    }                   
}
    
        
