using UnityEngine;
using XNode;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using XNode.NodeGroups;

namespace Engage.AFX.v1
{
    [CreateAssetMenu]
    public class AFXGraph : NodeGraph
    {
        private const float graphVersion = 1.000f; // Update this with every major revision of system.
        public float GraphVersion { get => graphVersion;}
        public bool RequiresRefSlotRefresh { get; set; }

        public Dictionary<string, UnityEngine.Object> ObjectReferencesGraph { get; } = new Dictionary<string, UnityEngine.Object>();
        public Dictionary<string, UnityEvent> EventReferencesGraph { get; } = new Dictionary<string, UnityEvent>();
        public Action AFXUpdate { get; set;}
        public Action AFXFixedUpdate { get; set; }
        public Action AFXLateUpdate { get; set; }
        public Action AFXStart { get; set; }
        public Action AFXOnEnable { get; set; }
        public Action AFXOnDisable { get; set; }

        [SerializeField]
        public Dictionary<string, NodePort> RegisteredObjects { get; } = new Dictionary<string, NodePort>();
        public Action RegisteredObjectAdded { get; set;}
        public Action RegisteredObjectRemoved { get; set;}

        public override NodeGraph Copy()
        {
            // Instantiate a new nodegraph instance
            AFXGraph graph = Instantiate(this);
            // Instantiate all nodes inside the graph
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;

                Node.graphHotfix = graph;
                Node node = Instantiate(nodes[i]);
                node.graph = graph;
                graph.nodes[i] = node;
            }

            // Redirect all connections
            for (int i = 0; i < graph.nodes.Count; i++)
            {
                if (graph.nodes[i] == null) continue;
                foreach (NodePort port in graph.nodes[i].Ports)
                {
                    port.Redirect(nodes, graph.nodes);
                }
            }
            return graph;
        }

        public override Node AddNode(Type type)
        {
            Node.graphHotfix = this;
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.graph = this;
            nodes.Add(node);
            return node;
        }
    }
}