using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.BoolLogic + "Logic Bool Changed")]
    public class LogicBoolChanged : AFXNode
    {
        [SerializeField]
        [Input] private bool input;

        [SerializeField]
        [Output] private bool output = false;

        private bool oldValue;
        private bool firstRun = true;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            if (firstRun)
            {
                oldValue = input;
                firstRun = false;
                return false;
            }

            if (oldValue != input)
            {
                oldValue = input;
                return true;
            }

            return false;
        }
    }
}