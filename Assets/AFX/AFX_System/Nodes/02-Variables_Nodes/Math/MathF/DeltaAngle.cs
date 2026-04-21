using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("DeltaAngle")]
    [CreateNodeMenu(AFXMenuTree.MathF + "DeltaAngle")]
    public class DeltaAngle : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float angle;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float targetAngle;
        
        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            angle = GetInputValue(nameof(angle), angle);
            targetAngle = GetInputValue(nameof(targetAngle), targetAngle);

            return Mathf.DeltaAngle(angle, targetAngle);
        }
    }
}