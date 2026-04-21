using UnityEngine;
using UnityEngine.Animations;
using XNode;
using System;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentConstraint + "Constraint Info")]
    public class ConstraintInfo : AFXNode
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
        [Output] private bool constraintActive;
        [SerializeField]
        [Output] private float weight;
        [SerializeField]
        [Output] private bool locked;
        [SerializeField]
        [Output] private int sourceCount;

        private string portNameIn1 = "Constraint";

        protected override void Init()
        {
            AddDynamicInput(typeof(ParentConstraint), connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, fieldName: portNameIn1);
        }

        public override object GetValue(NodePort port)
        {
            IConstraint constraint = (IConstraint)GetInputPort(portNameIn1).GetInputValue();
            if (constraint == null) return null;

            if (port.fieldName == nameof(constraintActive))
            {
                return constraint.constraintActive;
            }
            if (port.fieldName == nameof(weight))
            {
                return constraint.weight;
            }
            if (port.fieldName == nameof(locked))
            {
                return constraint.locked;
            }
            if (port.fieldName == nameof(sourceCount))
            {
                return constraint.sourceCount;
            }
            return null;
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