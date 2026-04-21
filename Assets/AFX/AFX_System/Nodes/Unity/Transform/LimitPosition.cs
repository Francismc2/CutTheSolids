using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform Limit Position")]
    public class LimitPosition : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Transform transformIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool worldSpace = false;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector2 limitXIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector2 limitYIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private Vector2 limitZIn;

        void LimitPositions()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            limitXIn = GetInputValue(nameof(limitXIn), limitXIn);
            limitYIn = GetInputValue(nameof(limitYIn), limitYIn);
            limitZIn = GetInputValue(nameof(limitZIn), limitZIn);
            if (worldSpace)
            {
                transformIn.position = CalculateLimitedPositon(transformIn.position, limitXIn, limitYIn, limitZIn);
            }
            else
            {
                transformIn.localPosition = CalculateLimitedPositon(transformIn.localPosition, limitXIn, limitYIn, limitZIn);
            }
        }

        Vector3 CalculateLimitedPositon(Vector3 posIN, Vector2 limitX, Vector2 limitY, Vector2 limitZ)
        {
            Vector3 newPos = posIN;
            newPos.x = Mathf.Clamp(posIN.x, Mathf.Min(limitX.x, limitX.y), Mathf.Max(limitX.x, limitX.y));
            newPos.y = Mathf.Clamp(posIN.y, Mathf.Min(limitY.x, limitY.y), Mathf.Max(limitY.x, limitY.y));
            newPos.z = Mathf.Clamp(posIN.z, Mathf.Min(limitZ.x, limitZ.y), Mathf.Max(limitZ.x, limitZ.y));
            return newPos;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LimitPositions();
            base.ExecuteNode(exit);
        }
    }
}