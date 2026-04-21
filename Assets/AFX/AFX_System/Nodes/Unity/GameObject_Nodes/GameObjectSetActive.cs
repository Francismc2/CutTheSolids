using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("GameObject SetActive")]
    [CreateNodeMenu(AFXMenuTree.GameObject+ "GameObject Set Active")]
    public class GameObjectSetActive : AFXActiveNode
    {
        [SerializeField]
        [Input] private GameObject gameObjectIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool setActive = false;

        void SetGOActive()
        {
            gameObjectIn = GetInputValue(nameof(gameObjectIn), gameObjectIn);
            setActive = GetInputValue(nameof(setActive), setActive);

            gameObjectIn.SetActive(setActive);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetGOActive();
            base.ExecuteNode(exit);
        }
    }
}