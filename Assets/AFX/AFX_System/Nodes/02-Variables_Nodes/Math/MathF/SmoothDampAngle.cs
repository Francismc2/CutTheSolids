using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SmoothDampAngle")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF SmoothDampAngle")]
    public class SmoothDampAngle : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float current;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float target;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float smoothTime;
        [SerializeField]
        [Output] private float output;
        [SerializeField]
        [Output] private float currentVelocity;

        public override object GetValue(NodePort port)
        {
            current = GetInputValue(nameof(current), current);
            target = GetInputValue(nameof(target), target);
            smoothTime = GetInputValue(nameof(smoothTime), smoothTime);
            if (port.fieldName == nameof(output))
            {
                return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime);
            }
            if (port.fieldName == nameof(currentVelocity))
            {
                return currentVelocity;
            }
            return null;
        }
    }
}