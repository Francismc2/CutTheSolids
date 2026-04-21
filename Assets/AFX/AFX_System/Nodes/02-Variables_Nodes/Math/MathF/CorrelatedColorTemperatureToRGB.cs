using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("CorrelatedColorTemperatureToRGB")]
    [CreateNodeMenu(AFXMenuTree.MathF + "MathF CorrelatedColorTemperatureToRGB")]
    public class CorrelatedColorTemperatureToRGB : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float kelvin;
        
        [SerializeField][Output] private Color output;

        public override object GetValue(NodePort port)
        {
            kelvin = GetInputValue(nameof(kelvin), kelvin);

            return Mathf.CorrelatedColorTemperatureToRGB(kelvin);
        }
    }
}