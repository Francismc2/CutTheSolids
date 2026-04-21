using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("Get MouseButton")]
    [CreateNodeMenu(AFXMenuTree.InputMouse + "Input Get Mouse Button")]
    public class GetMouseButton : AFXActiveNode
    {
        [SerializeField][Output] private AFXFlow leftClick;
        [SerializeField][Output] private AFXFlow middleClick;
        [SerializeField][Output] private AFXFlow rightClick;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (Input.GetMouseButton(0))
            {
                leftClick.ActivateNextNode(GetPort(nameof(leftClick)));
            }

            if (Input.GetMouseButton(1))
            {
                rightClick.ActivateNextNode(GetPort(nameof(rightClick)));
            }

            if (Input.GetMouseButton(2))
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