namespace Engage.BuildTools
{
    public partial class MenuLabels
    {
        public const string Assets = "Assets/";
        public const string CreatorSDK = "Creator SDK/";
        public const string Engage = "ENGAGE/";
        public const string AssetTools = "Asset Tools/";
        public const string Export = "Export/";

        // Asset Management System
        public const string EngageAssetManagementPath = CreatorSDK + "Asset Management System/";
        public const string EngageContentManagementPath = CreatorSDK + "Content Management/";

        // Export
        public const string ExportMenuPath = Engage + Export;

        // Asset Tools
        public const string AssetsToolsPath = Assets + AssetTools;
        public const string CreatorSDKToolsPath = CreatorSDK + AssetTools;

        // Quick Tools
        public const string QuickToolsPath = Assets + "Quick Tools/";

        public const string GameObjectContext = "GameObject/";
        public const string GameObjectPrimitivesContext = "GameObject/3D Object/";
        public const string GameObjectAlignRoot = "GameObject/Align Root/";

        // Project Conversion Tools
        public const string ProjectConversionTools = CreatorSDK + "Project Conversion Tools/";

        public const int AssetToolsPriority = 100;
        public const int BuildViewPriority = 0;

        public const int ManagerViewPriority = 100;
        public const int TestingViewPriority = 1000;
        public const int VersionViewPriority = 2000;
        public const int ExportViewPriority = 3000;
        public const int OtherViewPriority = 10000;

        #region Legacy Constants
        [System.Obsolete("For backwards compatibility only")]
        public const string MenubarToolsPath = "Creator SDK/Asset Tools/";
        [System.Obsolete("For backwards compatibility only")]
        public const int SettingsViewPriority = 200;
        #endregion
    }
}
