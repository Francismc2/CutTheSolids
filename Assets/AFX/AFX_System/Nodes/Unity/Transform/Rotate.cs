using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform Rotate")]
    public class Rotate : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 vector3In;

        void RotateObject()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            vector3In = GetInputValue(nameof(vector3In), vector3In);
            if (worldSpace)
            {
                transformIn.Rotate(vector3In, Space.World);
            }
            else
            {
                transformIn.Rotate(vector3In, Space.Self);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            RotateObject();
            base.ExecuteNode(exit);
        }
    }
}