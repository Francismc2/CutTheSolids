using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("LayerToName")]
    [CreateNodeMenu(AFXMenuTree.LayerMask + "LayerMask LayerToName")]
    public class LayerMaskLayerToName : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int layer;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private string output;

        public override object GetValue(NodePort port)
        {
            layer = GetInputValue(nameof(layer), layer);
            return LayerMask.LayerToName(layer);
        }
    }
}