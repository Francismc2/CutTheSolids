using UnityEngine;
using XNode;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.List + "List Get at Index")]
    public class ListGetAt : AFXNode
    {
        private const string PortNameIn1 = "List";
        private const string PortNameOut = "Output";

        [SerializeField][Input(ShowBackingValue.Unconnected)] private int index;

        protected override void Init()
        {
            this.InitPort<List<object>>(PortNameIn1);
        }

        public override object GetValue(NodePort port)
        {
            index = GetInputValue(nameof(index), index);
            var collection = (IList)GetInputPort(PortNameIn1).GetInputValue();

            if (index < 0 || index > collection.Count - 1)
            {
                Error = $"Index out of range: [{index}]";
                return null;
            }

            return collection[index];
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName != PortNameIn1) return;

            var inputType = to.Connection.ValueType;
            Type itemType = inputType.GetGenericArguments()[0];
            if (HasPort(PortNameOut))
            {
                RemoveDynamicPort(PortNameOut);
            }

            AddDynamicOutput(itemType, fieldName: PortNameOut);
        }
    }
}