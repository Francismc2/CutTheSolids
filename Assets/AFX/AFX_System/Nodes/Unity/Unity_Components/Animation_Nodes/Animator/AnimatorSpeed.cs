using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Speed")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimation + "Set Animator Speed")]
    public class AnimatorSpeed : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Animator animatorIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float speed = 0;

        public void SetSpeed()
        {
            animatorIn = GetInputValue(nameof(animatorIn), animatorIn);
            speed = GetInputValue(nameof(speed), speed);

            animatorIn.speed = speed;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetSpeed();
            base.ExecuteNode(exit);
        }
    }
}