using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.BoolCast + "Cast Bool to String")]
    public class BoolToString : AFXNode
    {
        [SerializeField]
        [Input] private bool input;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string trueValue = "true";
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string falseValue = "false";

        [SerializeField]
        [Output] private string output;

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