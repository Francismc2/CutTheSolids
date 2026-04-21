using UnityEngine;

namespace Engage.AFX.v1.Components.AnimationNodes.v1
{
    [NodeTitle("Set Animator Float")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimationSetParams + "Set Animator Parameter Float")]
    public class SetAnimatorParameterFloat : AFXActiveNode
    {

        [SerializeField] [Input(ShowBackingValue.Never)] private Animator animator;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private string name;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float floatIn;

        public void SetParamaterFloat()
        {
            animator = GetInputValue(nameof(animator), animator);
            name = GetInputValue(nameof(name), name);

            if (!string.IsNullOrEmpty(name))
            {
                floatIn = GetInputValue(nameof(floatIn), floatIn);
                animator.SetFloat(name, floatIn);
            }
            else
            {
                Error = $"[{this.name}] Parameter Name Empty";
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetParamaterFloat();
            base.ExecuteNode(exit);
        }
    }
}