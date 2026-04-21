using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    [AddComponentMenu("AFX/Anchor/Anchor To Object - Distance")]
    public class AnchorToObjectByDistance : AnchorToObject
    {
        [SerializeField] private float attachDistance;

        private void Update()
        {
            DetachCheck();
            AttachCheck();
        }

        protected override void AttachCheck()
        {
            foreach (AnchorSocket socket in socketColection.SocketList)
            {
                if (anchor.Socket != null) continue;
                if (socket.CurrentAnchors.Count >= socket.MaximumConnections) break;

                float distance = (anchor.ChildTransform.position - socket.SocketTransform.position).sqrMagnitude;
                if (distance > attachDistance) continue;

                AddAnchor(anchor, socket);
            }
        }

        protected override void DetachCheck()
        {
            if (anchor.Socket == null) return;
            if (anchor.Socket.LockConnection) return;

            if (anchor.Socket.CurrentAnchors.Count == 0) return;

            foreach (var anchor in anchor.Socket.CurrentAnchors)
            {
                float distance = (anchor.Socket.SocketTransform.position - anchor.ChildTransform.position).sqrMagnitude;

                if (distance < attachDistance) continue;

                RemoveAnchor(anchor);
                break;
            }
        }
    }
}