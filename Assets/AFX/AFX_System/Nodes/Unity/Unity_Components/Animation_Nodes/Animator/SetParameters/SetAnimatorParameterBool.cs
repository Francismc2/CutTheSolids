using UnityEngine;

namespace Engage.AFX.v1.Components.AnimationNodes.v1
{
    [NodeTitle("Set Animator Bool")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimationSetParams + "Set Animator Parameter Bool")]
    public class SetAnimatorParameterBool : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Animator animator;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private string name;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool boolIn;

        public void SetParamaterBool()
        {
            animator = GetInputValue(nameof(animator), animator);
            name = GetInputValue(nameof(name), name);

            if (!string.IsNullOrEmpty(name))
            {
                boolIn = GetInputValue(nameof(boolIn), boolIn);
                animator.SetBool(name, boolIn);
            }
            else
            {
                Error = $"[{this.name}] Parameter Name Empty";
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetParamaterBool();
            base.ExecuteNode(exit);
        }
    }
}