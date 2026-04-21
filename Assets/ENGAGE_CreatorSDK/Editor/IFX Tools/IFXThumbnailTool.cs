
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace IFXTools{
    public class IFXThumbnailToolThumbnailPreviewWindow : EditorWindow
    {
        
        IFXThumbnailTool _thumbnailToolInstance;
        
        public IFXThumbnailToolThumbnailPreviewWindow(IFXThumbnailTool thumbnailToolInstance)
        {
            _thumbnailToolInstance = thumbnailToolInstance;
            this.minSize = new Vector2(750,500);
        }
        void OnGUI()
        {
            EditorGUILayout.LabelField("IFX Thumbnail Creation Tool");
            //the thumbnail preview
            if (_thumbnailToolInstance.ifxObject!=null)
            {
                GUILayout.Label(_thumbnailToolInstance.previewImage, GUILayout.Width(this.position.width), GUILayout.Height(this.position.height));
                var thumbnailPreviewRect = GUILayoutUtility.GetLastRect();
                //_thumbnailToolInstance.UpdatePreviewImage();
                this.Repaint();
            }                        
        }
        void OnDestroy()
        {
            DestroyImmediate(_thumbnailToolInstance.ifxObject, true);
        }
    }
    public class IFXThumbnailToolWindow : EditorWindow
    {   
       
        IFXThumbnailTool thumbnailToolInstance;
        
        IFXThumbnailToolThumbnailPreviewWindow thumbnailPreview;
        public string saveLocation;
        void OnEnable()
        {
            thumbnailToolInstance = new IFXThumbnailTool();
            thumbnailToolInstance.ThumbnailSetup(thumbnailToolInstance.ifxObject);
            this.minSize = new Vector2(300,450);
            
        }
        void OnDestroy()
        {
            thumbnailPreview.Close();
            //DestroyImmediate(thumbnailPreview, true);
        }
        void OnGUI()
        {
            ThumbnailToolWindowUI();
            if (thumbnailToolInstance!=null)
            {
                thumbnailToolInstance.ThumbnailToolControlsUI(); 
            }
                       
        }
        

        private void ThumbnailToolWindowUI()
        {
            if (GUILayout.Button("Load Thumbnail Scene"))
                {
                    if (EditorUtility.DisplayDialog("WARNING!", "Unsaved work in  the current scene will be lost", "Load IFX Thumbnail Scene", "Cancel"))
                    {
                        EditorSceneManager.OpenScene("Assets/ENGAGE_CreatorSDK/Editor/IFX Tools/ThumbnailToolAssets/IFX_Thumbnail_Scene.unity");
                    }
                
                }
            
            if (GUILayout.Button("Load Object for camera"))
            {
                 // Create a temporary reference to the current scene.
                    Scene currentScene = SceneManager.GetActiveScene ();
            
                    // Retrieve the name of this scene.
                    string sceneName = currentScene.name;
                    if (sceneName != "IFX_Thumbnail_Scene")
                    {
                        if (EditorUtility.DisplayDialog("WARNING!", "You must be in the IFX Thumbnail Scene for this tool to function", "Load IFX Thumbnail Scene", "Cancel"))
                        {
                            if (EditorUtility.DisplayDialog("WARNING!", "Unsaved work in  the current scene will be lost", "Ok", "Cancel"))
                            {
                                EditorSceneManager.OpenScene("Assets/ENGAGE_CreatorSDK/Editor/IFX Tools/ThumbnailToolAssets/IFX_Thumbnail_Scene.unity");
                            }
                        }
                        
                    }

                if (thumbnailPreview == null)
                {
                    thumbnailPreview = new IFXThumbnailToolThumbnailPreviewWindow(thumbnailToolInstance);
                    //thumbnailPreview = GetWindow<IFXThumbnailToolThumbnailPreviewWindow>();
                    thumbnailPreview.position.Set(0,0,thumbnailToolInstance.imageResolutionWidth,thumbnailToolInstance.imageResolutionHeight);
                
                    //editorWindow.End();
                    thumbnailPreview.Show();

                }           
                


                if (thumbnailToolInstance.ifxObject != Selection.activeGameObject)
                {
                    DestroyImmediate(thumbnailToolInstance.ifxObject, true);
                }
                if (Selection.activeObject is GameObject)
                {                    
                    GameObject obj = Selection.activeObject as GameObject;
                    thumbnailToolInstance.ifxObject = (GameObject)Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
                    thumbnailToolInstance.ThumbnailSetup(thumbnailToolInstance.ifxObject);                    
                }
                else
                {
                    Debug.Log("Select a GameObject object first");
                }
            }         
            if (GUILayout.Button("Reset Camera"))
            {
                thumbnailToolInstance.ResetCameraSettings();
            }
            // if (GUILayout.Button("Auto Camera - This is a WIP"))
            // {
            //     thumbnailToolInstance.AutoCamera(thumbnailToolInstance.ifxObject, thumbnailToolInstance.cameraObject);
            // }
            EditorGUILayout.LabelField(" ");
            if (GUILayout.Button("Save Thumbnail"))
            {
                if (thumbnailToolInstance.ifxObject == null)
                {
                    EditorUtility.DisplayDialog("WARNING!", "Please load an object, before trying to save","Ok");
                    return;
                }
                
                if(!string.IsNullOrEmpty(PlayerPrefs.GetString("IFXThumbnailLocation")))
                {
                    saveLocation = ValidatePathAndSaveThumbnail(PlayerPrefs.GetString("IFXThumbnailLocation"));
                }
                saveLocation = ValidatePathAndSaveThumbnail(saveLocation);
                PlayerPrefs.SetString("IFXThumbnailLocation", saveLocation);
            }
            if (GUILayout.Button("Change Save Location"))
            {
                saveLocation = EditorUtility.OpenFolderPanel("Select thumbnail save location", "", "");
                PlayerPrefs.SetString("IFXThumbnailLocation", saveLocation);
            }
            //if the camera still exists, Update the preview
            if (thumbnailToolInstance.cameraObject)
            {
                //thumbnailToolInstance.ThumbnailToolControlsUI();
                thumbnailToolInstance.UpdatePreviewImage();
            }            
        }
        string ValidatePathAndSaveThumbnail(string saveLocationPath )
        {
            if (!string.IsNullOrEmpty(saveLocationPath) && Directory.Exists(saveLocationPath))
            {
                thumbnailToolInstance.SaveThumbnail(saveLocationPath);
                return saveLocationPath;
            }
            else
            {
                if (EditorUtility.DisplayDialog("WARNING!", "Thumbnail Save location Not Set, please pick a save directory", "Browse", "Cancel"))
                {
                    saveLocationPath = EditorUtility.OpenFolderPanel("Select thumbnail save location", "", "");
                    if (!string.IsNullOrEmpty(saveLocationPath) && Directory.Exists(saveLocationPath))
                    {
                        thumbnailToolInstance.SaveThumbnail(saveLocationPath);
                        return saveLocationPath;
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("WARNING!", "This is not a valid save directory", "Ok", ""))
                        {
                            return saveLocationPath;
                        }
                    }
                }
                return saveLocationPath;
            }
        }
    }
    


    public class IFXThumbnailTool
    {
        public float lightBrightness { get; set; } = 1.5f;
        public float lightRotation  { get; set; } = 0;
        public float cameraZoom { get; set; } = 50;

        public GameObject ifxObject { get; set; }
        public float objRotationX { get; set; } = 0;
        public float objRotationY { get; set; } = 0;

        public float objPosY { get; set; } = 0;
        public float objPosX { get; set; } = 0;
        public float objScale { get; set; } = 1;
        public Texture2D thumbnailImage { get; set; }
        public int imageResolutionWidth =750;
        public int imageResolutionHeight = 500;
        public int imageResolutionbitDepth  = 8;
        public Texture2D previewImage {get; set;}
        public Light[] lights { get; set; }

        public Camera camera {get; set;}
        public Transform lightMover {get; set;}
        public GameObject ifxThumbnailRig {get; set;}
        public Camera cameraObject {get; set;}
        public Rect thumbnailPreviewRect {get; set;}
        ///////////////////////Internal/////////////////////////
        RenderTexture activeRenderTexture = RenderTexture.active;       
        
        
        public void ThumbnailToolControlsUI()
        {
            if (this.ifxObject==null)
            {
                return;
            }

            // Constrain all drawing to be within a 800x600 pixel area centered on the screen.
             // Starts an area to draw elements
             
                //GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
                GUI.BeginGroup(new Rect(50, 10, 50, 50));
                    if (GUI.Button(new Rect(50, 10, 50, 50),"^up^"))
                    {
                        Debug.Log("Clicked the button with an image");
                    }
                    if (GUI.Button(new Rect(50, 70, 50, 50),"Down"))
                    {
                        Debug.Log("Clicked the button with an image");
                    }
                GUI.EndGroup();

            
            
            //Lights Comtrols
            GUILayout.Label("Brightness");
            lightBrightness = EditorGUILayout.Slider(lightBrightness, 0.1f, 10);
            LightsBrightness(lightBrightness);
            GUILayout.Label("Rotate Lights");
            lightRotation = EditorGUILayout.Slider(lightRotation, 180, -180);
            RotateLights(new Vector3(0, lightRotation, 0));

            //Camera_Controls
            GUILayout.Label("Camera Zoom");
            cameraZoom = EditorGUILayout.Slider(cameraZoom, 100, 1);
            camera.fieldOfView = cameraZoom;

            //Object Thumbnail Controls
            //Scale
            GUILayout.Label("IFX Scale");
            objScale = EditorGUILayout.Slider(objScale, 0.01f, 2);
            ScaleIFX(new Vector3(objScale, objScale, objScale));

            //Rotate
            GUILayout.Label("IFX Rotation - Up Down");
            objRotationX = EditorGUILayout.Slider(objRotationX, 180, -180);
            GUILayout.Label("IFX Rotation - Left Right");
            objRotationY = EditorGUILayout.Slider(objRotationY, 180, -180);
            RotateIFX(new Vector3(objRotationX, objRotationY, 0));

            //move
            GUILayout.Label("IFX Move - Up Down");
            objPosY = EditorGUILayout.Slider(objPosY, 10, -10);
            GUILayout.Label("IFX Move - Left Right");
            objPosX = EditorGUILayout.Slider(objPosX, -10, 10);
            MoveIFX(new Vector3(objPosX, objPosY, 0));
            
        }
        public void ResetCameraSettings()
        {
            
            if (cameraObject!=null)
            {
                cameraObject.transform.position = new Vector3(0,0,-5);
                cameraObject.transform.localEulerAngles =new Vector3(0,0,0);
            }
            cameraZoom = 50;
            objScale = 1;
            objRotationX = 0;
            objRotationY = 0;

            objPosY = 0;
            objPosX = 0;
        }

        public void ThumbnailSetup(GameObject ifxObjectIN)
        {
            ifxObject = ifxObjectIN;
            try
            {
                ifxThumbnailRig = GameObject.Find("IFX_Thumbnail_Rig");
                cameraObject = GameObject.Find("IFX_Thumbnail_Camera").GetComponent(typeof(Camera)) as Camera;
                lightMover = ifxThumbnailRig.transform.Find("Light_Mover") ;
            }
            catch (FileNotFoundException e)
            {
                Debug.Log($"IFX_Thumbnail_Rig part was not found: '{e}'");
            }
            

            lights = lightMover.GetComponentsInChildren<Light>();

            camera = cameraObject.GetComponent(typeof(Camera)) as Camera;
            RenderTexture rt = new RenderTexture(imageResolutionWidth,imageResolutionHeight,imageResolutionbitDepth, RenderTextureFormat.ARGB32);
            camera.targetTexture = rt;
            RenderTexture activeRenderTexture = RenderTexture.active;          
            previewImage = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            thumbnailImage = previewImage;
        }
        public void UpdatePreviewImage()
        {
            RenderTexture.active = camera.targetTexture;
            camera.Render();
            previewImage.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            previewImage.Apply();
            RenderTexture.active = activeRenderTexture;
            
        }
        public void SaveThumbnail(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                //Save Image to file
                byte[] bytes = thumbnailImage.EncodeToPNG();
                File.WriteAllBytes(path +"/"+ ifxObject.name.Replace("(Clone)","") + ".png", bytes);
            }
            else
            {
                 EditorUtility.DisplayDialog("No Folder Found at: "+path,
                 "Please Choose a thumbnail save location in the settings menu of this tool", "OK");
            }

            
            
        }

        public void AutoCamera(GameObject obj, Camera camera )//obj is the asset, and transform in this case was the camera itself. not working 100%
            { //this needs more work and dosn't work currently
        //     Camera camera = transform.GetComponent(typeof(Camera)) as Camera;
        //     //auto adjust camera
        //     Mesh mesh = obj.GetComponentInChildren<MeshFilter>().sharedMesh;
        //     Bounds bounds = mesh.bounds;
        //     float adjac = Vector3.Distance(transform.TransformPoint(bounds.center), transform.TransformPoint(bounds.extents));
        //     float theta = 90 - (camera.fieldOfView/2);
        //     float hypot = adjac/Mathf.Cos(theta);
        //     if(hypot < 0)
        //         hypot *= -1;
        //         Debug.Log(hypot);
        //     float distance = hypot*1.2f;
        //     transform.position = transform.rotation * new Vector3(0, 0, -distance) + obj.transform.position;



        //////////////////////////////////////////////////////////////////////////////////////////////////////Seporate idea
                // MeshFilter[] mFInChildren = obj.GetComponentsInChildren<MeshFilter>();
                // Bounds tempBounds;
                // Vector3 combinedScale;
                
                // if (mFInChildren.Length >0)
                // {                   
                //     tempBounds =  new Bounds(IFXTools.IFXToolsStaticMethods.MeshCenter(mFInChildren[0].gameObject),mFInChildren[0].sharedMesh.bounds.size);
                //     for (var i = 0; i < mFInChildren.Length; i++)
                //     {
                //         GameObject child = mFInChildren[i].gameObject;
                //         Debug.Log("children: "+child.name);

                //         Vector3 meshCenter=IFXTools.IFXToolsStaticMethods.MeshCenter(child.gameObject);
                        
                //         tempBounds.Encapsulate(meshCenter);
                //     }
                //     combinedScale = tempBounds.size;
                //     Debug.Log("combined cale: "+combinedScale);
                //     float distance = Mathf.Max(combinedScale.x, combinedScale.y, combinedScale.z);
                //     //distance /= (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad));
                //     // Move camera in -z-direction; change '2.0f' to your needs
                //     this.objScale = distance;
                // }              
                
                

        }
    
        public void RotateLights(Vector3 rotateIN)
        {
            if (lightMover == null)
            {
                Debug.Log("ThumbnailTool: LightMover not Found");
                return;
            }
            lightMover.localRotation = Quaternion.Euler(rotateIN);
        }
        public void LightsBrightness(float BrightnessIN)
        {
            if (lights == null)
            {
                Debug.Log("ThumbnailTool: Lights not Found");
                return;
            }
            foreach (Light light in lights)
            {
                light.intensity = BrightnessIN;
            }
        }
        public void MoveIFX(Vector3 positionIN)
        {
            ifxObject.transform.localPosition = positionIN;
        }
        public void RotateIFX(Vector3 rotateIN)
        {
            ifxObject.transform.localRotation = Quaternion.Euler(rotateIN);
        }
        public void ScaleIFX(Vector3 scaleIN)
            {
                ifxObject.transform.localScale = scaleIN;
            }
            
            
            
    }
}
        
    