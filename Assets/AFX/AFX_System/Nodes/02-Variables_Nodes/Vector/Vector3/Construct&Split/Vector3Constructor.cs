using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Build Vector3")]
    [CreateNodeMenu(AFXMenuTree.Vector3 + "Build Vector3")]
    public class Vector3Constructor : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float xIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float yIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float zIn;

        [SerializeField]
        [Output] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            output = new Vector3(GetInputValue(nameof(xIn), xIn), GetInputValue(nameof(yIn), yIn), GetInputValue(nameof(zIn), zIn));
            return output;
        }
    }
}