using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Transform + "Transform Limit Rotation")]
    public class LimitRotation : AFXActiveNode
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

        void LimitRotations()
        {
            transformIn = GetInputValue(nameof(transformIn), transformIn);
            worldSpace = GetInputValue(nameof(worldSpace), worldSpace);
            limitXIn = GetInputValue(nameof(limitXIn), limitXIn);
            limitYIn = GetInputValue(nameof(limitYIn), limitYIn);
            limitZIn = GetInputValue(nameof(limitZIn), limitZIn);
            if (worldSpace)
            {
                transformIn.rotation = CalculateLimitedVector3Rotation(transformIn.eulerAngles, limitXIn, limitYIn, limitZIn);
            }
            else
            {
                transformIn.localRotation = CalculateLimitedVector3Rotation(transformIn.localEulerAngles, limitXIn, limitYIn, limitZIn);
            }
        }

        Quaternion CalculateLimitedVector3Rotation(Vector3 rotIN, Vector2 limitX, Vector2 limitY, Vector2 limitZ)
        {
            Vector3 newRot = rotIN;
            if (newRot.x > 180) newRot.x -= 360;
            if (newRot.y > 180) newRot.y -= 360;
            if (newRot.z > 180) newRot.z -= 360;
            float clampMinX = Mathf.Min(limitX.x, limitX.y);
            float clampMaxX = Mathf.Max(limitX.x, limitX.y);
            float clampMinY = Mathf.Min(limitY.x, limitY.y);
            float clampMaxY = Mathf.Max(limitY.x, limitY.y);
            float clampMinZ = Mathf.Min(limitZ.x, limitZ.y);
            float clampMaxZ = Mathf.Max(limitZ.x, limitZ.y);
            newRot.x = Mathf.Clamp(newRot.x, clampMinX, clampMaxX);
            newRot.y = Mathf.Clamp(newRot.y, clampMinY, clampMaxY);
            newRot.z = Mathf.Clamp(newRot.z, clampMinZ, clampMaxZ);
            return Quaternion.Euler(newRot);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            LimitRotations();
            base.ExecuteNode(exit);
        }
    }
}