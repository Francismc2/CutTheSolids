using UnityEngine;
using UnityEditor;
using System.Net;
using System.IO;
using System;
using System.Xml;
using System.Security.Cryptography;

namespace AssetBundles
{
    [InitializeOnLoad]
    public class UpdateManager : EditorWindow
    {
        static bool isBatchMode;
        static bool updateInProgress = false;
        static bool automaticUpdatesEnabled = false;
        static bool packageUpToDate = true;
        static bool checkOnly = false;
        static bool autoupdate = true;
        static bool oldAutoUpdate;
        static bool initialPackageChecked = false;

        float defaultLabelWidth;
        readonly float guiLabelWidth = 160f;
        static string packageStatus = "";
        static readonly string _filepath = "CreatorSDK.unitypackage";
        static readonly string _localManifestPath = "manifest.xml";
        static readonly string _xpathConfig = "packageData/autoupdate";
        static readonly string _xpathVersion = "packageData/checksum";
        static readonly string _packageUrl = "https://github.com/immersivevreducation/Engage_CreatorSDK/blob/master/CreatorSDK.unitypackage?raw=true";
        static readonly string _manifestURL = "https://github.com/immersivevreducation/Engage_CreatorSDK/blob/master/manifest.xml?raw=true";

        [MenuItem("Creator SDK/Check for updates")]
        public static void ShowUpdateWindow()
        {
            //packageStatus = "Checking for update";
            //checkOnly = true;
            //ImportPackage();
            GetWindow<UpdateManager>(false, "Update manager", true);
        }

        [MenuItem("Creator SDK/View Documentation")]
        public static void ShowDocumentation()
        {
            Application.OpenURL("https://docs.engagevr.io/developer");
        }

        static UpdateManager()
        {
            EditorApplication.update += Initialize;
        }

        static void Initialize()
        {
            EditorApplication.update -= Initialize;
            if (!initialPackageChecked && !Application.isBatchMode)
            {
                if (!PlayerPrefs.HasKey("SDKAutoUpdate"))
                {
                    autoupdate = true;
                    PlayerPrefs.SetString("SDKAutoUpdate", "True");
                }
                else
                {
                    if (PlayerPrefs.GetString("SDKAutoUpdate") == "False")
                    {
                        autoupdate = false;
                    }
                }

                oldAutoUpdate = autoupdate;

                if (autoupdate)
                {
                    packageStatus = "Checking for update";
                    checkOnly = true;
                    ImportPackage();
                }
            }
        }

        
        private void OnGUI()
        {
            GUILayout.Label("ENGAGE Creator SDK");
            EditorGUILayout.Space();

            autoupdate = GUILayout.Toggle(autoupdate, "Check for SDK Updates automatically");

            if(autoupdate != oldAutoUpdate)
            {
                PlayerPrefs.SetString("SDKAutoUpdate", autoupdate.ToString());
                Debug.Log("Set auto-update to " + autoupdate);
                oldAutoUpdate = autoupdate;
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Check for Update") && !updateInProgress)
            {
                CheckXMLExists();
                packageStatus = "Checking for update";
                checkOnly = true;
                ImportPackage();
            }

            EditorGUILayout.Space();

            if (!packageUpToDate)
            {
                if (GUILayout.Button("Update to latest version") && !updateInProgress)
                {
                    packageStatus = "Updating to latest version";
                    checkOnly = false;
                    ImportPackage();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            GUILayout.Label(packageStatus);
        }

        static void CheckXMLExists()
        {
            if (!File.Exists(_localManifestPath))
            {
                string xmlFile = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n<packageData>\n  <checksum>af0200e7837cc</checksum>\n  <autoupdate>False</autoupdate>\n</packageData>";
                File.WriteAllText(_localManifestPath, xmlFile);
            }
        }

        private static void ImportPackage()
        {
            WebClient wc = new WebClient();
            Uri _uri = new Uri(_packageUrl);
            wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            try
            {
                updateInProgress = true;
                wc.DownloadFileAsync(_uri, "CreatorSDK");
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        private static bool PackageIsUpToDate(string _path)
        {
            if (File.Exists(_localManifestPath))
            {
                return GetMD5Checksum(_path) == GetValueFromXML(File.ReadAllText(_localManifestPath), _xpathVersion);
            }
            else
            {
                return false;
            }
        }

        private static void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            updateInProgress = false;
            try
            {
                if (File.Exists(_filepath))
                    FileUtil.DeleteFileOrDirectory(_filepath);
                FileUtil.MoveFileOrDirectory("CreatorSDK", _filepath);

                if (File.Exists(_filepath))
                {
                    if (PackageIsUpToDate(_filepath))
                    {
                        packageStatus = "Creator SDK Package is up to date";
                        packageUpToDate = true;
                        initialPackageChecked = true;
                        return;
                    }
                    else
                    {
                        if (!checkOnly)
                        {
                            AssetDatabase.ImportPackage(_filepath, false);
                            WriteDataToXML(File.ReadAllText(_localManifestPath), "packageData/checksum", GetMD5Checksum(_filepath));
                            packageStatus = "Creator SDK Package is up to date";
                            packageUpToDate = true;
                            initialPackageChecked = true;
                        }
                        else
                        {
                            packageUpToDate = false;
                            packageStatus = "New Creator SDK Update Available!\n\nPlease click \"Update to Latest Version\" to stay up-to-date";
                            ShowUpdateWindow();
                            initialPackageChecked = true;
                        }
                    }
                }
                else
                {
                    packageStatus = "Error Updating to latest version";
                    throw new FileNotFoundException();
                }
            }
            catch
            {
                packageStatus = "Error Updating to latest version";
                throw e.Error;
            }
        }

        private static string GetValueFromXML(string _xml, string _xpath)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(_xml);
            string xpath = _xpath;
            var node = xDoc.SelectSingleNode(xpath);

            return node.InnerXml;
        }

        private static void WriteDataToXML(string _xml, string _xpath, string _value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(_xml);
            string xpath = _xpath;
            var node = xDoc.SelectSingleNode(xpath);
            node.InnerXml = _value;
            xDoc.Save(_localManifestPath);
        }

        private static void WriteDataToXML(string _xml, string _xpath, int _value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(_xml);
            string xpath = _xpath;
            var node = xDoc.SelectSingleNode(xpath);
            node.InnerXml = _value.ToString();
            xDoc.Save(_localManifestPath);
        }

        private static void WriteDataToXML(string _xml, string _xpath, bool _value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(_xml);
            string xpath = _xpath;
            var node = xDoc.SelectSingleNode(xpath);
            node.InnerXml = _value.ToString();
            xDoc.Save(_localManifestPath);
        }

        private static string GetMD5Checksum(string _path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(_path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}