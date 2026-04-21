using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("IsVisable")]
    [CreateNodeMenu(AFXMenuTree.ComponentRendering + "Renderer IsVisible")]
    public class RendererIsVisible : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Renderer rendererIn;

        [SerializeField]
        [Output] private Material materialOut;
        [SerializeField]
        [Output] private Material sharedMaterialOut;
        [SerializeField]
        [Output] private bool isVisible;

        public override object GetValue(NodePort port)
        {
            rendererIn = GetInputValue(nameof(rendererIn), rendererIn);
            return rendererIn.isVisible;
        }
    }
}