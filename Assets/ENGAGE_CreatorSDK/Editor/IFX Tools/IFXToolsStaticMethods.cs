using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using systemDebug = System.Diagnostics;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
namespace IFXTools{
    public class IFXToolsStaticMethods : ScriptableObject
    {


        //Bundle building related methods////////////////////////////////////////////////////////////////
        public static string[] GetAllScriptsInProject()
        {
            string[] filesOfType = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories );
            return filesOfType;    
        }


        public static bool CheckBuildModuleInstalled(string ModuleType)
        {
            string editorPath = EditorApplication.applicationPath;
            string editorPathTrimmed = editorPath.Substring(0, editorPath.Length-9);
            string ModulePath =editorPathTrimmed + "Data/PlaybackEngines/";
            Debug.Log(ModulePath+ModuleType);
            if (Directory.Exists(ModulePath+ModuleType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool DoesSelectedFolderContainFileType(UnityEngine.Object selectedFolder, string fileType)
        {
            string path = Path.GetFullPath(AssetDatabase.GetAssetPath(selectedFolder.GetInstanceID()));
            string[] filesOfType = Directory.GetFiles(path, "*.unity");
            if(filesOfType.Length > 0 )
            {
                return true;
            }            
            return false;
        }


        public static bool DoesSelectedFolderContainOnlyScenes(UnityEngine.Object selectedFolder)
        {
            string path = Path.GetFullPath(AssetDatabase.GetAssetPath(selectedFolder.GetInstanceID()));
            string[] files = Directory.GetFiles(path);
            
            string[] sceneFiles = Directory.GetFiles(path, "*.unity");
            string[] sceneMetaFiles = Directory.GetFiles(path, "*.unity.meta");
            
            if (files.Length == sceneFiles.Length + sceneMetaFiles.Length)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }


       public static void CopyFolderContents(string source,string destination)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
            {
                Debug.Log("Could not copy folder contents, source or destination is null ");
                return;
            }
            if (Directory.Exists(source))
            {                
                //Copy all files found in source folder to destination folder
                System.IO.DirectoryInfo sourceFolder = new DirectoryInfo(source);
                foreach (FileInfo file in sourceFolder.GetFiles())
                {
                    string destFile = System.IO.Path.Combine(destination, file.Name);
                    Debug.Log(destFile);
                    System.IO.File.Copy(file.FullName, destFile, true);
                }
            }
            else
            {
                Debug.Log("Could not copy folder contents, Folder doesn't exist: "+ source);
            }            
        }

        public static void DeleteFolderContents(string Folder)
        {   
            if (Directory.Exists(Folder))
            {
                System.IO.DirectoryInfo dir = new DirectoryInfo(Folder);
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete(); 
                }
            }
            else
            {
                Debug.Log("DeleteFolderContents: Can't delete contents as no folder exists at path");
            }            
        }

        public static void ClearAllAssetLabelsInProject()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (var name in names)
            {
                AssetDatabase.RemoveAssetBundleName(name,true);
            }
        }

        public static void GitPull(string cdnProjectPath)
        {
            // Declaration of the array
            List<string> commands= new List<string>();

            // Initialization of array
            commands.Add("cd /D "+"\""+cdnProjectPath+"\"");          

            //commands.Add("git stash");
            commands.Add("git pull");
            //commands.Add("git stash pop");
            
            RunCMD(commands);
            
        }

         public static List<string> SyncUnityProjects(string buildType,string WinProjectPath,bool fullSync)
        {
            List<string> commands = new List<string>();
            commands.Add("mkdir "+"\""+WinProjectPath.Replace("/","\\")+"\\IFXBuildToolProjects\\"+buildType+"\\AssetBundles\\"+buildType+"\"");            
            if (fullSync)
            {
                commands.Add("robocopy "+"\""+WinProjectPath+"\" "+WinProjectPath+"/IFXBuildToolProjects/"+buildType+" /MIR  /XD \"IFXBuildToolProjects\" \"Library\" \".git\" \"Temp\"");
                //commands.Add("robocopy "+"\""+WinProjectPath+"\" "+WinProjectPath+"/IFXBuildToolProjects/"+buildType+" /MIR  /XD "+"\""+WinProjectPath+"/IFXBuildToolProjects/"+"\"");      
            }
            else
            {
                commands.Add("robocopy "+"\""+WinProjectPath+"/Assets/ENGAGE_CreatorSDK"+"\""+ " " +"\""+WinProjectPath+"/IFXBuildToolProjects/"+buildType+"/Assets/ENGAGE_CreatorSDK"+"\""+" "+" /MIR /XD "+"\""+WinProjectPath+"/IFXBuildToolProjects"+"\"");
                commands.Add("robocopy "+"\""+WinProjectPath+"/ProjectSettings"+"\""+" "+"\""+WinProjectPath+"/IFXBuildToolProjects/"+buildType+"/ProjectSettings"+"\""+ " /MIR /XD "+"\""+WinProjectPath+"/IFXBuildToolProjects"+"\""); 
            }
            return commands;            
        }

        public static string CreateBatchCMDSFile(string pathforBatch,List<string> input,List<string> input2=null,List<string> input3=null)
        {

            List<string> commandsList = new List<string>();
            //commandsList.Add("@echo on");
            //commandsList.Add(">> Bundle_Output_Log.txt 0>&1 2>&1 ("); // container to output to log
            if (input != null)
            {
                commandsList.AddRange(input);
            }
            if (input2 != null)
            {
                commandsList.AddRange(input2);
            }
            if (input3 != null)
            {
                commandsList.AddRange(input3);
            }
            //commandsList.Add(")");//End of container
            
            //string TempCMDBatchPath = Application.dataPath + "/ENGAGE_CreatorSDK/Editor/IFX Tools/BundleTool/"+fileNameforBatch+"_Temp.bat";
            
            StreamWriter writer = new StreamWriter(pathforBatch, false);
            foreach (string cmd in commandsList)
            {

                //string cmdIN = cmd.Replace("/","\\");
                writer.WriteLine(cmd);
            }
            writer.WriteLine("TIMEOUT 1");
            writer.Close();
            return pathforBatch;
        }
        public static string RunCMD(List<string> arguments)
        {
            
            
            Process cmd = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            
            
            info.WindowStyle = systemDebug.ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            cmd.StartInfo = info;
            cmd.Start();
            using (StreamWriter sw = cmd.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    foreach (string arg in arguments)
                    {
                        
                        //string argIN = arg.Replace("/","\\");
                        sw.WriteLine(arg);
                        Debug.Log("CMD LINE input command: "+arg);
                    }
                }
                
            }
            // Synchronously read the standard output of the process.
            string output = cmd.StandardOutput.ReadToEnd();             
            
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return output;
        }
        public static bool SelectionIsDirectoryBool(UnityEngine.Object[] selectedObject) 
        {
            //If any selected object is not a dir then fail test return false
            bool dirFound=true;
            if (selectedObject != null)
            {
                foreach (UnityEngine.Object item in selectedObject)
                {
                    var path = AssetDatabase.GetAssetPath(item.GetInstanceID());
                    
                    if (path.Length > 0)
                    {
                        if (Directory.Exists(path) == false)
                        {
                            dirFound= false;                            
                        }
                        
                    }
                    else
                    {
                        Debug.Log("Could not get item path: "+ item.name);
                        return false;
                    }
                }
            }
            return dirFound;
        }

        
        public static List<UnityEngine.Object> GetSelectedObjectsAsList()
        {
            List<UnityEngine.Object> selection =new List<UnityEngine.Object>();
            foreach (var item in Selection.objects)
            {
                selection.Add(item);
            }
            return selection;
        }
        public static List<string> RoboCopyDependenciesFiles(string buildType,string projectLocationPath,List<UnityEngine.Object> Selections)
        {
            List<string> commands = new List<string>();
            commands.Add("REM -Script Transfers-");//these REM commands are just comments in the batch file
            foreach (string file in GetAllScriptsInProject())
            {
                var folder = Path.GetDirectoryName(file).Replace("\\","/");
                string folderLocalPath = folder.Replace(projectLocationPath,"");
                
                commands.Add("robocopy "+"\""+folder+"\""+" "+"\""+projectLocationPath+"/IFXBuildToolProjects/"+buildType+folderLocalPath+"\""+" /MIR");    
            }
            commands.Add("REM -Folder Dependencies-");
            
            List<string> dependencies =  GetFolderDependencies(Selections);
            
            foreach (var itemPath in dependencies)
            {
                Debug.Log("Dependencies: "+itemPath);
                
                var itemDirectory = Path.GetDirectoryName(itemPath);
                commands.Add("robocopy "+"\""+projectLocationPath+"/"+itemDirectory+"\""+" "+"\""+projectLocationPath+"/IFXBuildToolProjects/"+buildType+"/"+itemDirectory+"\""+" /MIR");   
            }
            commands.Add("REM -Selected Folders-");
            foreach (var item in Selections) //fix this could break if they change selection when building
            {
                var directoryName = Path.GetDirectoryName(AssetDatabase.GetAssetPath(item));
                commands.Add("robocopy "+"\""+projectLocationPath+"/"+directoryName +"\""+" "+ "\""+projectLocationPath+"/IFXBuildToolProjects/"+buildType+"/" +directoryName +"\""+"  *.meta /MIR");
            }            
            return commands;
        }

        public static List<string> GetFolderDependencies(List<UnityEngine.Object> listOfObjectsIn) // get dependencies for every object in folder
        {
            List<string> result = new List<string>();
            foreach (var obj in listOfObjectsIn)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);                
                AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                if (assetImporter.assetBundleName != obj.name)
                {
                    assetImporter.assetBundleName = obj.name;
                }            
                string[] assetsInCurBundle = AssetDatabase.GetAssetPathsFromAssetBundle(assetImporter.assetBundleName);
                foreach (string item in assetsInCurBundle)
                {
                    string[] dependencies = AssetDatabase.GetDependencies(item,true);
                    foreach (var item2 in dependencies)
                    {
                        result.Add(item2);                        
                    }                  
                } 
            }
            return result;
        }

        public static void ClearDependenciesCache(bool hardReset,string projectLocationPath)
        {
            if (Directory.Exists(projectLocationPath+"/IFXBuildToolProjects/"))
            {
                if (hardReset)
                {
                    Directory.Delete(projectLocationPath+"/IFXBuildToolProjects/",true);
                    EditorUtility.DisplayDialog("Cache Cleared",
                    "Cache Fully cleared", "OK");
                }
                else
                {
                    if (Directory.Exists(projectLocationPath+"/IFXBuildToolProjects/Android/Assets"))
                    {
                        Directory.Delete(projectLocationPath+"/IFXBuildToolProjects/Android/Assets",true);
                    }
                    if (Directory.Exists(projectLocationPath+"/IFXBuildToolProjects/iOS/Assets"))
                    {
                        Directory.Delete(projectLocationPath+"/IFXBuildToolProjects/iOS/Assets",true);
                    }                   
                    EditorUtility.DisplayDialog("Cache Cleared",
                    "Cache cleared", "OK");
                }                   
            }
            else
            {
                Debug.Log("Cache Directory not found");
            }
            
        }

        //quicktools related methods////////////////////////////////////////////////////////////////
        public static bool ObjectPivotCenteredCheck(GameObject sourceGO, bool useAleternateCenter=false)
        {
            bool objectIsCentered=true;
            float diffrenceTollerence=2f;
            Vector3 sceneCenter = new Vector3(0,0,0);
            Vector3 center = GetAverageCenter(sourceGO,useAleternateCenter);
            
            float diff = center.sqrMagnitude;
            if (diff>=diffrenceTollerence)
            {
                objectIsCentered =false;                
            }
            return objectIsCentered;
        }

        public static Vector3 MeshCenter(GameObject inputGO)
        {
            MeshFilter meshFilter = inputGO.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                Mesh mesh = meshFilter.sharedMesh;
                Vector3[] vertices = mesh.vertices;
                Bounds tempBounds =  new Bounds(inputGO.transform.TransformPoint(mesh.bounds.center),new Vector3(0,0,0));
                for (var i = 0; i < vertices.Length; i++)
                {   
                    tempBounds.Encapsulate(inputGO.transform.TransformPoint(vertices[i]));
                }
                Vector3 result = tempBounds.center;
                return tempBounds.center;
            }
            else
            {
                return Vector3.zero;
            }
            
        }
        static Vector3 GetAverageCenter (GameObject inputGO, bool useAleternateCenter = false)
        {
            
            //index error here
            Bounds tempBounds;
            Vector3 centerAverage;
            if (useAleternateCenter)
            {
                
                MeshFilter[] mFInChildren = inputGO.GetComponentsInChildren<MeshFilter>();
                
                if (mFInChildren.Length >0)
                {                   
                    tempBounds =  new Bounds(MeshCenter(mFInChildren[0].gameObject),Vector3.zero);
                    for (var i = 0; i < mFInChildren.Length; i++)
                    {
                        GameObject child = mFInChildren[i].gameObject;
                        Debug.Log("children: "+child.name);

                        Vector3 meshCenter=MeshCenter(child.gameObject);
                        
                        tempBounds.Encapsulate(meshCenter);
                    }
                    centerAverage = tempBounds.center;
                    centerAverage = Vector3.zero;
                }              
                else
                {
                    Debug.Log("no Meshfilter Components found use other centering method");
                    centerAverage = Vector3.zero;
                }
            }
            else
            {
                List<Vector3> boundsCenterList = new List<Vector3>();
                Renderer[] renderComponentsInChildren = inputGO.GetComponentsInChildren<Renderer>();
                
                if (renderComponentsInChildren.Length >0)
                {
                   
                    foreach (Renderer renderComponent in renderComponentsInChildren)
                    {
                        //Debug.Log("Render Components test");
                        boundsCenterList.Add(renderComponent.bounds.center);
                    }
                    //Debug.Log("Render Components it aint null bruh");
                    tempBounds =  new Bounds(boundsCenterList[0],Vector3.zero);
                    foreach (Vector3 bound in boundsCenterList)
                    {
                        tempBounds.Encapsulate(bound);
                    }
                    centerAverage = tempBounds.center;  
                }
                else
                {
                    Debug.Log("no Render Components found use other centering method");
                    centerAverage = Vector3.zero;
                }                    
            } 
            return centerAverage;
        }
        public static void CenterIFX(GameObject inputGO, bool useAleternateCenter = false)
        {
            //if it has an asset path AKA it's a prefab then load that prefab for editing otherwise just use the scene GO
            GameObject prefabRoot = inputGO;
            string assetPath="";
            assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(inputGO);
            //Debug.Log("Asset path is: "+assetPath);
            if (assetPath!="")
            {
                prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
            }
            Vector3 averageCenter = GetAverageCenter(prefabRoot,useAleternateCenter);
            //Need to load all the direct children transforms into a list before reparenting or the foreach count gets confused 
            List<Transform> childrenTransformsList = new List<Transform>();
            //create a GO that is moved to the averaged center of geo. Paaretn everything to it then move it to 0,0,0
            GameObject offsetMoverGO = new GameObject();
            offsetMoverGO.name="ReCentering Mover";
            offsetMoverGO.transform.parent = prefabRoot.transform;
            offsetMoverGO.transform.position = averageCenter;
            //Debug.Log("average center is: "+averageCenter);
            //Debug.Log("mover pos is: "+offsetMoverGO.transform.position);
            //Debug.Log(prefabRoot.transform.childCount);
            foreach (Transform child in prefabRoot.transform)
            {
                Debug.Log(child.name);
                childrenTransformsList.Add(child.transform);
            }
            foreach (Transform child in childrenTransformsList)
            {
                child.transform.SetParent(offsetMoverGO.transform);
            }
            //move the offset back to zero
            offsetMoverGO.transform.position =new Vector3(0,0,0);
            //parent all the objects back to their original parent then delete the offset mover
            foreach (Transform child in childrenTransformsList)
            {
                child.transform.SetParent(prefabRoot.transform);
            }
            DestroyImmediate(offsetMoverGO);
            //if it was used on a prefab save it over the old prefab
            if (assetPath!="")
            {
                PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            }        
        }

        public static void BatchCenterIFX(GameObject[] inputGO, bool useAleternateCenter = false)
        {
            foreach (GameObject GO in inputGO)
            {
                Debug.Log("got to batch center:");
                CenterIFX(GO,useAleternateCenter);
            }
        }

        public static void CreateDependenciesFolder(string currentIFXNum,string folderNameIn)
        {
            
            Debug.Log("Creating:" + folderNameIn);
            string createDependenciesFolder = AssetDatabase.CreateFolder("Assets", "Dependencies_" + folderNameIn);
            string createBundleFolder = AssetDatabase.CreateFolder("Assets/---IFXBundles--Windows_Android_Both", "ifx"+currentIFXNum + "-" + folderNameIn.ToLower());
        }

        public static void ImagePlaneFromImage()
        {
            UnityEngine.Object[] activeGOs = Selection.GetFiltered(typeof(Texture2D),SelectionMode.Editable | SelectionMode.TopLevel);
            float ratio1=1f;
            float ratio2=1f;
            if (activeGOs.Length >0)
            {
                foreach (Texture2D image in activeGOs)
                {
                    var itemPath = AssetDatabase.GetAssetPath(image);
                    var itemDirectory = Path.GetDirectoryName(itemPath);
                    TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath(itemPath);
                    textureImporter.npotScale = TextureImporterNPOTScale.None;
                    textureImporter.SaveAndReimport();
                    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    ratio1 = image.height;
                    ratio2 = image.width;
                    //scale to the ratios
                    plane.transform.localScale = new Vector3(ratio2/ratio1, 1, ratio1/ratio1);
                    //Set the name
                    plane.name = image.name;
                    //Remove Colliders
                    DestroyImmediate(plane.GetComponent<Collider>());
                    //Material Stuff
                    Renderer rend = plane.GetComponent<Renderer>();
                    Material material = new Material(Shader.Find("Unlit/Transparent Cutout"));
                    material.SetFloat("_Mode", 1.0f);
                    material = MaterialCutoutmode(material);
                    material.name = image.name + "_MAT";
                    material.SetTexture("_MainTex", image);
                    rend.material = material;
                    AssetDatabase.CreateAsset(material, itemDirectory + "/" + image.name + "_MAT.mat");
                    //Rotate
                    plane.transform.eulerAngles = new Vector3(90, 0, 0);
                    //Duplicate the plane
                    GameObject planeOtherSide = Instantiate(plane, new Vector3(0, 0, 0), Quaternion.identity);
                    planeOtherSide.name = plane.name + "_OtherSide";
                    planeOtherSide.transform.eulerAngles = new Vector3(90, 0, 180);
                    //Paretnt to empty
                    GameObject topEmpty = new GameObject();
                    topEmpty.name = plane.name+"_ScaleThis";
                    plane.transform.parent = topEmpty.transform;
                    planeOtherSide.transform.parent = topEmpty.transform;
                }
            }
            else
            {
                Debug.Log("No image selected");
                EditorUtility.DisplayDialog("WARNING!", "Select an image in the project view first", "OK", "Cancel");
            } 
        }

        private static Material MaterialCutoutmode(Material material)
        {
            material.SetOverrideTag("RenderType", "TransparentCutout");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.EnableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
            return material;
        }

        public static void CreateAnimClipsFromSelectedFBX()
        {
            string assetPath = AssetDatabase.GetAssetPath((GameObject)Selection.activeObject);

            ModelImporter MI = (ModelImporter)AssetImporter.GetAtPath(assetPath);
            ModelImporterClipAnimation[] clips = MI.clipAnimations;

            var itemPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var itemDirectory = Path.GetDirectoryName(itemPath);
            var findClipsAtPath = AssetDatabase.LoadAllAssetRepresentationsAtPath(itemPath);

            foreach (var aClips in findClipsAtPath)
            {
                var animationClip = aClips as AnimationClip;

                if (animationClip != null)
                {
                    var createController = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPathWithClip(itemDirectory + "/" + animationClip.name + "_ANIM_CONTROLLER.controller", animationClip);
                    Debug.Log("Found animation clip");
                }
            }
        }

        public static void InsertSelectedObjectIntoEmpty(GameObject[] selectedObjects,string prefix,string affix)
        {
            foreach (GameObject item in selectedObjects)
            {
                var topEmpty = new GameObject();
                topEmpty.name = prefix + item.name + affix;
                item.transform.parent = topEmpty.transform;
                topEmpty.transform.position = new Vector3(0, 0, 0);
                topEmpty.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        //Quality Check related methods////////////////////////////////////////////////////////////////
        public static void FixPrefabs(GameObject inputGO)
        {
            GameObject prefabRoot = inputGO;
            string assetPath="";
        
            assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefabRoot);
            Debug.Log("Asset path is: "+assetPath);
            if (assetPath!="")
            {
                prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
            }

            Camera[] cameras = prefabRoot.GetComponentsInChildren<Camera>();
            Collider[] colliders = prefabRoot.GetComponentsInChildren<Collider>();
            Light[] lights = prefabRoot.GetComponentsInChildren<Light>();
            LODGroup[] lodGroups = prefabRoot.GetComponentsInChildren<LODGroup>();

            foreach (Camera item in cameras)
            {
                DestroyImmediate(item);
            }
            foreach (Collider item in colliders)
            {
                DestroyImmediate(item);
            }
            foreach (Light item in lights)
            {
                DestroyImmediate(item);
            }
            foreach (LODGroup item in lodGroups)
            {
                DestroyImmediate(item);
            }

            PrefabZeroTransforms(prefabRoot);

            

            if (assetPath!="")
            {
                PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            }
            // center ifx opens prefab so it's better to keep it after the rest to avoid conflict then prefab is opened for above changes
            if (ObjectPivotCenteredCheck(inputGO))
            {
                CenterIFX(inputGO);
            }
        }

        public static List<string> PrefabQualityCheck(GameObject gameObjectToCheck)
        {
            
            List<string> errorsFound = new List<string>();
            
            
            if (gameObjectToCheck.transform.position!=Vector3.zero | gameObjectToCheck.transform.rotation != Quaternion.identity | gameObjectToCheck.transform.localScale != new Vector3(1,1,1))
            {
                Debug.Log("Prefab Root not zeroed: ");
                
                errorsFound.Add("Prefab Root not zeroed");
            }
            
            if(ObjectPivotCenteredCheck(gameObjectToCheck) == false)
            {
                Debug.Log("Object appears off center: ");
                
                errorsFound.Add("Object Appears OffCenter");
                
            }
            if(gameObjectToCheck.GetComponentInChildren(typeof(Camera),true) != null)
            {
                Debug.Log("Camera component found within ");
                
                errorsFound.Add("Camera component found within: ");
                
            
            }

            if(gameObjectToCheck.GetComponentInChildren(typeof(Collider),true) != null)
            {
                Debug.Log("Collider component found within ");
                
                errorsFound.Add("Collider component found within: ");
                
                
            }

            if(gameObjectToCheck.GetComponentInChildren(typeof(Light),true) != null)
            {
                Debug.Log("Light component found within ");
                
                errorsFound.Add("Light component found within: ");
                
            }

            if(gameObjectToCheck.GetComponentInChildren(typeof(LODGroup),true) != null)
            {
                Debug.Log("LODGroup component found within ");
                
                errorsFound.Add("LODGroup component found within: ");                
            }

            if(gameObjectToCheck.GetComponentInChildren(typeof(AudioSource),true) != null && gameObjectToCheck.GetComponentInChildren(typeof(AudioDefaultScale),true) == null)
            {
                Debug.Log("LODGroup component found within ");
                
                errorsFound.Add("AudioSource component requires \"AudioDefaultScale\" script: ");                
            }
                        
            return errorsFound;
            }


        public static void PrefabZeroTransforms(GameObject inputGO)
        {
            inputGO.transform.position = Vector3.zero;
            inputGO.transform.rotation = Quaternion.identity;
            inputGO.transform.localScale = new Vector3(1,1,1);
        }

    }
}
