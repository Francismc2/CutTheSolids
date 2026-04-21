using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow + "Flow Toggle")]
    public class Toggle : AFXFlowNode
    {
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow turnOn;
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow turnOff;
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow toggle;

        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow on;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow off;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow turnedOn;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow turnedOff;

        [SerializeField][Output] private bool isOn;

        private NodePort turnOnPortCache;
        private NodePort turnOffPortCache;
        private NodePort togglePortCache;

        public override object GetValue(NodePort port)
        {
            return isOn;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (turnOnPortCache == null)
            {
                turnOnPortCache = GetPort(nameof(turnOn));
            }

            if (turnOffPortCache == null)
            {
                turnOffPortCache = GetPort(nameof(turnOff));
            }

            if (togglePortCache == null)
            {
                togglePortCache = GetPort(nameof(toggle));
            }

            PortSpecificActions(afxFlow);

            if (isOn)
            {
                on.ActivateNextNode(GetPort(nameof(on)));
            }
            else
            {
                off.ActivateNextNode(GetPort(nameof(off)));
            }
        }

        private void PortSpecificActions(AFXFlow afxFlow)
        {
            if (afxFlow.Port == turnOnPortCache.Connection)
            {
                if (!isOn)
                {
                    isOn = true;
                    turnedOn.ActivateNextNode(GetPort(nameof(turnedOn)));
                }
                return;
            }

            if (afxFlow.Port == turnOffPortCache.Connection)
            {
                if (isOn)
                {
                    isOn = false;
                    turnedOff.ActivateNextNode(GetPort(nameof(turnedOff)));
                }
                return;
            }

            if (afxFlow.Port == togglePortCache.Connection)
            {
                if (isOn)
                {
                    isOn = false;
                    turnedOff.ActivateNextNode(GetPort(nameof(turnedOff)));
                }
                else
                {
                    isOn = true;
                    turnedOn.ActivateNextNode(GetPort(nameof(turnedOn)));
                }
            }
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(turnedOn) => turnedOn,
                nameof(turnedOff) => turnedOff,
                nameof(on) => on,
                nameof(off) => off,
                _ => null
            };
        }
    }
}