using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.EngagePhysics + "Physics Tracker")]
    public class AFXPhysicsTracker : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private Transform transform;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float timeSlice;

        [SerializeField][Output(ShowBackingValue.Never)] private float speed;
        [SerializeField][Output(ShowBackingValue.Never)] private float accelerationStrength;
        [SerializeField][Output(ShowBackingValue.Never)] private float angularAccelerationStrength;
        [SerializeField][Output(ShowBackingValue.Never)] private float angularSpeed;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 direction;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 velocity;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 acceleration;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 angularAxis;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 angularVelocity;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 angularAcceleration;

        private PhysicsTracker physicsTracker = new();

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            transform = GetInputValue(nameof(transform), transform);
            timeSlice = GetInputValue(nameof(timeSlice), timeSlice);


            physicsTracker.Update(transform.transform.position, transform.transform.rotation, timeSlice);

            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(speed))
            {
                return physicsTracker.Speed;
            }

            if (port.fieldName == nameof(accelerationStrength))
            {
                return physicsTracker.AccelerationStrength;
            }

            if (port.fieldName == nameof(direction))
            {
                return physicsTracker.Direction;
            }

            if (port.fieldName == nameof(velocity))
            {
                return physicsTracker.Velocity;
            }

            if (port.fieldName == nameof(acceleration))
            {
                return physicsTracker.Acceleration;
            }

            if (port.fieldName == nameof(angularSpeed))
            {
                return physicsTracker.AngularSpeed;
            }

            if (port.fieldName == nameof(angularAxis))
            {
                return physicsTracker.AngularAxis;
            }

            if (port.fieldName == nameof(angularVelocity))
            {
                return physicsTracker.AngularVelocity;
            }

            if (port.fieldName == nameof(angularAccelerationStrength))
            {
                return physicsTracker.AngularAccelerationStrength;
            }

            if (port.fieldName == nameof(angularAcceleration))
            {
                return physicsTracker.AngularAcceleration;
            }

            return null;
        }
    }
}