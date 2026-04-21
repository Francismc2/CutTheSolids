using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Build Color")]
    [CreateNodeMenu(AFXMenuTree.UnityCompRendering + "Build Color")]
    public class ColorVariable : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Color colorIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float rIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float gIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float bIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float aIn;

        [SerializeField]
        [Output] private Color colorOut;
        [SerializeField]
        [Output] private float rOut;
        [SerializeField]
        [Output] private float gOut;
        [SerializeField]
        [Output] private float bOut;
        [SerializeField]
        [Output] private float aOut;

        public override object GetValue(NodePort port)
        {
            if (GetInputPort(nameof(colorIn)).IsConnected)
            {
                colorIn = GetInputValue(nameof(colorIn), colorIn);
            }

            if (GetInputPort(nameof(rIn)).IsConnected)
            {
                colorIn.r = GetInputValue(nameof(rIn), rIn);
            }

            if (GetInputPort(nameof(gIn)).IsConnected)
            {
                colorIn.g = GetInputValue(nameof(gIn), gIn);
            }

            if (GetInputPort(nameof(bIn)).IsConnected)
            {
                colorIn.b = GetInputValue(nameof(bIn), bIn);
            }

            if (GetInputPort(nameof(aIn)).IsConnected)
            {
                colorIn.a = GetInputValue(nameof(aIn), aIn);
            }

            if (port.fieldName == nameof(colorOut))
            {
                return colorIn;
            }

            if (port.fieldName == nameof(rOut))
            {
                return colorIn.r;
            }

            if (port.fieldName == nameof(gOut))
            {
                return colorIn.g;
            }

            if (port.fieldName == nameof(bOut))
            {
                return colorIn.b;
            }

            if (port.fieldName == nameof(aOut))
            {
                return colorIn.a;
            }

            return colorIn;
        }
    }
}