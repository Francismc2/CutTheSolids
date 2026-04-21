using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Sin")]
    public class Sin : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return Mathf.Sin(input);
        }
    }
}