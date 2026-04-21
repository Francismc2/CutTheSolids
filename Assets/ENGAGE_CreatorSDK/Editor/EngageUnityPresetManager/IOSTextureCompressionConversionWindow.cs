// ===============================
// AUTHOR: Dean Ryan
//================================

using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class IOSTextureCompressionConversionWindow : EditorWindow
{
    #region Fields

    private static IOSTextureCompressionConversionWindow _window;

    #endregion

    #region Methods - Editor Window

    [MenuItem("Creator SDK/Project Conversion Tools/Add iOS Texture Compression to Android Project")]
    public static void Init()
    {
        WindowSetUp();
    }

    private static bool FormatChecker(TextureImporterFormat currentTextureImporterFormat, string fileToValidate)
    {
        var formatValidation = (from object enums in Enum.GetValues(typeof(TextureImporterFormat))
                                where enums.ToString().Contains("HDR")
                                select (TextureImporterFormat)enums).ToList().
             Any(format => format == currentTextureImporterFormat);

        if (formatValidation)
        {
            Debug.LogWarning("Asset " + fileToValidate + " cannot have HDR format , issue has been resolved");
        }

        return formatValidation;
    }

    private static void SetTextureData(string textureFile, TextureImporterPlatformSettings androidPlatformTextureSetting)
    {
        //iOS texture conversion
        string iOSUnityAssetPath = GetRelativeAssetPath(textureFile);
        TextureImporter iOSTextureImporter = (TextureImporter)AssetImporter.GetAtPath(iOSUnityAssetPath);
        iOSTextureImporter.isReadable = true;

        Debug.Log("Asset Path:" + iOSUnityAssetPath);

        var platformTextureSetting = iOSTextureImporter.GetPlatformTextureSettings("iPhone");
        platformTextureSetting.overridden = true;

        var currentTextureImporterFormat = FormatChecker(androidPlatformTextureSetting.format, iOSUnityAssetPath) ? TextureImporterFormat.ASTC_8x8 : androidPlatformTextureSetting.format;

        platformTextureSetting.format = currentTextureImporterFormat;
        platformTextureSetting.maxTextureSize = androidPlatformTextureSetting.maxTextureSize;
        platformTextureSetting.resizeAlgorithm = androidPlatformTextureSetting.resizeAlgorithm;
        platformTextureSetting.textureCompression = androidPlatformTextureSetting.textureCompression;
        platformTextureSetting.compressionQuality = androidPlatformTextureSetting.compressionQuality;

        try
        {
            iOSTextureImporter.SetPlatformTextureSettings(platformTextureSetting);
            iOSTextureImporter.SaveAndReimport();
        }
        catch (Exception e)
        {
            Debug.Log("Couldn't set texture data " + androidPlatformTextureSetting.format + " :  " + iOSUnityAssetPath + "  :  " + e);
            Debug.Log("Asset Path:" + iOSUnityAssetPath);
            Debug.Log("Override:" + androidPlatformTextureSetting.overridden);
            Debug.Log("Resize algorithm:" + androidPlatformTextureSetting.resizeAlgorithm);
            Debug.Log("Format:" + androidPlatformTextureSetting.format);
            Debug.Log("Compress quality:" + androidPlatformTextureSetting.compressionQuality);
        }
    }

    private static void GetTextureData()
    {
        Debug.Log("<color=green>Each Color is a different asset</color>");
        var rootFiles = Directory.EnumerateFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
            .Where(s => s.ToLower().EndsWith(".png") || s.ToLower().EndsWith(".jpg") || s.ToLower().EndsWith(".tga") || s.ToLower().EndsWith(".psd") || s.ToLower().EndsWith(".jpeg") || s.ToLower().EndsWith(".tiff") || s.ToLower().EndsWith(".bmp") || s.ToLower().EndsWith(".tif") || s.ToLower().EndsWith(".exr")); //PSD, TIFF, JPG, TGA, PNG, GIF, BMP, IFF, PICT

        foreach (string textureFile in rootFiles)
        {
            var androidUnityAssetPath = GetRelativeAssetPath(textureFile);
            TextureImporter androidTextureImporter = (TextureImporter)AssetImporter.GetAtPath(androidUnityAssetPath);

            var r = Random.Range(0, 255);
            var g = Random.Range(0, 255);
            var b = Random.Range(0, 255);
            Color color = new Color(r / 255f, g / 255f, b / 255f);
            var hexvalue = ColorUtility.ToHtmlStringRGBA(color);
            try
            {
                TextureImporterPlatformSettings androidPlatformTextureSetting = androidTextureImporter.GetPlatformTextureSettings("Android");

                /*Debug.Log("<color=#"+hexvalue+">Asset Path:"+androidUnityAssetPath+"</color>");
                Debug.Log("<color=#"+hexvalue+">Override:"+androidPlatformTextureSetting.overridden+"</color>");
                Debug.Log("<color=#"+hexvalue+">Resize algorithm:"+androidPlatformTextureSetting.resizeAlgorithm+"</color>");
                Debug.Log("<color=#"+hexvalue+">Format:"+androidPlatformTextureSetting.format+"</color>");
                Debug.Log("<color=#"+hexvalue+">Compress quality:"+androidPlatformTextureSetting.compressionQuality+"</color>");*/

                SetTextureData(textureFile, androidPlatformTextureSetting);
            }
            catch (Exception _e)
            {
                Debug.Log("Failed on texture " + textureFile + " : " + _e);
            }
        }
        //AssetDatabase.ForceReserializeAssets();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void WindowSetUp()
    {
        Type inspectorType = Type.GetType("UnityEditor.InspectorWindow,UnityEditor.dll");
        _window = GetWindow<IOSTextureCompressionConversionWindow>("IOS Texture Compression Settings", new Type[] { inspectorType });
    }

    #endregion

    #region Methods - OnGUI

    static string GetRelativeAssetPath(string absolutePath)
    {
        if (absolutePath.StartsWith(Application.dataPath))
        {
            return "Assets" + absolutePath.Substring(Application.dataPath.Length);
        }

        return string.Empty;
    }

    /// <summary>
    /// Draws all the ui elements for the window
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Space(10);
        if (GUILayout.Button("Set iOS Texture Compression Settings to match Android"))
        {
            GetTextureData();
        }
    }

    #endregion

    #region Methods - Implementation

    #endregion
}