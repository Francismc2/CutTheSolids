using UnityEngine.Events;
using UnityEngine;
using Engage.AFX.v1;

namespace Engage.AFX.Anchor.v1
{
    public abstract class AnchorTo : MonoBehaviour
    {
        [SerializeField] protected UnityEvent attachEvent;
        [SerializeField] protected UnityEvent detachEvent;

        [SerializeField] protected Anchor anchor;

        protected abstract void AttachCheck();

        protected abstract void DetachCheck();

        public abstract void AddAnchor(Anchor child, IAnchorSocket parent);

        public abstract void RemoveAnchor(Anchor child);
    }
}