using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("OnTriggerStay")]
    [CreateNodeMenu(AFXMenuTree.EventsCollision + "OnTriggerStay")]
    public class OnTriggerStay : AFXEventNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Collider collider;
        [SerializeField]
        [Output(ShowBackingValue.Never)] private Collider triggerCollider;

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
                colliderPassthrough.onTriggerStayEvent += OnTrigger;
                Graph.AFXLateUpdate -= Setup;
            }
        }

        public override object GetValue(NodePort port)
        {
            return triggerCollider;
        }

        public Collider OnTrigger(Collider collider)
        {
            triggerCollider = collider;
            ExecuteNode(exit);
            return collider;
        }

        private void OnDestroy()
        {
            if (colliderPassthrough == null) return;
            colliderPassthrough.onTriggerStayEvent -= OnTrigger;
        }
    }
}