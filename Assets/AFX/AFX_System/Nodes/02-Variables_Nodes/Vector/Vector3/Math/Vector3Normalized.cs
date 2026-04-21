using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Vector3 Normalized")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Normalized")]
    public class Vector3Normalized : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 input;

        [SerializeField]
        [Output] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return input.normalized;
        } 
    }
}