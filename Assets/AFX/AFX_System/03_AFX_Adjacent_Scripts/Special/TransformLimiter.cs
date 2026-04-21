// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 20/07/22
// Author: tlynch
// Description: ENGAGE
// Engage XR
// ---------------------------------------------
// -------------------------------------------*/

using UnityEngine;

namespace Engage.AFX.v1
{
    [AddComponentMenu("AFX/Special/Transform Limiter")]
    public class TransformLimiter : MonoBehaviour 
    {
        [SerializeField]
        private Transform transformToLimit;
        [SerializeField]
        private bool onUpdate = false;
        [SerializeField]
        private bool onLateUpdate = true;

        [SerializeField]
        private bool limitPosition = false;
        [SerializeField]
        private bool limitRotation = false;
        [SerializeField]
        public bool limitScale = false;

        [Header("Limit Local Position")]
        [SerializeField]
        private Vector2 limitPosXIn;
        [SerializeField]
        private Vector2 limitPosYIn;
        [SerializeField]
        private Vector2 limitPosZIn;

        [Header("Limit Potential Angle Change")]
        [SerializeField]
        private Vector2 limitRotXIn;
        [SerializeField]
        private Vector2 limitRotYIn;
        [SerializeField]
        private Vector2 limitRotZIn;

        [Header("Limit Scale range")]
        [SerializeField]
        private Vector2 limitScaleXIn;
        [SerializeField]
        private Vector2 limitScaleYIn;
        [SerializeField]
        private Vector2 limitScaleZIn;

        private void Update()
        {
            if (onUpdate)
            {
                LimitTransform();
            }            
        }        

        void LateUpdate()
        {
            if (onLateUpdate)
            {
                LimitTransform();
            }            
        }

        private void LimitTransform()
        {
            if (transformToLimit == null)
            {
                return;
            }
            if (limitPosition)
            {
                transformToLimit.localPosition = CalulateLimitedVector3(transformToLimit.localPosition, limitPosXIn, limitPosYIn, limitPosZIn);
            }
            if (limitRotation)
            {
                transformToLimit.localRotation = CalulateLimitedVector3Rotation(transformToLimit.localEulerAngles, limitRotXIn, limitRotYIn, limitRotZIn);
            }
            if (limitScale)
            {
                transformToLimit.localScale = CalulateLimitedVector3(transformToLimit.localScale, limitScaleXIn, limitScaleYIn, limitScaleZIn);
            }
        }

        Vector3 CalulateLimitedVector3(Vector3 posIN, Vector2 limitX, Vector2 limitY, Vector2 limitZ)
        {
            Vector3 newPos = posIN;
        

            newPos.x = Mathf.Clamp(posIN.x, Mathf.Min(limitX.x, limitX.y), Mathf.Max(limitX.x, limitX.y));


            newPos.y = Mathf.Clamp(posIN.y, Mathf.Min(limitY.x, limitY.y), Mathf.Max(limitY.x, limitY.y));


            newPos.z = Mathf.Clamp(posIN.z, Mathf.Min(limitZ.x, limitZ.y), Mathf.Max(limitZ.x, limitZ.y));

            return newPos; 
        }

        Quaternion CalulateLimitedVector3Rotation(Vector3 rotIN, Vector2 limitX, Vector2 limitY, Vector2 limitZ)
        {
            Vector3 newRot = rotIN;
            if (newRot.x > 180) newRot.x -= 360;
            if (newRot.y > 180) newRot.y -= 360;
            if (newRot.z > 180) newRot.z -= 360;
            float clampMinX = Mathf.Min(limitX.x, limitX.y);
            float clampMaxX= Mathf.Max(limitX.x, limitX.y );

            float clampMinY = Mathf.Min(limitY.x, limitY.y);
            float clampMaxY = Mathf.Max(limitY.x, limitY.y);

            float clampMinZ = Mathf.Min(limitZ.x, limitZ.y);
            float clampMaxZ = Mathf.Max(limitZ.x, limitZ.y);

            newRot.x = Mathf.Clamp(newRot.x, clampMinX, clampMaxX);

            newRot.y = Mathf.Clamp(newRot.y, clampMinY, clampMaxY);

            newRot.z = Mathf.Clamp(newRot.z, clampMinZ, clampMaxZ);
            return Quaternion.Euler(newRot); 
        }        
    }
}
