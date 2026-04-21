using UnityEngine;
using XNode;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
namespace Engage.AFX.v1
{
    [RequireNode(typeof(SubGraphStartNode), typeof(SubGraphEndNode))]
    [CreateAssetMenu]
    public class AFXSubGraph : AFXGraph
    {
        public SubGraphEndNode EndNode { get => endNode; set => endNode = value; }
        public SubGraphStartNode StartNode { get => startNode; set => startNode = value; }
        public AFXGraph ParentGraph { get => parentGraph; set => parentGraph = value; }

        private SubGraphStartNode startNode;
        private SubGraphEndNode endNode;
        private AFXGraph parentGraph;
    }
}