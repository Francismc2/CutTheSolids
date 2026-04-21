using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("WheelCollider Info")]
    [CreateNodeMenu(AFXMenuTree.WheelCollider + "WheelCollider Info")]
    public class WheelColliderInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private WheelCollider wheelCollider;

        [SerializeField]
        [Output] private float rpm;
        [SerializeField]
        [Output] private float steerAngle;
        [SerializeField]
        [Output] private float sprungMass;
        [SerializeField]
        [Output] private bool isGrouded;
        [SerializeField]
        [Output] private Vector3 groundHitPosition;
        [SerializeField]
        [Output] private Vector3 wheelPosition;
        [SerializeField]
        [Output] private Quaternion wheelRotation;

        private WheelHit wheelHit;

        public override object GetValue(NodePort port)
        {
            wheelCollider = GetInputValue(nameof(wheelCollider), wheelCollider);

            rpm = GetInputValue(nameof(rpm), rpm);
            steerAngle = GetInputValue(nameof(steerAngle), steerAngle);
            sprungMass = GetInputValue(nameof(sprungMass), sprungMass);
            isGrouded = GetInputValue(nameof(isGrouded), isGrouded);
            groundHitPosition = GetInputValue(nameof(groundHitPosition), groundHitPosition);
            wheelPosition = GetInputValue(nameof(wheelPosition), wheelPosition);
            wheelRotation = GetInputValue(nameof(wheelRotation), wheelRotation);

            if (port.fieldName == nameof(rpm))
            {
                return wheelCollider.rpm;
            }

            if (port.fieldName == nameof(steerAngle))
            {
                return wheelCollider.steerAngle;
            }

            if (port.fieldName == nameof(sprungMass))
            {
                return wheelCollider.sprungMass;
            }

            if (port.fieldName == nameof(isGrouded))
            {
                return wheelCollider.isGrounded;
            }

            if (port.fieldName == nameof(groundHitPosition))
            {
                wheelCollider.GetGroundHit(out wheelHit);
                return wheelHit.point;
            }

            wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

            if (port.fieldName == nameof(wheelPosition))
            {
                return wheelPosition;
            }

            if (port.fieldName == nameof(wheelRotation))
            {
                return wheelRotation;
            }

            return null;
        }
    }
}