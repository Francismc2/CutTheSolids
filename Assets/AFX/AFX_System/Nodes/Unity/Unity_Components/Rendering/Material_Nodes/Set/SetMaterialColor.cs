using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterialSet + "Set Material Color")]
    public class SetMaterialColor : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Material materialIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string colorPropertyName = "_Color";
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Color colorIn;

        private int propertyId = -1;

        protected override void Init()
        {
            base.Init();
            colorPropertyName = GetInputValue(nameof(colorPropertyName), colorPropertyName);
            if (!string.IsNullOrEmpty(colorPropertyName))
            {
                propertyId = Shader.PropertyToID(colorPropertyName);
            }
        }

        void SetColor()
        {
            materialIn = GetInputValue(nameof(materialIn), materialIn);
            colorIn = GetInputValue(nameof(colorIn), colorIn);
            materialIn.SetColor(propertyId, colorIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetColor();
            base.ExecuteNode(exit);
        }
    }
}