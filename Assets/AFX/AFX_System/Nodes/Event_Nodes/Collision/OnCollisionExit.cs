using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("OnCollisionExit")]
    [CreateNodeMenu(AFXMenuTree.EventsCollision + "OnCollisionExit")]
    public class OnCollisionExit : AFXEventNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Collider collider;
        [Header("---Collision Info---")]
        [SerializeField]
        [Output(ShowBackingValue.Never)] private GameObject gameObjectHit;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Rigidbody rigidbodyHit;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Collider colliderHit;


        private Collision collisionInfo;
        private AFXColliderEventPassthrough colliderPassthrough;

        protected override void Init()
        {
            Graph.AFXLateUpdate += Setup;
        }

        private void Setup()
        {
            collider = GetInputValue(nameof(collider), collider);

            if (collider.gameObject.TryGetComponent(out AFXColliderEventPassthrough passthrough))
            {
                colliderPassthrough = passthrough;
            }
            else
            {
                colliderPassthrough = collider.gameObject.AddComponent<AFXColliderEventPassthrough>();
            }

            if (colliderPassthrough != null)
            {
                colliderPassthrough.onCollisionExitEvent += OnCollision;
                Graph.AFXLateUpdate -= Setup;
            }
        }

        public override object GetValue(NodePort port)
        {
            if (collisionInfo == null) return null;

            if (port.fieldName == nameof(gameObjectHit))
            {
                return collisionInfo.gameObject;
            }

            if (port.fieldName == nameof(rigidbodyHit))
            {
                return collisionInfo.rigidbody;
            }

            if (port.fieldName == nameof(colliderHit))
            {
                return collisionInfo.collider;
            }

            return null;
        }

        public Collision OnCollision(Collision collisionIn)
        {
            collisionInfo = collisionIn;
            ExecuteNode(exit);
            return collisionInfo;
        }

        private void OnDestroy()
        {
            if (colliderPassthrough == null) return;
            colliderPassthrough.onCollisionExitEvent -= OnCollision;
        }
    }
}