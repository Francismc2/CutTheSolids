using UnityEngine;

namespace Engage.Avatars.Poses
{
    [CreateAssetMenu(menuName = "Avatars/ New PoseData", fileName = "New Pose")]
    public class PoseDataAsset : ScriptableObject
    {
        [System.Flags]
        public enum poseFlags
        {
            None = 0,
            ignoredPose = 1,
            vrOnlyPose = 2,
            nonVROnlyPose = 4,
            overrideVRHead_Pos = 8,
            overrideVRHead_Rot = 16,
            overrideVRHand_L = 32,
            overrideVRHand_R = 64,
            skirtOnly = 128,
            ChestPosingEnabled = 256

        }
        public enum handPosesOptions
        {
            None = 0,
            Fist = 1,
            Point = 2
            

        }
        [Header("---Min(x) and Max(y) Heights-------------------------")]
        [SerializeField]
        public Vector2 heightRange;
        [Header("---Specialised Options----------------------------------------")]
        [SerializeField]
        public poseFlags flags;
        [SerializeField]
        public handPosesOptions handPose_Left;
        public handPosesOptions handPose_Right;



        #region _Anchors
        [Header("---Raw Data-------------------------------------------------")]
        [SerializeField]
        public Vector3 m_head_Anchor_Pos;
        [SerializeField]
        public Vector3 m_head_Anchor_Rot;
        [SerializeField]
        public Vector3 m_pelvis_Anchor_Pos;
        [SerializeField]
        public Vector3 m_pelvis_Anchor_Rot;
        [SerializeField]
        public Vector3 m_chest_Anchor_Pos;
        [SerializeField]
        public Vector3 m_chest_Anchor_Rot;

        [SerializeField]
        public Vector3 m_rightFoot_Anchor_Pos;
        [SerializeField]
        public Vector3 m_rightFoot_Anchor_Rot;
        [SerializeField]
        public Vector3 m_leftFoot_Anchor_Pos;
        [SerializeField]
        public Vector3 m_leftFoot_Anchor_Rot;



        [SerializeField]
        public Vector3 m_rightHand_Anchor_Pos;
        [SerializeField]
        public Vector3 m_rightHand_Anchor_Rot;
        [SerializeField]
        public Vector3 m_leftHand_Anchor_Pos;
        [SerializeField]
        public Vector3 m_leftHand_Anchor_Rot;
        //////////////////////////////////////////

        [SerializeField]
        public Vector3 m_rightElbow_Anchor_Pos;
        [SerializeField]
        public Vector3 m_leftElbow_Anchor_Pos;

        [SerializeField]
        public Vector3 m_rightKnee_Anchor_Pos;

        [SerializeField]
        public Vector3 m_leftKnee_Anchor_Pos;


        #endregion
        [Header("---Do Not CHANGE-------------------------------------------------")]
        [SerializeField]
        public float maxHeadDistance = 1.25f;
    }

}