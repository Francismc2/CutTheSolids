using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Magnitude")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 Magnitude")]
    public class Vector3Magnitude : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 input;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return input.magnitude;
        } 
    }
}