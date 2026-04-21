using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.IntCast + "Cast Int to Float")]
    public class IntToFloat : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int input;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            return (float)input;
        }
    }
}