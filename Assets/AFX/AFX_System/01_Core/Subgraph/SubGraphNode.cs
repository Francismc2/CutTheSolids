using UnityEngine;
using XNode;
using System.Linq;
using System.Collections.Generic;

namespace Engage.AFX.v1
{
    [NodeTitle("Run Subgraph")]
    [CreateNodeMenu(AFXMenuTree.Subgraph + "Run Subgraph")]
    public class SubGraphNode : AFXActiveNode
    {
        [HideInInspector]
        [SerializeField]
        public AFXSubGraph SubGraph
        {
            get
            {
                if (Application.isPlaying)
                {
                    if (subgraphEditor == null) return null;
                    if (subgraphRuntime == null)
                    {
                        subgraphRuntime = (AFXSubGraph)subgraphEditor.Copy();
                        SetupSubGraph(subgraphRuntime);
                        return subgraphRuntime;
                    }
                    return subgraphRuntime;
                }
                return subgraphEditor;
            }
            set
            {
                subgraphEditor = value;
            }
        }

        [HideInInspector]
        [SerializeField]
        private AFXSubGraph subgraphRuntime;
        [HideInInspector]
        [SerializeField]
        private AFXSubGraph subgraphEditor;

        [ContextMenu("Setup Ports")]
        public void SetupPorts()
        {
            if (SubGraph == null)
            {
                ClearAllPorts();
                return;
            }

            SetupSubGraph(SubGraph);
            ClearAllPorts();

            //Generate Output Ports
            foreach (NodePort port in SubGraph.EndNode.DynamicInputs.OrderByDescending(item => item.fieldName))
            {
                AddDynamicOutput(port.ValueType, ConnectionType.Multiple, TypeConstraint.None, port.fieldName);
            }

            //Generate Input Ports
            foreach (NodePort item in SubGraph.StartNode.DynamicOutputs.OrderByDescending(item => item.fieldName))
            {
                if (this.DynamicOutputs.Contains(item)) continue;
                AddDynamicInput(item.ValueType, ConnectionType.Multiple, TypeConstraint.None, item.fieldName);
            }
        }

        public void ClearAllPorts()
        {
            ClearDynamicPorts();
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SubGraph.StartNode.ExecuteNode(afxFlow);
        }

        public void ContinueToNextNode()
        {
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            foreach (NodePort dynamicPort in SubGraph.EndNode.Ports)
            {
                if (port.fieldName == dynamicPort.fieldName)
                {
                    return SubGraph.EndNode.GetInputPort(dynamicPort.fieldName).GetInputValue();
                }
            }

            return null;
        }

        public void SetupSubGraph(AFXSubGraph nodeGraphIn)
        {
            nodeGraphIn.ParentGraph = this.Graph;
            foreach (SubGraphStartNode start in nodeGraphIn.nodes.Where(node => node is SubGraphStartNode))
            {
                nodeGraphIn.StartNode = start;
                nodeGraphIn.StartNode.ParentNode = this;
            }

            foreach (SubGraphEndNode end in nodeGraphIn.nodes.Where(node => node is SubGraphEndNode))
            {
                nodeGraphIn.EndNode = end;
                nodeGraphIn.EndNode.ParentNode = this;
            }
        }

        List<ObjectReferenceNode> GetObjectReferenceList(AFXSubGraph nodeGraphIn)
        {
            List<ObjectReferenceNode> tempList = new List<ObjectReferenceNode>();
            foreach (ObjectReferenceNode graphRef in nodeGraphIn.nodes.Where(node => node is ObjectReferenceNode))
            {
                tempList.Add(graphRef);
            }

            return tempList;
        }
    }
}