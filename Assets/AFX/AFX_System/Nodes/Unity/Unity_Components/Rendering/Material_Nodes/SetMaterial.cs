using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterial + "Set Material")]
    public class SetMaterial : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Renderer rendererIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Material material;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool sharedMaterial = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int materialIndex = 0;

        private void SwapMaterials()
        {
            rendererIn = GetInputValue(nameof(rendererIn), rendererIn);
            material = GetInputValue(nameof(material), material);
            sharedMaterial = GetInputValue(nameof(sharedMaterial), sharedMaterial);
            materialIndex = GetInputValue(nameof(materialIndex), materialIndex);

            var materials = sharedMaterial ? rendererIn.sharedMaterials : rendererIn.materials;
            materialIndex = Mathf.Clamp(materialIndex, 0, materials.Length - 1);
            materials[materialIndex] = material;

            if (sharedMaterial)
            {
                rendererIn.sharedMaterials = materials;
            }
            else
            {
                rendererIn.materials = materials;
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SwapMaterials();
            base.ExecuteNode(exit);
        }
    }
}