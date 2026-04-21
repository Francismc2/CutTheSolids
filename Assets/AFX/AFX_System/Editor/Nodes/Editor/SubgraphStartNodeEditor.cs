using XNodeEditor;
using UnityEngine;
using XNode;
using UnityEditor;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(SubGraphStartNode))]
    public class SubGraphStartNodeEditor : NodeEditor
    {
        private SubGraphStartNode startNode;
        private NodePort templatePort;
        private string inputName = "";

        GUIStyle headerStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        GUIStyle textStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 10 };

        public override void OnBodyGUI()
        {
            if (startNode == null) startNode = target as SubGraphStartNode;
            base.OnBodyGUI();
            
            if (GUILayout.Button("Clear Inputs"))
            {
                startNode.ClearDynamicPorts();
            }

            AddOutputs(headerStyle, textStyle);
        }

        private void AddOutputs(GUIStyle headerStyle, GUIStyle textStyle)
        {
            GUILayout.Label("Add An Input", headerStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("Enter name for Input port", textStyle, GUILayout.ExpandWidth(true));

            inputName = EditorGUILayout.TextField(inputName);

            if (templatePort == null) templatePort = startNode.GetInputPort("addInput");
            GUILayout.Label("Connect Value Here", textStyle, GUILayout.ExpandWidth(true));
            NodeEditorGUILayout.PortField(templatePort);
            if (GUILayout.Button("Add Input"))
            {
                if (!string.IsNullOrEmpty(inputName))
                {
                    startNode.DynamicNodesOutput.Add(startNode.AddDynamicOutput(templatePort.Connection.ValueType, Node.ConnectionType.Multiple, Node.TypeConstraint.None, inputName));
                    templatePort.ClearConnections();
                    inputName = "";
                }
                else
                {
                    Debug.Log($"[{startNode}] Input Name is Empty!");
                }
            }
        }
    }
}