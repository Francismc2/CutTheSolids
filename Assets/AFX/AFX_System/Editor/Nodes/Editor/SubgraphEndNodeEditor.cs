using XNodeEditor;
using UnityEngine;
using XNode;
using UnityEditor;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(SubGraphEndNode))]
    public class SubGraphEndNodeEditor : NodeEditor
    {
        private SubGraphEndNode endNode;
        private NodePort templatePort;
        private string outputName = "";

        GUIStyle headerStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        GUIStyle textStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 10 };

        public override void OnBodyGUI()
        {
            if (endNode == null) endNode = target as SubGraphEndNode;
            base.OnBodyGUI();
            if (Application.isPlaying && endNode.ParentNode.Graph != null)
            {
                if (GUILayout.Button("Open Parent Graph", GUILayout.Height(50)))
                {
                    window = NodeEditorWindow.Open(endNode.ParentNode.Graph);
                    window.Repaint();
                }
            }

            GUILayout.Space(20);
            if (GUILayout.Button("Clear Outputs"))
            {
                endNode.ClearDynamicPorts();
                endNode.DynamicNodesInput.Clear();
            }
            AddOutputs(headerStyle, textStyle);
        }

        private void AddOutputs(GUIStyle headerStyle, GUIStyle textStyle)
        {
            GUILayout.Label("Add An Output", headerStyle, GUILayout.ExpandWidth(true));
            GUILayout.Label("Enter name for output slot", textStyle, GUILayout.ExpandWidth(true));

            outputName = EditorGUILayout.TextField(outputName);

            if (templatePort == null) templatePort = endNode.GetInputPort("addOutput");
            GUILayout.Label("Connect Value Here", textStyle, GUILayout.ExpandWidth(true));
            NodeEditorGUILayout.PortField(templatePort);
            if (GUILayout.Button("Add Output"))
            {
                if (!string.IsNullOrEmpty(outputName))
                {
                    endNode.DynamicNodesInput.Add(endNode.AddDynamicInput(templatePort.Connection.ValueType, Node.ConnectionType.Override, Node.TypeConstraint.Strict, outputName));
                    NodePort targetPort = endNode.GetPort(outputName);
                    foreach (var connection in templatePort.GetConnections())
                    {
                        connection.Connect(targetPort);
                    }
                    templatePort.ClearConnections();
                    outputName = "";
                }
                else
                {
                    Debug.Log($"[{endNode}] Output Name is Empty!");
                }
            }
        }
    }
}