using UnityEngine;
using XNode;
using System;
namespace Engage.AFX.v1
{
    [NodeTitle("Set AutoConfigAnchor")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJoint + "Set Joint AutoConfigureConnectedAnchor")]
    public class SetJointAutoConfigureConnectedAnchor : AFXActiveNode
    {
        private const string portNameIn1 = "Joint";

        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool autoConfigureConnectedAnchor;


        protected override void Init()
        {
            AddDynamicInput(typeof(Joint), connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, fieldName: portNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Joint joint = (Joint)GetInputPort(portNameIn1).GetInputValue();
            autoConfigureConnectedAnchor = GetInputValue(nameof(autoConfigureConnectedAnchor), autoConfigureConnectedAnchor);
            joint.autoConfigureConnectedAnchor = autoConfigureConnectedAnchor;
            base.ExecuteNode(exit);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);
            if (to.fieldName == portNameIn1)
            {
                SetupInputPorts(to);
            }
        }

        private void SetupInputPorts(NodePort to)
        {
            if (to.Connection.ValueType == typeof(Joint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(Joint));
                return;
            }

            if (to.Connection.ValueType.IsSubclassOf(typeof(Joint)))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, to.Connection.ValueType);
                return;
            }
        }
    }
}