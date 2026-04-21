using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterial + "Get Material")]
    public class GetMaterial : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Renderer rendererIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool sharedMaterial = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int materialIndex = 0;

        [SerializeField]
        [Output] private Material materialOut;

        public override object GetValue(NodePort port)
        {
            rendererIn = GetInputValue(nameof(rendererIn), rendererIn);
            sharedMaterial = GetInputValue(nameof(sharedMaterial), sharedMaterial);
            materialIndex = GetInputValue(nameof(materialIndex), materialIndex);

            if (sharedMaterial)
            {
                materialIndex = Mathf.Clamp(materialIndex, 0, rendererIn.sharedMaterials.Length - 1);
                return rendererIn.sharedMaterials[materialIndex];
            }
            else
            {
                materialIndex = Mathf.Clamp(materialIndex, 0, rendererIn.materials.Length - 1);
                return rendererIn.materials[materialIndex];
            }
        }
    }
}