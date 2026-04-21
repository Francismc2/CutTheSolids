using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.UnityCompAnimationCurve + "AnimationCurve Evaluate")]
    public class AnimationCurveEvaluate : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private AnimationCurve animationCurve;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private float time;

        [SerializeField][Output(ShowBackingValue.Never)] private float output;

        public override object GetValue(NodePort port)
        {
            animationCurve = GetInputValue(nameof(animationCurve), animationCurve);
            time = GetInputValue(nameof(time), time);

            return animationCurve.Evaluate(time);
        }
    }
}
