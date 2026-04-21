using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("NameToLayer")]
    [CreateNodeMenu(AFXMenuTree.LayerMask + "LayerMask NameToLayer")]
    public class LayerMaskNameToLayer : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string layerName;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private int output;

        public override object GetValue(NodePort port)
        {
            layerName = GetInputValue(nameof(layerName), layerName);
            return LayerMask.NameToLayer(layerName);
        }
    }
}