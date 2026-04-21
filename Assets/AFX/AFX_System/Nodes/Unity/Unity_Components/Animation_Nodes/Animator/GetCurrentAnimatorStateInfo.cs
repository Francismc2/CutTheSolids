using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [NodeTitle("Get Animator State Info")]
    [CreateNodeMenu(AFXMenuTree.ComponentAnimation + "Get Current Animator State Info")]
    public class GetCurrentAnimatorStateInfo : AFXNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private Animator animatorIn;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int layer = 0;

        [SerializeField]
        [Output] private float length;
        [SerializeField]
        [Output] private bool loop;
        [SerializeField]
        [Output] private float normalizedTime;
        [SerializeField]
        [Output] private float speed;
        [SerializeField]
        [Output] private float speedMultiplier;
        [SerializeField]
        [Output] private int fullPathHash;


        private AnimatorStateInfo stateInfo;

        public override object GetValue(NodePort port)
        {
            animatorIn = GetInputValue(nameof(animatorIn), animatorIn);
            layer = GetInputValue(nameof(layer), layer);

            stateInfo = animatorIn.GetCurrentAnimatorStateInfo(layer);            
            if (port.fieldName == nameof(length))
            {
                length = stateInfo.length;
                return length;
            }
            if (port.fieldName == nameof(loop))
            {
                loop = stateInfo.loop;
                return loop;
            }
            if (port.fieldName == nameof(normalizedTime))
            {
                normalizedTime = stateInfo.normalizedTime;
                return normalizedTime;
            }
            if (port.fieldName == nameof(speed))
            {
                speed = stateInfo.speed;
                return speed;
            }
            if (port.fieldName == nameof(speedMultiplier))
            {
                speedMultiplier = stateInfo.speedMultiplier;
                return speedMultiplier;
            }
            if (port.fieldName == nameof(fullPathHash))
            {
                fullPathHash = stateInfo.fullPathHash;
                return fullPathHash;
            }
            return null;
        }
    }
}