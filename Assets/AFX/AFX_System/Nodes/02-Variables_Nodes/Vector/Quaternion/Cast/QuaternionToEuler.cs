using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.QuaternionNodes.v1
{
    [CreateNodeMenu(AFXMenuTree.QuaternionCast + "Quaternion to Euler")]
    public class QuaternionToEuler : AFXNode
    {
        [SerializeField] [Input] private Quaternion quaternion;

        [SerializeField] [Output] private Vector3 eulerAngles;

        public override object GetValue(NodePort port)
        {
            quaternion = GetInputValue(nameof(quaternion), quaternion);
            eulerAngles = quaternion.eulerAngles;
            return eulerAngles;
        }
    }
}