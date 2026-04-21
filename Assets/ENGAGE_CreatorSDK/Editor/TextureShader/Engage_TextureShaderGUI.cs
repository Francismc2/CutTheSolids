using UnityEditor;
using UnityEngine;

public class Engage_TextureShaderGUI : ShaderGUI
{
    private static string[] stereoModeLabels = new string[] { "None", "Top Bottom", "Left Right", "Custom UV" };

    public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
    {
        var stereoMode = FindProperty("_StereoMode", properties);
        var splitImages = FindProperty("_SplitImages", properties);
        var chroma = FindProperty("_Chroma", properties);

        var label = new GUIContent("Stereo Mode");
        stereoMode.floatValue = EditorGUILayout.Popup(label, (int)stereoMode.floatValue, stereoModeLabels);

        label = new GUIContent("Split Images");
        splitImages.floatValue = EditorGUILayout.Toggle(label, splitImages.floatValue == 1) ? 1 : 0;

        label = new GUIContent("Chroma");
        chroma.floatValue = EditorGUILayout.Toggle(label, chroma.floatValue == 1) ? 1 : 0;

        GUILayout.Space(10);
        GUILayout.Label("Maps", EditorStyles.boldLabel);

        if (splitImages.floatValue == 1f)
        {
            var mainTex = FindProperty("_MainTex", properties);
            label = new GUIContent("Left Texture");
            editor.TexturePropertySingleLine(label, mainTex);

            var rightTex = FindProperty("_RightTex", properties);
            label = new GUIContent("Right Texture");
            editor.TexturePropertySingleLine(label, rightTex);
        }
        else
        {
            var mainTex = FindProperty("_MainTex", properties);
            label = new GUIContent("Main Texture");
            editor.TexturePropertySingleLine(label, mainTex);
        }

        if (splitImages.floatValue == 1f && stereoMode.floatValue == 3f) // Custom Stereo UVs
        {
            GUILayout.Space(10);
            GUILayout.Label("Custom UVs", EditorStyles.boldLabel);

            var leftUVs = FindProperty("_LeftEye", properties);
            editor.VectorProperty(leftUVs, "Left eye UVs");

            var rightUVs = FindProperty("_RightEye", properties);
            editor.VectorProperty(rightUVs, "Right eye UVs");
        }

        if (chroma.floatValue == 1f)
        {
            GUILayout.Space(10);
            GUILayout.Label("Chroma", EditorStyles.boldLabel);

            var chromaColor = FindProperty("_ChromaKeyColor", properties);
            editor.ColorProperty(chromaColor, "Chroma Key Color");

            var chromaThreshold = FindProperty("_ChromaThreshold", properties);
            editor.FloatProperty(chromaThreshold, "Chroma Threshold");

            var chromaTolerance = FindProperty("_ChromaTolerance", properties);
            editor.FloatProperty(chromaTolerance, "Chroma Tolerance");
        }
    }
}