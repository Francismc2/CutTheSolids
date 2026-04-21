using System;
using System.Collections.Generic;
using System.Linq;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;
using XNode;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
#endif

namespace XNodeEditor {
    /// <summary> Base class to derive custom Node editors from. Use this to create your own custom inspectors and editors for your nodes. </summary>
    [CustomNodeEditor(typeof(XNode.Node))]
    public class NodeEditor : XNodeEditor.Internal.NodeEditorBase<NodeEditor, NodeEditor.CustomNodeEditorAttribute, XNode.Node> {
        private Color guiColor;
        private readonly Color DEFAULTCOLOR = new Color32(90, 97, 105, 255);

        /// <summary> Fires every whenever a node was modified through the editor </summary>
        public static Action<XNode.Node> onUpdateNode;
        public readonly static Dictionary<XNode.NodePort, Vector2> portPositions = new Dictionary<XNode.NodePort, Vector2>();

#if ODIN_INSPECTOR
        protected internal static bool inNodeEditor = false;
#endif
        /// <summary> Returns color for target port </summary>
        public static Color GetPortColor(NodePort port)
        {
            if (NodeEditorWindow.current?.graphEditor?.GetPortColor(port) == null) return Color.white;
            return NodeEditorWindow.current.graphEditor.GetPortColor(port);
        }

        public virtual void OnHeaderGUI() {
            GUILayout.Label(GetTitle(), GetHeaderStyle(), GUILayout.Height(30));
            guiColor = GUI.color;
            GUI.color = GetAccentTint();
            EditorGUILayout.LabelField("", NodeEditorResources.styles.horizontalLine);
            GUI.color = guiColor;
        }

        /// <summary> Draws standard field editors for all public fields </summary>
        public virtual void OnBodyGUI() {
#if ODIN_INSPECTOR
            inNodeEditor = true;
#endif

            // Unity specifically requires this to save/update any serial object.
            // serializedObject.Update(); must go at the start of an inspector gui, and
            // serializedObject.ApplyModifiedProperties(); goes at the end.
            serializedObject.Update();
            string[] excludes = { "m_Script", "graph", "position", "ports" };

#if ODIN_INSPECTOR
            InspectorUtilities.BeginDrawPropertyTree(objectTree, true);
            GUIHelper.PushLabelWidth(84);
            objectTree.Draw(true);
            InspectorUtilities.EndDrawPropertyTree(objectTree);
            GUIHelper.PopLabelWidth();
#else

            // Iterate through serialized properties and draw them like the Inspector (But with ports)
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren)) {
                enterChildren = false;
                if (excludes.Contains(iterator.name)) continue;
                NodeEditorGUILayout.PropertyField(iterator, true);
            }
#endif

            // Iterate through dynamic ports and draw them in the order in which they are serialized
            foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }

            serializedObject.ApplyModifiedProperties();

#if ODIN_INSPECTOR
            // Call repaint so that the graph window elements respond properly to layout changes coming from Odin
            if (GUIHelper.RepaintRequested) {
                GUIHelper.ClearRepaintRequest();
                window.Repaint();
            }
#endif

#if ODIN_INSPECTOR
            inNodeEditor = false;
#endif
        }

        public virtual int GetWidth() {
            Type type = target.GetType();
            int width;
            if (type.TryGetAttributeWidth(out width)) return width;
            else return 208;
        }

        /// <summary> Returns color for target node </summary>
        public virtual Color GetTint() {
            // Try get color from [NodeTint] attribute
            Type type = target.GetType();
            Color color;
            if (type.TryGetAttributeTint(out color)) return color;
            // Return default color (grey)
            else return DEFAULTCOLOR;
        }

        /// <summary> Returns color for target nodes Accent details </summary>
        public virtual Color GetAccentTint()
        {
            // Try get color from [NodeAccentTint] attribute
            Type type = target.GetType();
            Color color;
            if (type.TryGetAttributeAccentTint(out color)) return color;

            // Return default color (grey)
            NodePort port = null;
            if (target.Inputs.Count() > 0)
            {
                port = target.Inputs.First();
            }
            else if (target.Outputs.Count() > 0)
            {
                port = target.Outputs.First();
            }

            if (port == null) return Color.white;

            if (NodeEditorWindow.current?.graphEditor?.GetPortColor(port) == null) return Color.white;
            return NodeEditorWindow.current.graphEditor.GetPortColor(port);
        }

        /// <summary> Returns title for target node </summary>
        public virtual string GetTitle()
        {
            // Try get title from [NodeTint] attribute
            Type type = target.GetType();
            string title;
            if (type.TryGetAttributeTitle(out title)) return title;

            else return target.name;
        }

        public virtual GUIStyle GetBodyStyle() {
            return NodeEditorResources.styles.nodeBody;
        }

        public virtual GUIStyle GetHeaderStyle()
        {
            return NodeEditorResources.styles.nodeHeader;
        }

        public virtual GUIStyle GetBodyHighlightStyle() {
            return NodeEditorResources.styles.nodeHighlight;
        }

        /// <summary> Add items for the context menu when right-clicking this node. Override to add custom menu items. </summary>
        public virtual void AddContextMenuItems(GenericMenu menu) {
            bool canRemove = true;
            // Actions if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node) {
                XNode.Node node = Selection.activeObject as XNode.Node;
                menu.AddItem(new GUIContent("Move To Top"), false, () => NodeEditorWindow.current.MoveNodeToTop(node));
                menu.AddItem(new GUIContent("Rename"), false, NodeEditorWindow.current.RenameSelectedNode);

                canRemove = NodeGraphEditor.GetEditor(node.graph, NodeEditorWindow.current).CanRemove(node);
            }

            // Add actions to any number of selected nodes
            menu.AddItem(new GUIContent("Copy"), false, NodeEditorWindow.current.CopySelectedNodes);
            menu.AddItem(new GUIContent("Duplicate"), false, NodeEditorWindow.current.DuplicateSelectedNodes);

            if (canRemove) menu.AddItem(new GUIContent("Remove"), false, NodeEditorWindow.current.RemoveSelectedNodes);
            else menu.AddItem(new GUIContent("Remove"), false, null);

            // Custom sctions if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node) {
                XNode.Node node = Selection.activeObject as XNode.Node;
                menu.AddCustomContextMenuItems(node);
            }
        }

        /// <summary> Rename the node asset. This will trigger a reimport of the node. </summary>
        public void Rename(string newName) {
            if (newName == null || newName.Trim() == "") newName = NodeEditorUtilities.NodeDefaultName(target.GetType());
            target.name = newName;
            OnRename();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
        }

        /// <summary> Called after this node's name has changed. </summary>
        public virtual void OnRename() { }

        [AttributeUsage(AttributeTargets.Class)]
        public class CustomNodeEditorAttribute : Attribute,
        XNodeEditor.Internal.NodeEditorBase<NodeEditor, NodeEditor.CustomNodeEditorAttribute, XNode.Node>.INodeEditorAttrib {
            private Type inspectedType;
            /// <summary> Tells a NodeEditor which Node type it is an editor for </summary>
            /// <param name="inspectedType">Type that this editor can edit</param>
            public CustomNodeEditorAttribute(Type inspectedType) {
                this.inspectedType = inspectedType;
            }

            public Type GetInspectedType() {
                return inspectedType;
            }
        }
    }
}