using System.IO;
using UnityEngine;
using UnityEditor;

namespace Engage.BuildTools
{
    public class BuildSettingsView : View<BuildSettingsViewModel>
    {
        private const int VERTICAL_SPACING = 12;
        private static bool buildTargetPanelOpen = true;
        private string buildTargetDisplay;
        private Vector2 scrollPos;

        private class Labels : BuildTools.Labels
        {
            public const string MenuTitle = "Build Preferences";
            public const string BuildSettingsTitle = "Default Build Settings";
        }

        // Keeping undisplayed for now unless additional functionality is added
        //[MenuItem(MenuLabels.AssetsToolsPath + Labels.MenuTitle, priority = MenuLabels.SettingsViewPriority)]
        //[MenuItem(MenuLabels.MenubarToolsPath + Labels.MenuTitle, priority = MenuLabels.SettingsViewPriority)]
        public static void OpenView()
        {
            var window = GetWindow<BuildSettingsView>(title: Labels.BuildSettingsTitle);
            window.buildTargetDisplay = BuildSettings.BuildTargets.ToString();

            window.Open();
        }

        public override void Draw()
        {
            if (GUILayout.Button(Labels.ResetToDefault))
            {
                if (EditorUtility.DisplayDialog(
                    title: Labels.ResetConfirmTitle,
                    message: Labels.ResetConfirmMessage,
                    ok: Labels.Reset,
                    cancel: Labels.Cancel
                    ))
                {
                    ViewModel.Reset();
                }

            }

            using (var scrollArea = new GuiTools.ScrollArea(ref scrollPos))
            {
                EditorGUILayout.Space();
                DrawLocalBuildPathPanel();

                GUILayout.Space(VERTICAL_SPACING);
                DrawDefaultBuildTargetPanel();

                EditorGUILayout.LabelField(string.Empty, GUILayout.ExpandHeight(true));

                using (var buttons = new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.Space();
                    GuiTools.DrawButton(Labels.Close, Close, GUILayout.Width(CreatorStyle.EXTRALONG_BUTTON_WIDTH));
                }

                EditorGUILayout.Space();
            }
        }

        private void DrawLocalBuildPathPanel()
        {
            EditorGUILayout.LabelField(Labels.LocalBuildPath);

            using (var bundlePathPanel = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.SelectableLabel(ViewModel.LocalBuildPath);

                if (GUILayout.Button(Labels.Ellipsis, GUILayout.Width(20)))
                {
                    var buildPath = new DirectoryInfo(ViewModel.LocalBuildPath);

                    if (!buildPath.Exists)
                    {
                        buildPath.Create();
                    }

                    var path = EditorUtility.OpenFolderPanel(
                        Labels.SelectLocalBuildPathTitle,
                        Path.GetDirectoryName(ViewModel.LocalBuildPath),
                        Path.GetFileName(ViewModel.LocalBuildPath)
                        );

                    if (!string.IsNullOrEmpty(path))
                    {
                        ViewModel.LocalBuildPath = path;
                    }
                }
            }
        }

        private void DrawDefaultBuildTargetPanel()
        {
            bool lastState = buildTargetPanelOpen;

            using (var buildTargetFoldout = new EditorGUILayout.HorizontalScope())
            {
                buildTargetPanelOpen = EditorGUILayout.Foldout(buildTargetPanelOpen, Labels.BuildTargets);
                EditorGUILayout.LabelField(buildTargetPanelOpen ? "" : $"[{buildTargetDisplay}]");
            }

            if (buildTargetPanelOpen)
            {
                using (var buildOptions = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    ViewModel.BuildForAndroid = EditorGUILayout.Toggle($"{EngagePlatform.Android}", ViewModel.BuildForAndroid);
                    ViewModel.BuildForWindows = EditorGUILayout.Toggle($"{EngagePlatform.Windows}", ViewModel.BuildForWindows);
                    ViewModel.BuildForIOS = EditorGUILayout.Toggle($"{EngagePlatform.iOS}", ViewModel.BuildForIOS);
                    ViewModel.BuildForMacOS = EditorGUILayout.Toggle($"{EngagePlatform.OSX}", ViewModel.BuildForMacOS);
                }
            }
            else
            {
                if (lastState != buildTargetPanelOpen)
                {
                    buildTargetDisplay = BuildSettings.BuildTargets.ToString();
                }
            }
        }
    }
}
