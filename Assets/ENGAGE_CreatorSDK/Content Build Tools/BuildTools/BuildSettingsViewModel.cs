using System.Collections.Generic;

namespace Engage.BuildTools
{
    public class BuildSettingsViewModel : ViewModel
    {
        public void Reset()
        {
            BuildSettings.Reset();
            NotifyPropertyChange(nameof(BuildSettings));
        }

        public string LocalBuildPath { get => BuildSettings.LocalBuildPath; set => BuildSettings.LocalBuildPath = value; }

        public bool BuildForAndroid
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.Android);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.Android;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.Android;
                }
            }
        }
        public bool BuildForWindows
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.Windows);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.Windows;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.Windows;
                }
            }
        }
        public bool BuildForIOS
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.iOS);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.iOS;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.iOS;
                }
            }
        }
        public bool BuildForMacOS
        {
            get => BuildSettings.BuildTargets.HasFlag(EngagePlatform.OSX);
            set
            {
                if (value)
                {
                    BuildSettings.BuildTargets |= EngagePlatform.OSX;
                }
                else
                {
                    BuildSettings.BuildTargets &= ~EngagePlatform.OSX;
                }
            }
        }
        public string BuildTargetString
        {
            get
            {
                var buildTargets = new List<string>();

                foreach (EngagePlatform target in System.Enum.GetValues(typeof(EngagePlatform)))
                {
                    if (target > EngagePlatform.None && BuildSettings.BuildTargets.HasFlag(target))
                    {
                        buildTargets.Add(target.ToString());
                    }
                }

                return string.Join(", ", buildTargets);
            }
        }
    }
}
