using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace XNodeEditor {
    public static class NodeEditorResources 
    {
        public static Dictionary<string, Texture> TextureOverrides = new()
            {
                {"AFXFlow", arrow},
                {typeof(UnityEngine.Object).Name, square}
            };

        // Textures
        public static Texture2D arrow { get { return _arrow != null ? _arrow : _arrow = Resources.Load<Texture2D>("xnode_arrow"); } }
        private static Texture2D _arrow;
        public static Texture2D square { get { return _square != null ? _square : _square = Resources.Load<Texture2D>("xnode_square"); } }
        private static Texture2D _square;
        public static Texture2D header { get { return _header != null ? _header : _header = Resources.Load<Texture2D>("xnode_header"); } }
        private static Texture2D _header;
        public static Texture2D dot { get { return _dot != null ? _dot : _dot = Resources.Load<Texture2D>("xnode_dot"); } }
        private static Texture2D _dot;
        public static Texture2D dotOuter { get { return _dotOuter != null ? _dotOuter : _dotOuter = Resources.Load<Texture2D>("xnode_dot_outer"); } }
        private static Texture2D _dotOuter;
        public static Texture2D nodeBody { get { return _nodeBody != null ? _nodeBody : _nodeBody = Resources.Load<Texture2D>("xnode_node"); } }
        private static Texture2D _nodeBody;
        public static Texture2D nodeHighlight { get { return _nodeHighlight != null ? _nodeHighlight : _nodeHighlight = Resources.Load<Texture2D>("xnode_node_highlight"); } }
        private static Texture2D _nodeHighlight;

        // Styles
        public static Styles styles { get { return _styles != null ? _styles : _styles = new Styles(); } }
        public static Styles _styles = null;
        public static GUIStyle OutputPort { get { return new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleRight }; } }
        public class Styles {
            public GUIStyle inputPort, nodeHeader, horizontalLine, nodeBody, tooltip, nodeHighlight;

            public Styles() {
                GUIStyle baseStyle = new GUIStyle("Label");
                baseStyle.fixedHeight = 18;

                inputPort = new GUIStyle(baseStyle);
                inputPort.alignment = TextAnchor.UpperLeft;
                inputPort.padding.left = 10;

                nodeHeader = new GUIStyle();
                nodeHeader.alignment = TextAnchor.MiddleCenter;
                nodeHeader.fontStyle = FontStyle.Bold;
                nodeHeader.normal.textColor = Color.white;
                                
                nodeBody = new GUIStyle();
                nodeBody.normal.background = NodeEditorResources.nodeBody;
                nodeBody.border = new RectOffset(32, 32, 32, 32);
                nodeBody.padding = new RectOffset(16, 16, 4, 16);

                nodeHighlight = new GUIStyle();
                nodeHighlight.normal.background = NodeEditorResources.nodeHighlight;
                nodeHighlight.border = new RectOffset(32, 32, 32, 32);

                tooltip = new GUIStyle("helpBox");
                tooltip.alignment = TextAnchor.MiddleCenter;

                horizontalLine = new GUIStyle();
                horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
                horizontalLine.fixedHeight = 2;

            }
        }

        public static Texture2D GenerateGridTexture(Color line, Color bg) {
            Texture2D tex = new Texture2D(64, 64);
            Color[] cols = new Color[64 * 64];
            for (int y = 0; y < 64; y++) {
                for (int x = 0; x < 64; x++) {
                    Color col = bg;
                    if (y % 16 == 0 || x % 16 == 0) col = Color.Lerp(line, bg, 0.65f);
                    if (y == 63 || x == 63) col = Color.Lerp(line, bg, 0.35f);
                    cols[(y * 64) + x] = col;
                }
            }
            tex.SetPixels(cols);
            tex.wrapMode = TextureWrapMode.Repeat;
            tex.filterMode = FilterMode.Bilinear;
            tex.name = "Grid";
            tex.Apply();
            return tex;
        }

        public static Texture2D GenerateCrossTexture(Color line) {
            Texture2D tex = new Texture2D(64, 64);
            Color[] cols = new Color[64 * 64];
            for (int y = 0; y < 64; y++) {
                for (int x = 0; x < 64; x++) {
                    Color col = line;
                    if (y != 31 && x != 31) col.a = 0;
                    cols[(y * 64) + x] = col;
                }
            }
            tex.SetPixels(cols);
            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.name = "Grid";
            tex.Apply();
            return tex;
        }
    }
}