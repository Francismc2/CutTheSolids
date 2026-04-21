using UnityEngine;
using UnityEngine.Animations;
using XNode;
using System;
namespace Engage.AFX.v1
{
    [NodeTitle("Constraint AddSource")]
    [CreateNodeMenu(AFXMenuTree.ComponentConstraint + "Constraint AddSource")]
    public class ConstraintAddSource : AFXActiveNode
    {
        private enum TypeOfInput
        {
            ParentConstraint,
            AimConstraint,
            LookAtConstraint,
            PositionConstraint,
            RotationConstraint,
            ScaleConstraint
        }

        [SerializeField]
        [Input] private Transform transform;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float weight;

        private string portNameIn1 = "Constraint";

        protected override void Init()
        {
            AddDynamicInput(typeof(ParentConstraint), connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, fieldName: portNameIn1);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            IConstraint constraint = (IConstraint)GetInputPort(portNameIn1).GetInputValue();
            transform = GetInputValue(nameof(transform), transform);
            weight = GetInputValue(nameof(weight), weight);
            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = transform;
            source.weight = weight;

            constraint.AddSource(source);
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
            if (to.Connection.ValueType == typeof(ParentConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(ParentConstraint));
                return;
            }
            if (to.Connection.ValueType == typeof(AimConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(AimConstraint));
                return;
            }
            if (to.Connection.ValueType == typeof(LookAtConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(LookAtConstraint));
                return;
            }
            if (to.Connection.ValueType == typeof(PositionConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(PositionConstraint));
                return;
            }
            if (to.Connection.ValueType == typeof(RotationConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(RotationConstraint));
                return;
            }
            if (to.Connection.ValueType == typeof(ScaleConstraint))
            {
                this.SwapDynamicInputPortWithNewType(portNameIn1, typeof(ScaleConstraint));
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