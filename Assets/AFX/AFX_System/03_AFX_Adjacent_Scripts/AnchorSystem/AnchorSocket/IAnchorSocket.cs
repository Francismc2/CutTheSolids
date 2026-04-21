using System.Collections.Generic;
using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    public interface IAnchorSocket
    {
        public Transform SocketTransform
        {
            get;
        }

        public List<Anchor> CurrentAnchors
        {
            get;
            set;
        }

        public int MaximumConnections
        {
            get;
            set;
        }

        public bool LockConnection
        {
            get;
            set;
        }
    }
}