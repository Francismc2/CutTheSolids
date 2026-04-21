using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.BoolCast + "Cast Bool to Int")]
    public class BoolToInt : AFXNode
    {
        [SerializeField]
        [Input] private bool input;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int trueValue = 1;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int falseValue = 0;


        [SerializeField]
        [Output] private int output;

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