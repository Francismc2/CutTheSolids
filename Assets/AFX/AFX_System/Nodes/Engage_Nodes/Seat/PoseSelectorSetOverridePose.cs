using UnityEngine;
using System.Collections.Generic;
using XNode;
using Engage.Avatars.Poses;
namespace Engage.AFX.v1
{
    [NodeTitle("Set Override Pose")]
    [CreateNodeMenu(AFXMenuTree.Seat + "PoseSelector Set Override Pose")]
    public class PoseSelectorSetOverridePose : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never)] private PoseSelector poseSelector;

        [SerializeField]
        private List<PoseDataAsset> poseList;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            poseSelector = GetInputValue(nameof(poseSelector), poseSelector);

            poseSelector.OverrideWithNewPoses(poseList);

            base.ExecuteNode(exit);
        }
    }
}