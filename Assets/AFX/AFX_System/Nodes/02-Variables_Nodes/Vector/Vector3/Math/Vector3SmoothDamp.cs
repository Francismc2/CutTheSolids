using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SmoothDamp")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 SmoothDamp")]
    public class Vector3SmoothDamp : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 current;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 target;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float smoothTime;
        [SerializeField]
        [Output] private Vector3 currentVelocity;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            current = GetInputValue(nameof(current), current);
            target = GetInputValue(nameof(target), target);
            smoothTime = GetInputValue(nameof(smoothTime), smoothTime);
            if (port.fieldName == nameof(output))
            {
                return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime);
            }
            if (port.fieldName == nameof(currentVelocity))
            {
                return currentVelocity;
            }
            return null;
        }
    }
}
