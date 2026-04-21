using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Raycast + "Raycast")]
    public class Raycast : AFXFlowNode
    {
        [SerializeField][Output(ShowBackingValue.Never)] private AFXFlow raycastHit;
        [SerializeField][Output(ShowBackingValue.Never)] private AFXFlow raycastMiss;


        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 origin;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 direction;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float maxDistance = Mathf.Infinity;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int layerMask = Physics.DefaultRaycastLayers;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private bool queryTriggerInteraction = true;
        
        [SerializeField][Output(ShowBackingValue.Never)] private bool hit;
        [SerializeField][Output(ShowBackingValue.Never)] private Collider colliderHit;
        [SerializeField][Output(ShowBackingValue.Never)] private float distance;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 point;
        [SerializeField][Output(ShowBackingValue.Never)] private Vector3 normal;

        private RaycastHit hitInfo;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(hit))
            {
                return hit;
            }

            if (port.fieldName == nameof(colliderHit))
            {
                return hitInfo.collider;
            }

            if (port.fieldName == nameof(distance))
            {
                return hitInfo.distance;
            }

            if (port.fieldName == nameof(point))
            {
                return hitInfo.point;
            }

            if (port.fieldName == nameof(normal))
            {
                return hitInfo.normal;
            }

            return null;
        }

        bool CastRay()
        {
            origin = GetInputValue(nameof(origin), origin);
            direction = GetInputValue(nameof(direction), direction);
            maxDistance = GetInputValue(nameof(maxDistance), maxDistance);
            layerMask = GetInputValue(nameof(layerMask), layerMask);
            queryTriggerInteraction = GetInputValue(nameof(queryTriggerInteraction), queryTriggerInteraction);

            if (queryTriggerInteraction)
            {
                return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Collide);
            }
            else
            {
                return Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Ignore);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (CastRay())
            {
                hit = true;
                raycastHit.ActivateNextNode(GetPort(nameof(raycastHit)));
            }
            else
            {
                hit = false;
                raycastMiss.ActivateNextNode(GetPort(nameof(raycastMiss)));
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(raycastHit) => raycastHit,
                nameof(raycastMiss) => raycastHit,
                _ => null
            };
        }
    }
}