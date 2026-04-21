using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("GetMouseButtonUp")]
    [CreateNodeMenu(AFXMenuTree.InputMouse + "Input GetMouseButtonUp")]
    public class GetMouseButtonUp : AFXActiveNode
    {
        [SerializeField][Output] private AFXFlow leftClick;
        [SerializeField][Output] private AFXFlow middleClick;
        [SerializeField][Output] private AFXFlow rightClick;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (Input.GetMouseButtonUp(0))
            {
                leftClick.ActivateNextNode(GetPort(nameof(leftClick)));
            }

            if (Input.GetMouseButtonUp(1))
            {
                rightClick.ActivateNextNode(GetPort(nameof(rightClick)));
            }

            if (Input.GetMouseButtonUp(2))
            {
                middleClick.ActivateNextNode(GetPort(nameof(middleClick)));
            }

            base.ExecuteNode(exit);
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(leftClick) => leftClick,
                nameof(rightClick) => rightClick,
                nameof(middleClick) => middleClick,
                _ => null
            };
        }
    }
}