using System.Collections.Generic;
using System.Collections;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.List + "List Clear")]
    public class ListClear : AFXActiveNode
    {
        private const string portNameIn1 = "List";

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            var list = (IList)GetInputPort(portNameIn1).GetInputValue();
            list.Clear();

            base.ExecuteNode(exit);
        }
    }
}