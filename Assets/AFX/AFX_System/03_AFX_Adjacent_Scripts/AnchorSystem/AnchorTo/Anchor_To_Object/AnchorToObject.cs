using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    public abstract class AnchorToObject : AnchorTo
    {
        [SerializeField] protected AnchorSocketCollection socketColection;

        public override void AddAnchor(Anchor anchor, IAnchorSocket socket)
        {
            if (socket.CurrentAnchors.Contains(anchor)) return;

            anchor.SetSocket(socket);
            if (anchor.Socket == null) return;

            socket.CurrentAnchors.Add(anchor);

            attachEvent?.Invoke();
        }

        public override void RemoveAnchor(Anchor child)
        {
            child.Socket.CurrentAnchors.Remove(child);
            child.ClearSocket();

            detachEvent?.Invoke();
        }
    }
}