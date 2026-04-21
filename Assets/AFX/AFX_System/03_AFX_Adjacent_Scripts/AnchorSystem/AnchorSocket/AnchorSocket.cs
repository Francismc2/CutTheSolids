using System.Collections.Generic;
using UnityEngine;

namespace Engage.AFX.Anchor.v1
{
    [AddComponentMenu("AFX/Anchor/Anchor Socket")]
    public class AnchorSocket : MonoBehaviour, IAnchorSocket
    {
        [SerializeField] private List<Anchor> currentAnchors;
        [SerializeField] private Transform socketTransform;
        [SerializeField] private bool lockConnection = false;
        [SerializeField] private int maximumConnections = 1;

        public Transform SocketTransform { get => socketTransform; set => socketTransform = value; }
        public List<Anchor> CurrentAnchors { get => currentAnchors; set => currentAnchors = value; }
        public int MaximumConnections { get => maximumConnections; set => maximumConnections = value; }
        public bool LockConnection { get => lockConnection; set => lockConnection = value; }

        public void SetLockConnection(bool lockConnection)
        {
            this.lockConnection = lockConnection;
        }

        public void SetMaximumConnections(int maximumConnections)
        {
            this.maximumConnections = maximumConnections;
        }
    }
}