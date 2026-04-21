using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Build Quaternion")]
    [CreateNodeMenu(AFXMenuTree.Quaternion + "Build Quaternion")]
    public class QuaternionConstructor : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float xIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float yIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float zIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float wIn;

        [SerializeField]
        [Output] private Quaternion quaternionOut;

        public override object GetValue(NodePort port)
        {
            quaternionOut = new Quaternion(GetInputValue(nameof(xIn), xIn), GetInputValue(nameof(yIn), yIn), GetInputValue(nameof(zIn), zIn), GetInputValue(nameof(wIn), wIn));
            return quaternionOut;
        }
    }
}