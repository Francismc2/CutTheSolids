using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public class TextViewer : EditorWindow
    {
        private Vector2 scrollPos;

        public string Content { get; set; }

        private void OnGUI()
        {
            using (var scrollarea = new GuiTools.ScrollArea(ref scrollPos))
            {
                EditorGUILayout.TextArea(Content);
            }

            if (GUILayout.Button("Done"))
            {
                Close();
            }
        }
    }
}
