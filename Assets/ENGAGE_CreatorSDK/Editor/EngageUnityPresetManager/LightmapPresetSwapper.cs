using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System;


[InitializeOnLoadAttribute]
public static class LightmapPresetSwapper
{

    public static Preset _lightmapPreset;

    private static Preset _defaultPresetFile;

    private static int _tryCount = 0;
    private static int TRY_LIMIT = 10;

    private static string ANDROID_TEXTURES_PRESET = "ENGAGEOverridePreset";
    private static string ANDROID_LIGHTMAP_PRESET = "ENGAGELightmapOverridePreset";

    static LightmapPresetSwapper()
    {
        EditorApplication.update += RetryInit;
        Init();
    }

    private static void Init()
    {
        _lightmapPreset = LoadPreset(ANDROID_LIGHTMAP_PRESET);
        _defaultPresetFile = LoadPreset(ANDROID_TEXTURES_PRESET);
        CheckInitSuccessfull();
    }

    /// <summary>
    /// Loads the Given Preset from the Asset folders
    /// </summary>
    /// <param name="presetName">Preset Name to be loaded</param>
    /// <returns>Loaded Preset</returns>
    private static Preset LoadPreset(string presetName)
    {
        string path = GetPresetPath(presetName);
        return AssetDatabase.LoadAssetAtPath<Preset>(path);
    }

    /// <summary>
    /// Returns the preset path for a given preset name
    /// </summary>
    /// <param name="assetName">Preset file name to load</param>
    /// <returns>Preset's path</returns>
    private static string GetPresetPath(string assetName)
    {
        string[] assets = AssetDatabase.FindAssets(assetName);
        string path = "";
        if (assets.Length > 0)
        {
            path = AssetDatabase.GUIDToAssetPath(assets[0]);
        }
        else if (_tryCount > TRY_LIMIT)
        {
            Debug.LogError("No Presets found.");
        }

        return path;
    }

    /// <summary>
    /// Checks if the presets aren't empty and then subscribes to the relevant events.
    /// </summary>
    private static void CheckInitSuccessfull()
    {
        if (_lightmapPreset != null)
        {
            Debug.Log("LightmapPresetSwapper :: Initialization Complete");

            if (_defaultPresetFile != null)
            {
                if (Preset.GetDefaultForPreset(_defaultPresetFile) == null || Preset.GetDefaultForPreset(_defaultPresetFile) == _lightmapPreset)
                {
                    Preset.SetAsDefault(_defaultPresetFile);
                }
            }
            Lightmapping.bakeStarted += OnBakeStarted;
            Lightmapping.bakeCompleted += SetPresetsToDefault;
            EditorApplication.quitting += SetPresetsToDefault;
            EditorApplication.update -= RetryInit;
        }
        else
        {
            if (_tryCount > TRY_LIMIT)
            {
                Debug.LogError("LightmapPresetSwapper :: Preset Files couldn't be found or have a different name than expected.");
                EditorApplication.update -= RetryInit;
            }
            else
            {
                _tryCount++;
            }
        }
    }

    private static void RetryInit()
    {
        Init();
    }

    /// <summary>
    /// Bake Listener, once started we switch the Presets to use the Lightmap Texture Presets.
    /// </summary>
    private static void OnBakeStarted()
    {
        if (_lightmapPreset != null)
        {

            if (_defaultPresetFile == null) _defaultPresetFile = Preset.GetDefaultForPreset(_lightmapPreset);
            Preset.SetAsDefault(_lightmapPreset);
            Debug.Log("LightmapPresetSwapper :: Bake Started set " + _lightmapPreset.name + " as the Texture Preset");
            EditorApplication.update += CheckBakeState;
        }
    }

    /// <summary>
    /// Listens to the Editor Update method, safety function that swaps back the presets in case of Force Stop or Cancel being pressed during the bake.
    /// </summary>
    private static void CheckBakeState()
    {
        if (!Lightmapping.isRunning)
        {
            EditorApplication.update -= CheckBakeState;
            SetPresetsToDefault();
        }
    }

    /// <summary>
    /// Swaps the presets back to their original values.
    /// </summary>
    private static void SetPresetsToDefault()
    {

        Debug.Log("LightmapPresetSwapper :: Bake stopped reverting asset import preset settings");
        if (_defaultPresetFile != null)
        {
            Preset.SetAsDefault(_defaultPresetFile);
            _defaultPresetFile = null;
        }

    }
}
