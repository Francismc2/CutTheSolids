using UnityEngine;
using XNode;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.List + "List Contains")]
    public class ListContains : AFXNode
    {
        [SerializeField]
        [Output] private bool boolOut;

        private const string portNameIn1 = "List";
        private const string portNameIn2 = "Object";

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn1);
        }

        public override object GetValue(NodePort port)
        {
            var list = (IList)GetInputPort(portNameIn1).GetInputValue();

            return list.Contains(GetInputPort(portNameIn2).GetInputValue());
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