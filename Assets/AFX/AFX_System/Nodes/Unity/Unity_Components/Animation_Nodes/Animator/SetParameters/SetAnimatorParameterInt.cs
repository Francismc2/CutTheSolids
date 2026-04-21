using UnityEngine;

namespace Engage.AFX.v1.Components.AnimationNodes.v1
{
    [NodeTitle("Set Animator Int")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimationSetParams + "Set Animator Parameter Int")]
    public class SetAnimatorParameterInt : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Animator animator;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private string name;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private int intIn;

        public void SetParameterInt()
        {
            animator = GetInputValue(nameof(animator), animator);
            name = GetInputValue(nameof(name), name);

            if (!string.IsNullOrEmpty(name))
            {
                intIn = GetInputValue(nameof(intIn), intIn);
                animator.SetInteger(name, intIn);
            }
            else
            {
                Error = $"[{this.name}] Parameter Name Empty";
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetParameterInt();
            base.ExecuteNode(exit);
        }
    }
}