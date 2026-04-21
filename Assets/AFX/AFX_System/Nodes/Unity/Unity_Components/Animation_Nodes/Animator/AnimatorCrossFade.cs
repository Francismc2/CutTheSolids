using UnityEngine;
namespace Engage.AFX.v1
{
    [NodeTitle("CrossFade")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimation + "Animator CrossFade")]
    public class AnimatorCrossFade : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Animator animatorIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private bool fixedTime;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private string stateName;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int stateNameHash;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float transitionDuration = 0.5f;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int layer = 0;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float timeOffset;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float transitionTime;

        public void CrossFade()
        {
            animatorIn = GetInputValue(nameof(animatorIn), animatorIn);
            fixedTime = GetInputValue(nameof(fixedTime), fixedTime);
            stateName = GetInputValue(nameof(stateName), stateName);
            stateNameHash = GetInputValue(nameof(stateNameHash), stateNameHash);
            transitionDuration = GetInputValue(nameof(transitionDuration), transitionDuration);
            timeOffset = GetInputValue(nameof(timeOffset), timeOffset);
            transitionTime = GetInputValue(nameof(transitionTime), transitionTime);

            if (GetInputPort(nameof(stateNameHash)).IsConnected)
            {
                if (fixedTime)
                {
                    animatorIn.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, timeOffset, transitionTime);
                }
                else
                {
                    animatorIn.CrossFade(stateNameHash, transitionDuration, layer, timeOffset, transitionTime);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(stateName))
                {
                    if (fixedTime)
                    {
                        animatorIn.CrossFadeInFixedTime(stateName, transitionDuration);
                    }
                    else
                    {
                        animatorIn.CrossFade(stateName, transitionDuration);
                    }
                }
                else
                {
                    Error = $"[{this.name}] State Name Empty";
                }
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            CrossFade();
            base.ExecuteNode(exit);
        }
    }
}