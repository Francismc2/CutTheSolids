using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Variable.Vector3Nodes.v1
{
    [CreateNodeMenu(AFXMenuTree.Vector3Cast + "Euler to Quaternion")]
    public class EulerToQuaternion : AFXNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private Vector3 eulerAngles;

        [SerializeField] [Output] private Quaternion output;

        public override object GetValue(NodePort port)
        {
            eulerAngles = GetInputValue(nameof(eulerAngles), eulerAngles);
            output = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
            return output;
        }
    }
}