using UnityEngine;
using XNodeEditor;
using UnityEditor;
using System;
using System.Linq;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(AFXNode))]
    public class AFXNodeEditor : NodeEditor
    {
        private AFXNode afxNode;

        private AFXNode AfxNode 
        {
            get
            {
                if (afxNode == null)
                {
                    afxNode = target as AFXNode;
                    return afxNode;
                }
                return afxNode;
            }
        }

        public override void AddContextMenuItems(GenericMenu menu)
        {
            bool canRemove = true;
            if (canRemove) menu.AddItem(new GUIContent("Remove"), false, NodeEditorWindow.current.RemoveSelectedNodes);
            else menu.AddItem(new GUIContent("Remove"), false, null);

            // Custom sections if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node)
            {
                XNode.Node node = Selection.activeObject as XNode.Node;
                menu.AddCustomContextMenuItems(node);
            }
        }

        public override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
            if (!String.IsNullOrEmpty(AfxNode.Error))
            {
                Color color = GUI.color;
                GUI.color = AFXNodeColors.Error;
                EditorGUILayout.LabelField(AfxNode.Error, AFXStyles.Error);
                EditorGUILayout.LabelField("", NodeEditorResources.styles.horizontalLine);
                GUI.color = color;
            }
        }

        public override string GetTitle()
        {
            NodeEditorResources.styles.nodeHeader.wordWrap = true;
            return base.GetTitle();
        }

        public override Color GetTint()
        {
            return AFXNodeColors.Normal;
        }
    }
}