using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Play")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimation + "Animator Play")]
    public class AnimatorPlay : AFXActiveNode
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
        [Input(ShowBackingValue.Unconnected)] private int layer = -1;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float time = 0;

        public void Play()
        {
            animatorIn = GetInputValue(nameof(animatorIn), animatorIn);
            fixedTime = GetInputValue(nameof(fixedTime), fixedTime);
            stateName = GetInputValue(nameof(stateName), stateName);
            stateNameHash = GetInputValue(nameof(stateNameHash), stateNameHash);
            layer = GetInputValue(nameof(layer), layer);
            time = GetInputValue(nameof(time), time);

            if (GetInputPort(nameof(stateNameHash)).IsConnected)
            {
                if (fixedTime)
                {
                    animatorIn.PlayInFixedTime(stateNameHash, layer, time);
                }
                else
                {
                    animatorIn.Play(stateNameHash, layer, time);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(stateName))
                {
                    if (fixedTime)
                    {
                        animatorIn.PlayInFixedTime(stateName, layer, time);
                    }
                    else
                    {
                        animatorIn.Play(stateName, layer, time);
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
            Play();
            base.ExecuteNode(exit);
        }
    }
}