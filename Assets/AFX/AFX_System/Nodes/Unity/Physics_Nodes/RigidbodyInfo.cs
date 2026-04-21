using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.PhysicsRigidBody + "Rigidbody Info")]
    public class RigidbodyInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Rigidbody rigidbodyIn;

        [SerializeField]
        [Output] private bool useGravityOut;
        [SerializeField]
        [Output] private bool isKinematicOut;
        [SerializeField]
        [Output] private bool freezeRotationOut;
        [SerializeField]
        [Output] private bool isSleepingOut;
        [SerializeField]
        [Output] private float massOut;
        [SerializeField]
        [Output] private float dragOut;
        [SerializeField]
        [Output] private float angularDragOut;
        [SerializeField]
        [Output] private Vector3 centerOfMassOut;
        [SerializeField]
        [Output] private Vector3 worldCenterOfMassOut;
        [SerializeField]
        [Output] private Vector3 positionOut;
        [SerializeField]
        [Output] private Quaternion rotationOut;
        [SerializeField]
        [Output] private Vector3 velocityOut;
        [SerializeField]
        [Output] private Vector3 angularVelocityOut;
        [SerializeField]
        [Output] private float speed;

        public override object GetValue(NodePort port)
        {
            rigidbodyIn = GetInputValue(nameof(rigidbodyIn), rigidbodyIn);
            if (port.fieldName == nameof(useGravityOut))
            {
                return rigidbodyIn.useGravity; 
            }
            if (port.fieldName == nameof(isKinematicOut))
            {
                return rigidbodyIn.isKinematic;
            }
            if (port.fieldName == nameof(massOut))
            {
                return rigidbodyIn.mass; 
            }
            if (port.fieldName == nameof(dragOut))
            {
                return rigidbodyIn.drag; 
            }
            if (port.fieldName == nameof(angularDragOut))
            {
                return rigidbodyIn.angularDrag;
            }
            if (port.fieldName == nameof(centerOfMassOut))
            {
                return rigidbodyIn.centerOfMass;
            }
            if (port.fieldName == nameof(worldCenterOfMassOut))
            {
                return rigidbodyIn.worldCenterOfMass;
            }
            if (port.fieldName == nameof(freezeRotationOut))
            {
                return rigidbodyIn.freezeRotation;
            }
            if (port.fieldName == nameof(positionOut))
            {
                return rigidbodyIn.position;
            }
            if (port.fieldName == nameof(rotationOut))
            {
                return rigidbodyIn.rotation;
            }
            if (port.fieldName == nameof(isSleepingOut))
            {
                return rigidbodyIn.IsSleeping();
            }
            if (port.fieldName == nameof(angularVelocityOut))
            {
                return rigidbodyIn.angularVelocity;
            }
            if (port.fieldName == nameof(velocityOut))
            {
                return rigidbodyIn.velocity;
            }
            if (port.fieldName == nameof(speed))
            {
                return rigidbodyIn.velocity.magnitude;
            }
            return null;
        }
    }
}