using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("MoveTowards")]
    [CreateNodeMenu(AFXMenuTree.Vector3Math + "Vector3 MoveTowards")]
    public class Vector3MoveTowards : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 current;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 target;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float maxDistanceDelta;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private Vector3 output;

        public override object GetValue(NodePort port)
        {
            current = GetInputValue(nameof(current), current);
            target = GetInputValue(nameof(target), target);
            maxDistanceDelta = GetInputValue(nameof(maxDistanceDelta), maxDistanceDelta);

            return Vector3.MoveTowards(current, target, maxDistanceDelta);
        }
    }
}