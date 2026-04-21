using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("SmoothDamp")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF SmoothDamp")]
    public class SmoothDamp : AFXNode
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
                return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
            }
            if (port.fieldName == nameof(currentVelocity))
            {
                return currentVelocity;
            }
            return null;
        }
    }
}