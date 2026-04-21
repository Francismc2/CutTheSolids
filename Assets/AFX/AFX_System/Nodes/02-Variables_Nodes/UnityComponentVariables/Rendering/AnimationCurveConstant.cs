using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTitle("AnimationCurve")]
    [CreateNodeMenu(AFXMenuTree.UnityCompAnimationCurve + "AnimationCurve")]
    public class AnimationCurveConstant : AFXNode
    {
        [SerializeField][Output(ShowBackingValue.Always)] private AnimationCurve curve;

        public override object GetValue(NodePort port)
        {
            return curve;
        }
    }
}