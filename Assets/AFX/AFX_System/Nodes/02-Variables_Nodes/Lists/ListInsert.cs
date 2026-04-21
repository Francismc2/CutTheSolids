using System.Collections.Generic;
using System.Collections;
using System;
using XNode;
using UnityEngine;

namespace Engage.AFX.v1
{
    [NodeTitle("Insert")]
    [CreateNodeMenu(AFXMenuTree.List + "List Insert")]
    public class ListInsert : AFXActiveNode
    {
        private const string PortNameIn1 = "List";
        private const string PortNameIn2 = "ItemToInsert";

        [SerializeField][Input(ShowBackingValue.Unconnected)] private int index;

        protected override void Init()
        {
            this.InitPort<List<object>>(PortNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Error = null;
            var list = (IList)GetInputPort(PortNameIn1).GetInputValue();
            index = GetInputValue(nameof(index), index);

            if (index < 0 || index > list.Count - 1)
            {
                Error = $"Index out of range: [{index}]";
                return;
            }

            list.Insert(index, GetInputPort(PortNameIn2).GetInputValue());

            base.ExecuteNode(exit);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName != PortNameIn1) return;

            var inputType = to.Connection.ValueType;
            Type itemType = inputType.GetGenericArguments()[0];
            this.SwapDynamicInputPortWithNewType(PortNameIn2, itemType);
        }
    }
}