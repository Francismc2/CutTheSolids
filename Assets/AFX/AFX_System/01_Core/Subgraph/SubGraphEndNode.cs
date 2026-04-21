using UnityEngine;
using XNode;
using System.Collections.Generic;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Subgraph + "SubGraph End Node")]
    public class SubGraphEndNode : AFXFlowNode
    {
        private SubGraphNode parentNode;
        public SubGraphNode ParentNode { get => parentNode; set => parentNode = value; }
        public List<NodePort> DynamicNodesInput { get; } = new List<NodePort>();


        [Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private object addOutput;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            ParentNode.ContinueToNextNode();
        }
    }
}
