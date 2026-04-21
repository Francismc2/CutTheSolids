using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.FloatCast + "Cast Float to String")]
    public class FloatToString : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float input;

        [SerializeField]
        [Output] private string output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return input.ToString();
        }
    }
}