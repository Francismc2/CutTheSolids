using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.TransformGet + "Get Tracked Rotation")]
    public class GetTrackedRotation : AFXActiveNode
    {
        private enum Axis { X, Y, Z }

        [SerializeField][Input(ShowBackingValue.Never)] private Transform transformIn;

        [SerializeField][Output] private float rotationDegreeCount;

        [SerializeField][NodeEnum] private Axis axis = Axis.X;

        private float delta;
        private Vector3 lastRotation;

        private Vector3 baseAxis;
        private Vector3 baseFacing;

        protected override void Init()
        {
            base.Init();

            baseAxis = axis switch
            {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
            };

            baseFacing = axis switch
            {
                Axis.X => Vector3.forward,
                Axis.Y => Vector3.right,
                Axis.Z => Vector3.up,
            };
        }

        public override object GetValue(NodePort port)
        {
            return rotationDegreeCount;
        }

        void GetTrackedRotations()
        {
            float output = rotationDegreeCount;
            transformIn = GetInputValue(nameof(transformIn), transformIn);

            var axis = transformIn.localRotation * baseAxis;
            var facing = transformIn.localRotation * baseFacing;

            delta = Vector3.SignedAngle(Vector3.ProjectOnPlane(lastRotation, axis), facing, axis);
            lastRotation = facing;

            output += delta;
            rotationDegreeCount = output;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            GetTrackedRotations();
            base.ExecuteNode(exit);
        }
    }
}