using XNodeEditor;
using UnityEngine;
using System;
using UnityEditor;
using XNode;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(ObjectReferenceNode))]
    public class ObjectReferenceEditor : AFXNodeEditor
    {
        private ObjectReferenceNode objRefNode;
        private SerializedProperty refName;
        private NodePort outputPort;
        private NodePort valuePort;
        private bool hasValuePort;

        public override void OnCreate()
        {
            objRefNode = target as ObjectReferenceNode;
            refName = serializedObject.FindProperty("referenceName");
            outputPort = objRefNode.GetOutputPort(ObjectReferenceNode.ObjectOutPortName);

            hasValuePort = objRefNode.HasPort(ObjectReferenceNode.ValueOutPortName);
            if (hasValuePort)
            {
                valuePort = objRefNode.GetOutputPort(ObjectReferenceNode.ValueOutPortName);
            }
        }

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            NodeEditorGUILayout.PortField(outputPort);

            if (hasValuePort)
            {
                NodeEditorGUILayout.PortField(valuePort);
            }

            NodeEditorGUILayout.PropertyField(refName, GUIContent.none);

            if (GUI.changed)
            {
                objRefNode.Graph.RequiresRefSlotRefresh = true;
            }

            serializedObject.ApplyModifiedProperties();

            ErrorDisplay();
        }

        private void ErrorDisplay()
        {
            if (Application.isPlaying)
            {
                if (string.IsNullOrEmpty(objRefNode.ReferenceName))
                {
                    objRefNode.Error = "Reference name is blank";
                    return;
                }

                if (outputPort.GetOutputValue() == null)
                {
                    objRefNode.Error = $"[{objRefNode.name}] Slot is empty: {objRefNode.ReferenceName}";
                }
            }
            else { objRefNode.Error = null; }
        }

        public override Color GetTint()
        {
            if (string.IsNullOrEmpty(objRefNode.ReferenceName) || !string.IsNullOrEmpty(objRefNode.Error))
            {
                return AFXNodeColors.Error;
            }

            return AFXNodeColors.Reference;
        }

        public override string GetTitle()
        {
            string title = base.GetTitle();
            title = title.Replace("Reference", "");
            return title;
        }

        public override int GetWidth()
        {
            if (!String.IsNullOrEmpty(objRefNode.Error) && Application.isPlaying) return base.GetWidth();

            return 125;
        }
    }
}