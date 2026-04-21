using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SqrMagnitude")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 SqrMagnitude")]
    public class Vector3SqrMagnitude : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 input;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            return input.sqrMagnitude;
        } 
    }
}