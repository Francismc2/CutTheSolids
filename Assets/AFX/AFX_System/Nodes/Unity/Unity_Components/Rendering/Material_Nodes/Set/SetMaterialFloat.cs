using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterialSet + "Set Material Float")]
    public class SetMaterialFloat : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Material materialIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string floatPropertyName;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float floatIn;

        private int propertyId = -1;

        protected override void Init()
        {
            base.Init();
            floatPropertyName = GetInputValue(nameof(floatPropertyName), floatPropertyName);
            if (!string.IsNullOrEmpty(floatPropertyName))
            {
                propertyId = Shader.PropertyToID(floatPropertyName);
            }
        }

        void SetFloat()
        {
            materialIn = GetInputValue(nameof(materialIn), materialIn);
            floatIn = GetInputValue(nameof(floatIn), floatIn);
            materialIn.SetFloat(propertyId, floatIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetFloat();
            base.ExecuteNode(exit);
        }
    }
}