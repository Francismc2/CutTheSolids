using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Component Enabled")]
    [CreateNodeMenu(AFXMenuTree.Component + "Set Component Enabled")]
    public class ComponentSetActive : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Behaviour behaviourIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool setActive = false;

        void SetComponentActive()
        {
            behaviourIn = GetInputValue(nameof(behaviourIn), behaviourIn);
            setActive = GetInputValue(nameof(setActive), setActive);
            behaviourIn.enabled = setActive;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetComponentActive();
            base.ExecuteNode(exit);
        }
    }
}