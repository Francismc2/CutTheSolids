using UnityEngine;
using XNode;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Engage.AFX.v1
{
    [NodeTitle("ForEach")]
    [CreateNodeMenu(AFXMenuTree.Flow + "Flow ForEach")]
    public class ForEach : AFXFlowNode
    {
        private const string portNameIn = "List";
        private const string portNameOut = "Item";

        [SerializeField] [Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow exit;
        [SerializeField] [Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow body;

        private object itemOBJ;

        protected override void Init()
        {
            this.InitPort<List<object>>(portNameIn);
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == portNameOut)
            {
                return itemOBJ;
            }

            return null;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            var collection = (IList)GetInputPort(portNameIn).GetInputValue();
            foreach (var item in collection)
            {
                itemOBJ = item;
                body.ActivateNextNode(GetPort(nameof(body)));
            }

            exit.ActivateNextNode(GetPort(nameof(exit)));
        }


        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == portNameIn)
            {
                var inputType = to.Connection.ValueType;            // Get the type of list
                Type itemType = inputType.GetGenericArguments()[0]; //Get the type of item that is in the list
                if (HasPort(portNameOut))
                {
                    RemoveDynamicPort(portNameOut);
                }
                AddDynamicOutput(itemType, fieldName: portNameOut);
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(body) => body,
                nameof(exit) => exit,
                _ => null
            };
        }
    }
}