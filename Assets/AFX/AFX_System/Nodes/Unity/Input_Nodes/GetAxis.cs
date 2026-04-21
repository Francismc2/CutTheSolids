using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("GetAxis")]
    [CreateNodeMenu(AFXMenuTree.Input + "Input GetAxis")]
    public class GetAxis : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string axisName;

        [SerializeField]
        [Output] private float floatOut;

        public override object GetValue(NodePort port)
        {
            axisName = GetInputValue(nameof(axisName), axisName);

            return Input.GetAxis(axisName);
        }
    }
}