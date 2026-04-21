using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("MoveTowardsAngle")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF MoveTowardsAngle")]
    public class MoveTowardsAngle : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float current;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float target;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxDelta;
        
        [SerializeField]
        [Output] private float output;

        public override object GetValue(NodePort port)
        {
            current = GetInputValue(nameof(current), current);
            target = GetInputValue(nameof(target), target);
            maxDelta = GetInputValue(nameof(maxDelta), maxDelta);

            return Mathf.MoveTowardsAngle(current, target, maxDelta);
        }
    }
}