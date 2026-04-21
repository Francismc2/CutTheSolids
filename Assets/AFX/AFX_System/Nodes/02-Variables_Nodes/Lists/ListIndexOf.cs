using UnityEngine;
using XNode;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Engage.AFX.v1
{
    [NodeTitle("IndexOf")]
    [CreateNodeMenu(AFXMenuTree.List + "List IndexOf")]
    public class ListIndexOf : AFXNode
    {
        [SerializeField]
        [Output] private int index;

        private const string portNameIn1 = "List";
        private const string portNameIn2 = "Object";

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn1);
        }

        public override object GetValue(NodePort port)
        {
            var list = (IList)GetInputPort(portNameIn1).GetInputValue();

            return list.IndexOf(GetInputPort(portNameIn2).GetInputValue());
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