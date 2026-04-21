using UnityEngine;
using XNode;
using System;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Tan")]
    public class Tan : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return Mathf.Tan(input);
        }
    }
}