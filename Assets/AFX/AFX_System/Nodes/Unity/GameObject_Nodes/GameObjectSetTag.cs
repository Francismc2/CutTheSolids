using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Tag")]
    [CreateNodeMenu(AFXMenuTree.GameObject + "GameObject Set Tag")]
    public class GameObjectSetTag : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private GameObject input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string tag;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            tag = GetInputValue(nameof(tag), tag);
            input.tag = tag;
            base.ExecuteNode(exit);
        }
    }
}