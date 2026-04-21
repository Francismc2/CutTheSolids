using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterialSet + "Set Material Texture")]
    public class SetMaterialTexture : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Material materialIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string texturePropertyName = "_MainTex";
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Texture textureIn;

        private int propertyId = -1;

        protected override void Init()
        {
            base.Init();
            texturePropertyName = GetInputValue(nameof(texturePropertyName), texturePropertyName);
            if (!string.IsNullOrEmpty(texturePropertyName))
            {
                propertyId = Shader.PropertyToID(texturePropertyName);
            }
        }

        void SetTexture()
        {
            materialIn = GetInputValue(nameof(materialIn), materialIn);
            textureIn = GetInputValue(nameof(textureIn), textureIn);
            materialIn.SetTexture(propertyId, textureIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetTexture();
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return exit;
        }
    }
}