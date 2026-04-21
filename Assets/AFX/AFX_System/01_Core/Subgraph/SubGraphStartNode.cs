using System.Collections.Generic;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Subgraph + "SubGraph Start Node")]
    public class SubGraphStartNode : AFXEventNode
    {
        private SubGraphNode parentNode;
        public SubGraphNode ParentNode { get => parentNode; set => parentNode = value; }
        public List<NodePort> DynamicNodesOutput { get; } = new List<NodePort>();

        [Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private object addInput;

        public override object GetValue(NodePort port)
        {
            foreach (NodePort dynamicPort in ParentNode.DynamicInputs)
            {
                if (port.fieldName == dynamicPort.fieldName)
                {
                    return dynamicPort.GetInputValue();
                }
            }

            return null;
        }
    }
}