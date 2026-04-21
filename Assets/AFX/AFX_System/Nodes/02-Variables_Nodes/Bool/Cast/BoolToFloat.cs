using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.BoolCast+ "Cast Bool to Float")]
    public class BoolToFloat : AFXNode
    {
        [SerializeField]
        [Input] private bool input;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float trueValue = 1;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float falseValue = 0;

        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            if (input)
            {
                trueValue = GetInputValue(nameof(trueValue), trueValue);
                return trueValue;
            }
            else
            {
                falseValue = GetInputValue(nameof(falseValue), falseValue);
                return falseValue;
            }
        }
    }
}