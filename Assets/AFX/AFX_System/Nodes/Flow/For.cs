using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow+ "Flow For")]
    public class For : AFXFlowNode
    {
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow exit;
        [SerializeField][Output(ShowBackingValue.Never, ConnectionType.Multiple)] private AFXFlow body;

        [SerializeField][Input(ShowBackingValue.Unconnected)] private int start;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int end;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int step;

        [SerializeField][Output] private int index;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(index))
            {
                return index;
            }

            return null;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            start = GetInputValue(nameof(start), start);
            end = GetInputValue(nameof(end), end);
            step = GetInputValue(nameof(step), step);

            for (index = start; index < end; index += step)
            {
                body.ActivateNextNode(GetPort(nameof(body)));
            }

            exit.ActivateNextNode(GetPort(nameof(exit)));
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(body) => body,
                nameof(exit) => exit,
                _ => null
            };
        }
    }
}