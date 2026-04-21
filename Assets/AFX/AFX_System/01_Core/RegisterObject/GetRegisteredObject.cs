using UnityEngine;
using XNode;
using System.Collections.Generic;

namespace Engage.AFX.v1
{
    [CreateNodeMenu("Get Registered Object")]
    public class GetRegisteredObject : AFXNode, IDelayUntilReady
    {
        private const string PortName = "Output";

        private NodePort registeredPort;
        private NodePort currentOutputPort;

        [HideInInspector]
        [SerializeField] private int selectedIndex;
        [HideInInspector]
        [SerializeField] private string selectedKey;

        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }
        public string SelectedKey { get => selectedKey; set => selectedKey = value; }

        public override object GetValue(NodePort port)
        {
            if (Graph.RegisteredObjects.Count == 0) return null;

            return Graph.RegisteredObjects.GetValueOrDefault(SelectedKey).GetInputValue();
        }

        private void ResetNode()
        {
            ClearDynamicPorts();
            selectedKey = null;
            registeredPort = null;
        }

        public void SetupPorts()
        {
            if (string.IsNullOrEmpty(SelectedKey) || Graph.RegisteredObjects.Count == 0)
            {
                return;
            }

            SwapOutput();
        }

        public void CleanUpPorts()
        {
            if (string.IsNullOrEmpty(SelectedKey))
            {
                ResetNode();
                return;
            }

            if (!this.Graph.RegisteredObjects.ContainsKey(SelectedKey))
            {
                ResetNode();
            }
        }

        private void LookUpValue()
        {
            Graph.RegisteredObjects.TryGetValue(SelectedKey, out registeredPort);
        }

        private void SwapOutput()
        {
            LookUpValue();
            if (registeredPort == null) return;
            if (!registeredPort.IsConnected) return;

            currentOutputPort = GetOutputPort(PortName);
            if (currentOutputPort == null || currentOutputPort.ValueType != registeredPort.Connection.ValueType)
            {
                this.SwapDynamicOutputPortWithNewType(PortName, registeredPort.Connection.ValueType);
            }

        }

        public bool IsValueReady()
        {
            LookUpValue();
            if (registeredPort != null) return true;

            Error = $"[{this.name}] Failed on Key: " + SelectedKey;

            return false;
        }
    }
}