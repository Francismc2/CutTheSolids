using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.GameObject + "GameObject Set Layer")]
    public class GameObjectSetLayer : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private GameObject input;

        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int layer;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            input = GetInputValue(nameof(input), input);
            layer = GetInputValue(nameof(layer), layer);
            input.layer = layer;
            base.ExecuteNode(exit);
        }
    }
}