using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [NodeTitle("Euler")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Quaternion Euler")]
    public class QuaternionEuler : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 euler;

        [SerializeField] [Output(ShowBackingValue.Never)] private Quaternion quaternion;

        public override object GetValue(NodePort port)
        {
            euler = GetInputValue(nameof(euler), euler);
            quaternion = Quaternion.Euler(euler);
            return quaternion;
        }
    }
}