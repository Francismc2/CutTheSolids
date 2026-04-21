using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get EulerAngles")]
    [CreateNodeMenu(AFXMenuTree.TransformGet + "Transform Get EulerAngles")]
    public class GetEulerAngles : AFXNode
    {        
        [SerializeField]
        [Input] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool local = true;

        [SerializeField]
        [Output] private Vector3 eulerAnglesOut;

        public override object GetValue(NodePort port)
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            local = GetInputValue(nameof(local), local);
            if (local)
            {
                return transformIn.localEulerAngles;
            }
            else
            {
                return transformIn.eulerAngles;
            }
        }
    }
}