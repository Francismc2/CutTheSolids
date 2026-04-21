using UnityEngine;
using XNode;
using System.Collections.Generic;
using System.Collections;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.List + "List Count")]
    public class ListCount : AFXNode
    {
        [SerializeField]
        [Output] private int count;

        private const string portNameIn1 = "List";

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn1);
        }

        public override object GetValue(NodePort port)
        {
            var list = (IList)GetInputPort(portNameIn1).GetInputValue();

            return list.Count;
        }
    }
}