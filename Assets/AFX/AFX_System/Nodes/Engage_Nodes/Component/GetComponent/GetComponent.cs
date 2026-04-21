using System;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Component + "GetComponent")]
    public class GetComponent : AFXActiveNode
    {
        private const string PortNameIn = "Type Example";
        private const string PortNameOut = "Component";

        private NodePort typeExamplePort;
        private NodePort outputPort;

        private Type type;
        private object component;

        [SerializeField][Input(ShowBackingValue.Never)] private GameObject gameObject;

        protected override void Init()
        {
            base.Init();
            typeExamplePort = this.InitPort<object>(PortNameIn);
        }

        public override object GetValue(NodePort port)
        {
            return component;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            component = null;
            gameObject = GetInputValue(nameof(gameObject), gameObject);

            outputPort = GetOutputPort(PortNameOut);
            if (outputPort == null)
            {
                Error = "No Output";
                return;
            }
            
            type = outputPort.ValueType;

            if (!ComponentsSafeToGet.IsSafeComponentType(type))
            {
                Error = "Unsupported Types";
                component = null;
                return;
            }

            component = gameObject.GetComponent(type);
            base.ExecuteNode(exit);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            Error = null;
            type = null;
            component = null;

            if (to.fieldName != PortNameIn) return;

            SetupPorts();
        }

        private void SetupPorts()
        {
            if (!typeExamplePort.IsConnected) return;

            type = typeExamplePort.Connection.ValueType;

            Error = null;

            if (!ComponentsSafeToGet.IsSafeComponentType(type))
            {
                Error = "Unsupported Types";
                return;
            }

            outputPort = this.SwapDynamicOutputPortWithNewType(PortNameOut, type);
        }
    }
}