using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterialSet + "Set Material Int")]
    public class SetMaterialInt : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Material materialIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string intPropertyName;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int intIn;

        private int propertyId = -1;

        protected override void Init()
        {
            base.Init();
            intPropertyName = GetInputValue(nameof(intPropertyName), intPropertyName);
            if (!string.IsNullOrEmpty(intPropertyName))
            {
                propertyId = Shader.PropertyToID(intPropertyName);
            }
        }

        void SetInt()
        {
            materialIn = GetInputValue(nameof(materialIn), materialIn);
            intIn = GetInputValue(nameof(intIn), intIn);
            materialIn.SetInt(propertyId, intIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetInt();
            base.ExecuteNode(exit);
        }
    }
}