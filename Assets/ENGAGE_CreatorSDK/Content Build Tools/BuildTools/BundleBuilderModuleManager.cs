using UnityEditor;
using System;
using System.Collections.Generic;

namespace Engage.BuildTools
{
    public static partial class BundleBuilder
    {
        private static readonly ModuleManager moduleManager = new ModuleManager();
        private static readonly HashSet<BuildTarget> installedBuildModules = new HashSet<BuildTarget>();
        private static bool installedBuildModulesInitialized = false;

        public static HashSet<BuildTarget> InstalledBuildModules
        {
            get
            {
                if (!installedBuildModulesInitialized)
                {
                    foreach (var target in SupportedBuildTargets)
                    {
                        if (moduleManager.IsModuleInstalled(target))
                        {
                            installedBuildModules.Add(target);
                        }
                    }
                }

                return installedBuildModules;
            }
        }

        public class ModuleManager
        {
            // Unity ModuleManager is an internal class, and can't be directly called due to its protection level.
            private readonly Type type;
            private readonly Func<BuildTarget, string> getTargetStringFromBuildTarget;
            private readonly Predicate<string> isPlatformSupportLoaded;

            public ModuleManager()
            {
                type = Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");

                var getTargetString = type.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                getTargetStringFromBuildTarget = (Func<BuildTarget, string>)getTargetString.CreateDelegate(typeof(Func<BuildTarget, string>));

                var isPlatformLoaded = type.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                isPlatformSupportLoaded = (Predicate<string>)isPlatformLoaded.CreateDelegate(typeof(Predicate<string>));
            }

            public bool IsModuleInstalled(BuildTarget target)
            {
                var targetString = getTargetStringFromBuildTarget?.Invoke(target);
                return isPlatformSupportLoaded?.Invoke(targetString) ?? false;
            }

            public BuildTarget[] GetInstalledModules()
            {
                var installedTargets = new List<BuildTarget>();

                foreach(var target in SupportedBuildTargets)
                {
                    if (IsModuleInstalled(target))
                    {
                        installedTargets.Add(target);
                    }
                }

                return installedTargets.ToArray();
            }
        }
    }
}
