using System;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [Serializable]
    public class AFXFlow
    {
        private NodePort port;
        private IExecutableNode nextNode;

        private float activeTime;
        private bool activeNoodle = false;

        public NodePort Port { get => port; }

        public bool ActiveNoodle
        {
            get
            {
                if (activeNoodle)
                {
                    activeTime += Time.deltaTime;
                    if (activeTime > 0.5)
                    {
                        SetNoodleState(false);
                    }
                    return activeNoodle;
                }
                return activeNoodle;
            }
        }

        private void SetNoodleState(bool state)
        {
            activeNoodle = state;
            activeTime = 0;
        }

        public void ActivateNextNode(NodePort port)
        {
            if (!port.IsConnected) return;

            this.port = port;
            SetNoodleState(true);

            foreach (NodePort connection in port.GetConnections())
            {
                if (connection.direction == NodePort.IO.Output) continue;

                nextNode = connection.node as IExecutableNode;
                nextNode.ExecuteNode(this);
            }
        }
    }
}