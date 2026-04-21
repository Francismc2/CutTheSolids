using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform Translate")]
    public class Translate : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 vector3In;

        void TranslatePosition()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            vector3In = GetInputValue(nameof(vector3In), vector3In);
            if (worldSpace)
            {
                transformIn.Translate(vector3In, Space.World);
            }
            else
            {
                transformIn.Translate(vector3In, Space.Self);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            TranslatePosition();
            base.ExecuteNode(exit);
        }
    }
}