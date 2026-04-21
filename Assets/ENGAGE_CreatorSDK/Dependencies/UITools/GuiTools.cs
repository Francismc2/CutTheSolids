using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public static class GuiTools
    {
        public static void DisplayText(string title, string content)
        {
            var window = EditorWindow.CreateWindow<TextViewer>(title);
            window.Content = content;
            window.ShowUtility();
        }

        public static GUIContent ResetButtonLabel => new GUIContent(Labels.Reset, Labels.ResetTooltip);
        public static GUIContent BrowsePathLabel => new GUIContent(Labels.Ellipsis, Labels.BrowseTooltip);

        public static string ToIdString(this int id) => id < 0 ? "-" : id.ToString();
        public static string ToIdString(this int? id) => id.ToString() ?? "-";

        public static void DrawButton(string label, Action action, bool? enabled = null, params GUILayoutOption[] options)
        {
            DrawButton(new GUIContent(label), action, enabled, options);
        }

        public static void DrawButton(GUIContent label, Action action, bool? enabled = null, params GUILayoutOption[] options)
        {
            var guiEnabled = GUI.enabled;

            if (enabled.HasValue)
            {
                GUI.enabled = enabled.Value;
            }

            if (GUILayout.Button(label, options))
            {
                action?.Invoke();
            }

            GUI.enabled = guiEnabled;
        }

        public static void DrawButton(string label, Action action = null, params GUILayoutOption[] options)
        {
            DrawButton(new GUIContent(label), action, options);
        }

        public static void DrawButton(GUIContent label, Action action = null, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options))
            {
                action?.Invoke();
            }
        }

        public static void DrawHeader(string label, params GUILayoutOption[] options)
        {
            DrawHeader(new GUIContent(label), null, options);
        }

        public static void DrawHeader(string label, Action action, params GUILayoutOption[] options)
        {
            DrawHeader(new GUIContent(label), action, options);
        }

        public static void DrawHeader(GUIContent label, params GUILayoutOption[] options)
        {
            DrawHeader(label, null, options);
        }

        public static void DrawHeader(GUIContent label, Action action, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, EditorStyles.boldLabel, options))
            {
                action?.Invoke();
            }
        }

        public static void DrawProperty(string label, string value, Color color)
        {
            DrawProperty(new GUIContent(label), value, color);
        }

        public static void DrawProperty(GUIContent label, string value, Color color)
        {
            Color wasColor = GUI.contentColor;
            GUI.contentColor = color;

            EditorGUILayout.LabelField(label, value);

            GUI.contentColor = wasColor;
        }

        public static void DrawSearchBar(ref string search)
        {
            using (var searchField = new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(4);
                search = EditorGUILayout.TextField(search, EditorStyles.toolbarSearchField);
            }
        }

        public static string BrowseFolderPath(string title, string startPath)
        {
            var buildPath = new DirectoryInfo(startPath);

            if (!buildPath.Exists)
            {
                buildPath.Create();
            }

            var path = EditorUtility.OpenFolderPanel(
                title,
                buildPath.Parent?.FullName ?? buildPath.Name,
                buildPath.Name
                );

            return string.IsNullOrEmpty(path) ? startPath : path;
        }

        public static string BrowseFilePath(string title, string startPath, string[] fileTypes = null)
        {
            string path = fileTypes != null ?
                    EditorUtility.OpenFilePanelWithFilters(title, startPath, fileTypes) :
                    EditorUtility.OpenFilePanel(title, startPath, "png");

            return path;
        }

        public static string FolderSelectionField(string label, string startPath, bool showReset = true)
        {
            using (var labelPanel = new EditorGUILayout.HorizontalScope())
            {
                GuiTools.DrawHeader(label, GUILayout.Width(CreatorStyle.LONG_BUTTON_WIDTH));

                if (showReset)
                {
                    if (GUILayout.Button(ResetButtonLabel, GUILayout.Height(20), GUILayout.Width(20)))
                    {
                        return null;
                    }
                }
            }

            using (var bundlePathPanel = new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.SelectableLabel(startPath);
                EditorGUILayout.LabelField(string.Empty, GUILayout.Width(30));

                if (GUI.Button(new Rect(bundlePathPanel.rect.x + bundlePathPanel.rect.width - 30, bundlePathPanel.rect.y + (bundlePathPanel.rect.height * 0.5f) - 10, 20, 20), BrowsePathLabel))
                {
                    return BrowseFolderPath(label, startPath);
                }
                else
                {
                    return startPath;
                }
            }
        }

        public static string FileSelectionField(string label, string startPath)
        {
            EditorGUILayout.LabelField(label);

            using (var bundlePathPanel = new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.SelectableLabel(startPath);
                EditorGUILayout.LabelField(string.Empty, GUILayout.Width(30));

                if (GUI.Button(new Rect(bundlePathPanel.rect.x + bundlePathPanel.rect.width - 30, bundlePathPanel.rect.y + (bundlePathPanel.rect.height * 0.5f) - 10, 20, 20), new GUIContent(Labels.Ellipsis)))
                {
                    return BrowseFolderPath(label, startPath);
                }
                else
                {
                    return startPath;
                }
            }
        }

        public static Texture2D LoadImageFromPath(string path)
        {
            var texture = new Texture2D(1, 1);

            if (File.Exists(path))
            {
                var bytes = File.ReadAllBytes(path);
                texture.LoadImage(bytes);
                texture.name = Path.GetFileName(path);
            }

            return texture;
        }

        public class EnabledScope : IDisposable
        {
            public bool WasEnabled { get; }
            public bool Enabled { get; }

            public EnabledScope(bool enabled = false)
            {
                WasEnabled = GUI.enabled;
                GUI.enabled = Enabled = enabled;
            }

            public void Dispose()
            {
                GUI.enabled = WasEnabled;
            }
        }

        public class ColorScope : IDisposable
        {
            public Color WasColor { get; }
            public Color Color { get; }
            public Color WasBackgroundColor { get; }
            public Color BackgroundColor { get; }

            public ColorScope(Color? color = null, Color? backgroundColor = null)
            {
                WasColor = Color = GUI.contentColor;
                WasBackgroundColor = BackgroundColor = GUI.backgroundColor;

                if (color.HasValue)
                    GUI.contentColor = Color = color.Value;

                if (backgroundColor.HasValue)
                    GUI.backgroundColor = BackgroundColor = backgroundColor.Value;
            }

            public void Dispose()
            {
                GUI.contentColor = WasColor;
                GUI.backgroundColor = WasBackgroundColor;
            }
        }

        public class ScrollArea : IDisposable
        {
            public EditorGUILayout.HorizontalScope ViewportScope { get; }
            public EditorGUILayout.ScrollViewScope ScrollViewScope { get; }
            public Vector2 ScrollPosition { get; }
            public Rect Viewport { get; }

            [System.Obsolete("For backwards compatibility only")]
            public ScrollArea(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options) : this(ref scrollPosition, style, options) { }
            [System.Obsolete("For backwards compatibility only")]
            public ScrollArea(Vector2 scrollPosition, params GUILayoutOption[] options) : this(ref scrollPosition, null, options) { }

            public ScrollArea(ref Vector2 scrollPosition, params GUILayoutOption[] options) : this(ref scrollPosition, null, options) { }

            public ScrollArea(ref Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
            {
                ViewportScope = new EditorGUILayout.HorizontalScope();
                ScrollViewScope = style == null ? new EditorGUILayout.ScrollViewScope(scrollPosition, options)
                    : new EditorGUILayout.ScrollViewScope(scrollPosition, style, options);

                scrollPosition = ScrollPosition = ScrollViewScope.scrollPosition;
                Viewport = new Rect(scrollPosition, ViewportScope.rect.size);
            }

            public void Dispose()
            {
                ScrollViewScope.Dispose();
                ViewportScope.Dispose();
            }
        }


        #region Extensions
        public static string ToPretty(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var chars = input.Trim('_').ToCharArray();
            var output = new List<char>(chars.Length);
            var lastChar = '_';

            foreach (char c in chars)
            {
                if (c == '_')
                {
                    output.Add(' ');
                }
                else if (lastChar == '_')
                {
                    output.Add(char.ToUpper(c));
                }
                else if (char.IsUpper(c) && char.IsLower(lastChar))
                {
                    output.Add(' ');
                    output.Add(c);
                }
                else
                {
                    output.Add(c);
                }

                lastChar = c;
            }

            return new string(output.ToArray());
        }

        #endregion
    }
}
