using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF Cos")]
    public class Cos : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float input;
        
        [SerializeField]
        [Output(ShowBackingValue.Never)] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return Mathf.Cos(input);
        }
    }
}