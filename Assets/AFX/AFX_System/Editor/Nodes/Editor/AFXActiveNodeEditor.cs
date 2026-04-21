using System.Linq;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(AFXActiveNode))]
    public class AFXActiveNodeEditor : AFXNodeEditor
    {
        public override void OnBodyGUI()
        {
            if (target.HasPort("enter") && target.HasPort("exit"))
            {
                NodeEditorGUILayout.PortPair(target.GetInputPort("enter"), target.GetOutputPort("exit"));
            }

            serializedObject.Update();
            string[] excludes = { "m_Script", "graph", "position", "ports", "exit", "enter" };

            // Iterate through serialized properties and draw them like the Inspector (But with ports)
            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (excludes.Contains(iterator.name)) continue;
                NodeEditorGUILayout.PropertyField(iterator, true);
            }

            // Iterate through dynamic ports and draw them in the order in which they are serialized
            foreach (XNode.NodePort dynamicPort in target.DynamicPorts)
            {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public override Color GetTint()
        {
            return AFXNodeColors.Active;
        }
    }
}