using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Debug + "Debug Log")]
    public class DebugLog : AFXActiveNode
    {
        private const string portName = "Input";
        private NodePort inputPort;

        protected override void Init()
        {
            inputPort = this.InitPort<object>(portName);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Debug.Log(inputPort.GetInputValue());

            base.ExecuteNode(afxFlow);
        }
    }
}