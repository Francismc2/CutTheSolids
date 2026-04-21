using Engage.AFX.v1;
using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    [AddComponentMenu("AFX/Anchor/Anchor To Object - Trigger")]
    public class AnchorToObjectOnTrigger : AnchorToObject
    {
        private Collider lastCollider;
        private AFXColliderEventPassthrough colliderPassthrough;

        private void Awake()
        {
            if (colliderPassthrough == null) colliderPassthrough = anchor.ChildTransform.gameObject.AddComponent<AFXColliderEventPassthrough>();
        }

        private void OnEnable()
        {
            colliderPassthrough.onTriggerEnterEvent += TriggerEnter;
            colliderPassthrough.onTriggerExitEvent += TriggerExit;
        }

        private void OnDisable()
        {
            colliderPassthrough.onTriggerEnterEvent -= TriggerEnter;
            colliderPassthrough.onTriggerExitEvent -= TriggerExit;
        }

        private Collider TriggerEnter(Collider other)
        {
            lastCollider = other;
            AttachCheck();
            return lastCollider;
        }

        private Collider TriggerExit(Collider other)
        {
            lastCollider = other;
            DetachCheck();
            return lastCollider;
        }

        protected override void AttachCheck()
        {
            foreach (AnchorSocket socket in socketColection.SocketList)
            {
                if (socket.CurrentAnchors.Count >= socket.MaximumConnections) break;
                if (anchor == null) continue;
                if (anchor.Socket != null) continue;
                if (lastCollider.gameObject != socket.gameObject) continue;

                AddAnchor(anchor, socket);
                break;
            }
        }

        protected override void DetachCheck()
        {
            if (anchor.Socket == null) return;
            if (anchor.Socket.LockConnection) return;
            if (anchor.Socket.CurrentAnchors.Count == 0) return;

            foreach (var anchor in anchor.Socket.CurrentAnchors)
            {
                if (lastCollider.gameObject != anchor.gameObject) continue;

                RemoveAnchor(anchor);
                break;
            }
        }
    }
}