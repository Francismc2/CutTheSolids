using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShaderSwap : EditorWindow
{
    public struct genericInfo
    {
        public int count;
        public string guid;
        public string path;
    }

    public struct matInfo
    {
        public int count;
        public string guid;
        public string path;
        public string shader;
    }

    public string shaderToSwapFrom = "";
    public string shaderToSwapTo = "";

    public string result = "Specify full (internal) shader names for swap operation below:";
    public string feedback = "Use with caution :)";

    public Dictionary<int, matInfo> allMats = new Dictionary<int, matInfo>();
    public Dictionary<int, genericInfo> externalMats = new Dictionary<int, genericInfo>();
    public Dictionary<int, genericInfo> embeddedMats = new Dictionary<int, genericInfo>();

    public Dictionary<int, genericInfo> allShaders = new Dictionary<int, genericInfo>();


    [MenuItem("RobotronInc/ShaderSwap")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ShaderSwap window = (ShaderSwap)EditorWindow.GetWindow(typeof(ShaderSwap));
        window.Show();
    }

    private void ListMat(bool verbose)
    {
        allMats.Clear();
        externalMats.Clear();
        embeddedMats.Clear();

        string[] matList = AssetDatabase.FindAssets("t: Material");
        int count = 0;
        foreach (string guid in matList)
        {
            genericInfo entry = new genericInfo();
            matInfo matentry = new matInfo();
            entry.count = count;
            entry.path = AssetDatabase.GUIDToAssetPath(guid);
            matentry.count = count;
            matentry.path = entry.path;

            if (entry.path.EndsWith(".mat"))  //external material
            {
                externalMats.Add(count, entry);
                if (verbose) Debug.Log(count.ToString() + "[external] : " + entry.path);
            }
            else    //embedded material
            {
                embeddedMats.Add(count, entry);
                if (verbose) Debug.Log(count.ToString() + "[embedded] : " + entry.path);
            }

            Material mat=AssetDatabase.LoadAssetAtPath<Material>(entry.path) ;
            if (mat!=null)
            {
                if (verbose) Debug.Log(mat.name+" found, using "+mat.shader.name+" shader.");
                
            }
            else
            {
                if (verbose) Debug.Log("No Material there");
            }


            allMats.Add(count, matentry);

            count++;
        }

        if (verbose) Debug.Log("");
        if (verbose) Debug.Log("Materials total: " + allMats.Count + " , external: " + externalMats.Count + " , embedded: " + embeddedMats.Count);
    }

    private void ListShaders(bool verbose)
    {
        allShaders.Clear();

        string[] shaderList = AssetDatabase.FindAssets("t: Shader");
        int count = 0;
        foreach (string guid in shaderList)
        {
            genericInfo entry = new genericInfo();
            entry.count = count;
            entry.path = AssetDatabase.GUIDToAssetPath(guid);
            allShaders.Add(count, entry);

            if (verbose) Debug.Log(count.ToString() + " : " + AssetDatabase.GUIDToAssetPath(guid));
            count++;
        }

        if (verbose) Debug.Log("");
        if (verbose) Debug.Log("Shaders total: " + allShaders.Count);

    }

    private void SwapShaders()
    {

        bool abort = false;

        Dictionary<int, matInfo> swapMat = new Dictionary<int, matInfo>();

        Shader toShader = Shader.Find(shaderToSwapTo);
        Shader fromShader = Shader.Find(shaderToSwapFrom);

        abort = (abort||(toShader == null)||(fromShader == null));

        if (!abort)
        {
            string[] matList = AssetDatabase.FindAssets("t: Material");
            int checkcount = 0;
            int swapcount = 0;
            int skipcount = 0;
            foreach (string guid in matList)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                Material mater = AssetDatabase.LoadAssetAtPath<Material>(path);

                if (mater.shader.name==shaderToSwapFrom)
                {
                    if (path.EndsWith(".mat"))  //external material
                    {
                        mater.shader = toShader;
                        Debug.Log("Swapping for material: " + mater.name);
                        swapcount++;
                    }
                    else    //embedded material
                    {
                        Debug.Log("Skipping embedded material : " + mater.name + " using [from] shader.");
                        skipcount++;
                    }
                }
                checkcount++;
            }
            feedback = "Swapped " + swapcount.ToString() + " times, skipped " + skipcount.ToString() + " times. [" +checkcount.ToString()+" materials total]";
        }
        else
        {
            feedback = "Invalid input. Try again.";
        }

        Material mat = AssetDatabase.LoadAssetAtPath<Material>("dfg");
        if (mat != null)
        {
            Debug.Log(mat.name + " found, using " + mat.shader.name + " shader.");

            if (mat.name == "Dummy_Standard")
            {
                mat.shader = Shader.Find("Engage_SpeedTree");
            }
        }
    }

    void OnGUI()
    {

        if (GUI.Button(new Rect(3, 25, position.width - 6, 20), "List Materials"))
            ListMat(true);

        if (GUI.Button(new Rect(3, 50, position.width - 6, 20), "List Shaders"))
            ListShaders(true);

        GUI.Label(new Rect(3, 90, position.width - 6, 20), result, "TextField");

        if (GUI.Button(new Rect(3, 115, position.width - 6, 20), "Swap Shaders"))
            SwapShaders();

        GUI.Label(new Rect(3, 140, 40, 20), "from:", "TextField");
        GUI.Label(new Rect(3, 165, 40, 20), "  to:", "TextField");

        shaderToSwapFrom = GUI.TextField(new Rect(50, 140, position.width - 56, 20), shaderToSwapFrom, 100);
        shaderToSwapTo = GUI.TextField(new Rect(50, 165, position.width - 56, 20), shaderToSwapTo, 100);

        GUI.Label(new Rect(3, 195, position.width - 6, 50), feedback , "TextField");

    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}





