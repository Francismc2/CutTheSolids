using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set EulerAngles")]
    [CreateNodeMenu(AFXMenuTree.TransformSet + "Transform Set EulerAngles")]
    public class SetEulerAngles : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector3 vector3In;

        void SetRotations()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            vector3In = GetInputValue(nameof(vector3In), vector3In);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            InputToEuler(vector3In, transformIn, worldSpace);                     
        }

        public void InputToEuler(Vector3 input, Transform transformIN, bool worldSpace)
        {
            if (worldSpace)
            {
                transformIN.transform.eulerAngles = input;
            }
            else
            {
                transformIN.transform.localEulerAngles = input;
            }
        }        

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetRotations();
            base.ExecuteNode(exit);
        }
    }
}