using UnityEngine;

namespace Engage.AFX.v1.Components.AnimationNodes.v1
{
    [NodeTitle("Set Animator Trigger")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimationSetParams + "Set Animator Parameter Trigger")]
    public class SetAnimatorParameterTrigger : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Animator animator;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private string name;

        public void SetParameterTrigger()
        {
            animator = GetInputValue(nameof(animator), animator);
            name = GetInputValue(nameof(name), name);

            if (!string.IsNullOrEmpty(name))
            {
                animator.SetTrigger(name);
            }
            else
            {
                Error = $"[{this.name}] Parameter Name Empty";
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetParameterTrigger();
            base.ExecuteNode(exit);
        }
    }
}