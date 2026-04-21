using XNodeEditor;
using UnityEngine;
using UnityEditor;

namespace Engage.AFX.v1
{
    [CustomNodeEditor(typeof(SubGraphNode))]
    public class SubGraphNodeEditor : AFXNodeEditor
    {
        private const string enterFlow = "enter";
        private const string exitFlow = "exit";

        private SubGraphNode subgraphNode;

        private GUIStyle nodeHeader = new(NodeEditorResources.styles.nodeHeader);

        private AFXSubGraph subgraphCache;

        public override void OnCreate()
        {
            base.OnCreate();
            if (subgraphNode == null) subgraphNode = target as SubGraphNode;
            if (subgraphNode.SubGraph != null)
            {
                subgraphCache = subgraphNode.SubGraph;
            }
        }

        public override string GetTitle()
        {
            nodeHeader.wordWrap = true;
            if (subgraphNode?.SubGraph != null)
            {
                return subgraphNode.SubGraph.name + " -SubGraph";
            }

            return base.GetTitle();
        }

        public override void OnBodyGUI()
        {
            if (target.HasPort(enterFlow) && target.HasPort(exitFlow))
            {
                NodeEditorGUILayout.PortPair(target.GetInputPort(enterFlow), target.GetOutputPort(exitFlow));
            }

            serializedObject.Update();

            // Iterate through dynamic ports and draw them in the order in which they are serialized
            foreach (XNode.NodePort dynamicPort in target.DynamicInputs)
            {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }

            foreach (XNode.NodePort dynamicPort in target.DynamicOutputs)
            {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                NodeEditorGUILayout.PortField(dynamicPort);
            }

            serializedObject.ApplyModifiedProperties();

            GUILayout.Label("-Subgraph-", nodeHeader, GUILayout.ExpandWidth(true));
            
            if (Application.isPlaying)
            {

                if (GUILayout.Button("Open SubGraph", GUILayout.Height(50)))
                {
                    window = NodeEditorWindow.Open(subgraphNode.SubGraph);
                    window.Repaint();
                }
            }
            else
            {
                subgraphNode.SubGraph = (AFXSubGraph)EditorGUILayout.ObjectField(subgraphNode.SubGraph, typeof(AFXSubGraph), false);

                if (subgraphNode.SubGraph == null)
                {
                    subgraphNode.ClearAllPorts();
                    subgraphCache = null;
                    return;
                }

                if (subgraphNode.SubGraph != subgraphCache)
                {
                    subgraphNode.SetupPorts();
                    subgraphCache = subgraphNode.SubGraph;
                }

                if (GUILayout.Button("Refresh Ports", GUILayout.Height(50)))
                {
                    subgraphNode.SetupPorts();
                }
            }
        }

        public override Color GetTint()
        {
            return AFXNodeColors.Subgraph;
        }
    }
}