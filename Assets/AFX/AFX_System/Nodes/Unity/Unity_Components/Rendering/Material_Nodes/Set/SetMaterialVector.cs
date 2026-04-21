using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.ComponentRenderingMaterialSet + "Set Material Vector")]
    public class SetMaterialVector : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Material materialIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string vectorPropertyName;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector4 vectorIn;

        private int propertyId = -1;

        protected override void Init()
        {
            base.Init();
            vectorPropertyName = GetInputValue(nameof(vectorPropertyName), vectorPropertyName);
            if (!string.IsNullOrEmpty(vectorPropertyName))
            {
                propertyId = Shader.PropertyToID(vectorPropertyName);
            }
        }

        void SetVector()
        {
            materialIn = GetInputValue(nameof(materialIn), materialIn);
            vectorIn = GetInputValue(nameof(vectorIn), vectorIn);
            materialIn.SetVector(propertyId, vectorIn);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetVector();
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return exit;
        }
    }
}