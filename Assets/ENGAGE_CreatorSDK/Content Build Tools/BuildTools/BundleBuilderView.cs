using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public class BundleBuilderView : View<BundleBuildJobManager>
    {
        private static bool buildTargetPanelOpen;
        private string buildTargetDisplay;
        private Vector2 scrollPos;
        private const float verticalSpace = 20f;

        private class Labels : BuildTools.Labels
        {
            public const string MenuTitle = "Bundle Builder";
            public const string ViewTitle = "Engage Bundle Builder";
            public const string Build = "Build Bundles";
            public const string ThumbnailTool = "Thumbnail Tool";
            public const string ThumbnailTooltip = "Thumbnail Tool is currently Work In Progress";

            public const string BatchBuildMode = "Batch Build Mode";
            public const string BatchBuildTooltip = "Batch build mode will create duplicate copies of the project and run the bundle build process in parallel";

            public static string[] Instructions = new string[]
            {
                "How To Use:",
                "Select the folders that you want made into bundles in the project window.",
                "Check the platforms you wish to build for. Click the Build Bundles button.",
                "",
                "The first time you build in a project can take a very long time (e.g. hours),",
                "subsequent builds will be much faster"
            };
        }


        [MenuItem(MenuLabels.AssetsToolsPath + Labels.MenuTitle, priority = MenuLabels.BuildViewPriority + 1)]
        [MenuItem(MenuLabels.CreatorSDKToolsPath + Labels.MenuTitle, priority = MenuLabels.BuildViewPriority + 1)]
        public static void OpenView() => GetWindow<BundleBuilderView>(title: Labels.ViewTitle).Open();

        protected override void Initialize()
        {
            buildTargetDisplay = ViewModel.BuildTargetString;
        }

        public override void Draw()
        {
            DrawInstructionText();

            // Possibly later include button to open Build Settings
            //GuiTools.DrawButton(new GUIContent("Build Settings"), BuildSettingsView.Open, GUILayout.Width(CreatorStyle.EXTRALONG_BUTTON_WIDTH));

            GUILayout.Space(verticalSpace);

            ViewModel.LocalBuildPath = GuiTools.FolderSelectionField(Labels.LocalBuildPath, ViewModel.LocalBuildPath);

            GUILayout.Space(verticalSpace);

            DrawBuildTargetPanel();

            GUILayout.Space(verticalSpace);

            ViewModel.BatchBuild = EditorGUILayout.Toggle(new GUIContent(Labels.BatchBuildMode, Labels.BatchBuildTooltip), ViewModel.BatchBuild);

            DrawJobQueue();

            using (var buttons = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(Labels.Refresh))
                {
                    ViewModel.Refresh();
                }

                if (GUILayout.Button(Labels.Build))
                {
                    ViewModel.BuildBundles();
                }
            }
        }

        private void DrawInstructionText()
        {
            foreach(var line in Labels.Instructions)
            {
                EditorGUILayout.LabelField(line);
            }
        }

        private void DrawJobQueue()
        {
            using (var scrollArea = new GuiTools.ScrollArea(ref scrollPos, EditorStyles.helpBox))
            {
                foreach (var job in ViewModel.JobList)
                {
                    DrawBuildJob(job);
                }
            }
        }

        private void DrawBuildJob(BundleBuildJob job)
        {
            using (var jobRow = new EditorGUILayout.HorizontalScope())
            {
                job.BundleLabel = EditorGUILayout.TextField(job.BundleLabel);
                EditorGUILayout.LabelField($"{job.AssetType}");
                EditorGUILayout.LabelField(job.BundlePath);
            }
        }

        private void DrawBuildTargetPanel()
        {
            bool lastState = buildTargetPanelOpen;

            using (var buildTargetFoldout = new EditorGUILayout.HorizontalScope())
            {
                buildTargetPanelOpen = EditorGUILayout.Foldout(buildTargetPanelOpen, "Building for");
                EditorGUILayout.LabelField(buildTargetPanelOpen ? "" : $"[{buildTargetDisplay}]");
            }

            if (buildTargetPanelOpen)
            {
                using (var buildOptions = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    var enabled = GUI.enabled;

                    GUI.enabled = ViewModel.AndroidModuleInstalled;
                    ViewModel.BuildForAndroid = EditorGUILayout.Toggle($"{EngagePlatform.Android}", ViewModel.BuildForAndroid);

                    GUI.enabled = ViewModel.WindowsModuleInstalled;
                    ViewModel.BuildForWindows = EditorGUILayout.Toggle($"{EngagePlatform.Windows}", ViewModel.BuildForWindows);

                    GUI.enabled = ViewModel.IosModuleInstalled;
                    ViewModel.BuildForIOS = EditorGUILayout.Toggle($"{EngagePlatform.iOS}", ViewModel.BuildForIOS);

                    GUI.enabled = ViewModel.OsxModuleInstalled;
                    ViewModel.BuildForMacOS = EditorGUILayout.Toggle($"{EngagePlatform.OSX}", ViewModel.BuildForMacOS);

                    GUI.enabled = enabled;
                }
            }
            else
            {
                if (lastState != buildTargetPanelOpen)
                {
                    buildTargetDisplay = ViewModel.BuildTargetString;
                }
            }
        }
    }
}




    

    
              
        