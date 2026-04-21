using System.Collections.Generic;
using System.Collections;
using System;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.List + "List Remove Item")]
    public class ListRemove : AFXActiveNode
    {
        private const string portNameIn1 = "List";
        private const string portNameIn2 = "ItemToRemove";

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            var list = (IList)GetInputPort(portNameIn1).GetInputValue();
            list.Remove(GetInputPort(portNameIn2).GetInputValue());

            base.ExecuteNode(exit);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName != portNameIn1) return;

            var inputType = to.Connection.ValueType;
            Type itemType = inputType.GetGenericArguments()[0];
            if (HasPort(portNameIn2))
            {
                RemoveDynamicPort(portNameIn2);
            }

            AddDynamicInput(itemType, fieldName: portNameIn2);
        }
    }
}