using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.FloatLogic + "Logic Float Changed")]
    public class LogicFloatChanged : AFXNode
    {
        [SerializeField][Input] private float input;

        [SerializeField][Output] private bool output = false;

        private float oldValue;
        private bool firstRun = true;

        protected override void Init()
        {
            base.Init();
            input = GetInputValue(nameof(input), input);
            oldValue = input;
        }

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