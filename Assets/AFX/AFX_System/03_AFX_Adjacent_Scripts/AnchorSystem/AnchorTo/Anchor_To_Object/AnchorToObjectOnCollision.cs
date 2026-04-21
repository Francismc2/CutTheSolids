using Engage.AFX.v1;
using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    [AddComponentMenu("AFX/Anchor/Anchor To Object - Collision")]
    public class AnchorToObjectOnCollision : AnchorToObject
    {
        private Collision lastCollision;
        private AFXColliderEventPassthrough colliderPassthrough;

        private void Awake()
        {
            if (lastCollision == null) colliderPassthrough = anchor.ChildTransform.gameObject.AddComponent<AFXColliderEventPassthrough>();
        }

        private void OnEnable()
        {
            colliderPassthrough.onCollisionEnterEvent += CollisionEnter;
            colliderPassthrough.onCollisionExitEvent += CollisionExit;
        }

        private void OnDisable()
        {
            colliderPassthrough.onCollisionEnterEvent -= CollisionEnter;
            colliderPassthrough.onCollisionExitEvent -= CollisionExit;
        }

        private Collision CollisionEnter(Collision collision)
        {
            lastCollision = collision;
            AttachCheck();
            return lastCollision;
        }

        private Collision CollisionExit(Collision collision)
        {
            lastCollision = collision;
            DetachCheck();
            return lastCollision;
        }

        protected override void AttachCheck()
        {
            foreach (AnchorSocket socket in socketColection.SocketList)
            {
                if (socket.CurrentAnchors.Count >= socket.MaximumConnections) break;
                if (anchor == null) continue;
                if (anchor.Socket != null) continue;
                if (lastCollision.collider.gameObject != socket.gameObject) continue;

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
                if (lastCollision.collider.gameObject != anchor.Socket.SocketTransform.gameObject) continue;

                RemoveAnchor(anchor);
                break;
            }
        }
    }
}