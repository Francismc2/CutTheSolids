using UnityEngine;
using XNode;
using System;
namespace Engage.AFX.v1
{
    [NodeTitle("Set ConnectedMassScale")]
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBodyJoint + "Set Joint ConnectedMassScale")]
    public class SetJointConnectedMassScale : AFXActiveNode
    {
        private enum TypeOfInput
        {
            Joint,
            CharacterJoint,
            HingeJoint,
            SpringJoint,
            FixedJoint,
            ConfigurableJoint
        }

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float massScale;

        private string portNameIn1 = "Joint";

        protected override void Init()
        {
            AddDynamicInput(typeof(Joint), connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, fieldName: portNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            Joint joint = (Joint)GetInputPort(portNameIn1).GetInputValue();
            massScale = GetInputValue(nameof(massScale), massScale);
            joint.connectedMassScale = massScale;
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
            if (to.Connection.ValueType == typeof(CharacterJoint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(CharacterJoint));
                return;
            }
            if (to.Connection.ValueType == typeof(HingeJoint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(HingeJoint));
                return;
            }
            if (to.Connection.ValueType == typeof(SpringJoint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(SpringJoint));
                return;
            }
            if (to.Connection.ValueType == typeof(FixedJoint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(FixedJoint));
                return;
            }
            if (to.Connection.ValueType == typeof(ConfigurableJoint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(ConfigurableJoint));
                return;
            }
            Debug.Log($"[{this.name}]: Input Type not supported by this node. Valid Types are:");
            foreach (string name in Enum.GetNames(typeof(TypeOfInput)))
            {
                Debug.Log($"[{this.name}]: " + name);
            }
        }
    }
}