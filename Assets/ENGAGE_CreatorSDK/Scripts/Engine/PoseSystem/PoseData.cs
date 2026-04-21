using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engage.Avatars.Poses
{
    [CreateAssetMenu(menuName = "Avatars/Config/PoseData", fileName = "New Pose")]
    public class PoseData : ScriptableObject
    {
        private Dictionary<PoseBodyPart, PoseDataHandle> m_handleDictionary;

        [SerializeField]
        private string m_uniqueName = "Unique Name";

        [SerializeField]
        private PoseType m_type;
        public PoseType Type { get { return m_type; } }

        [SerializeField]
        private bool m_disable = false;
        public bool Disabled { get { return m_disable; } }

        #region Handles

        [SerializeField]
        private PoseDataHandle m_headHandle;
        [SerializeField]
        private PoseDataHandle m_pelvisHandle;
        [SerializeField]
        private PoseDataHandle m_chestHandle;

        [SerializeField]
        private PoseDataHandle m_rightFootHandle;
        [SerializeField]
        private PoseDataHandle m_leftFootHandle;

        [SerializeField]
        private PoseDataHandle m_rightKneeHandle;
        [SerializeField]
        private PoseDataHandle m_leftKneeHandle;

        [SerializeField]
        private PoseDataHandle m_rightHandHandle;
        [SerializeField]
        private PoseDataHandle m_leftHandHandle;

        [SerializeField]
        private PoseDataHandle m_rightElbowHandle;
        [SerializeField]
        private PoseDataHandle m_leftElbowHandle;

        #endregion

        private void OnValidate()
        {
            Initialise();
        }

        private void Initialise()
        {
            if (m_handleDictionary != null)
                return;

            ResetPoseData();
        }

        public void ResetPoseData()
        {
            Debug.Log("Setting Pose Data");
            if (m_handleDictionary == null)
                m_handleDictionary = new Dictionary<PoseBodyPart, PoseDataHandle>(6);
            else
                m_handleDictionary.Clear();

            AddHandle(PoseBodyPart.PELVIS, m_pelvisHandle);
            AddHandle(PoseBodyPart.HEAD, m_headHandle);
            AddHandle(PoseBodyPart.CHEST, m_chestHandle);

            AddHandle(PoseBodyPart.RIGHT_FOOT, m_rightFootHandle);
            AddHandle(PoseBodyPart.LEFT_FOOT, m_leftFootHandle);
            AddHandle(PoseBodyPart.RIGHT_KNEE, m_rightKneeHandle);
            AddHandle(PoseBodyPart.LEFT_KNEE, m_leftKneeHandle);

            AddHandle(PoseBodyPart.RIGHT_HAND, m_rightHandHandle);
            AddHandle(PoseBodyPart.LEFT_HAND, m_leftHandHandle);
            AddHandle(PoseBodyPart.RIGHT_ELBOW, m_rightElbowHandle);
            AddHandle(PoseBodyPart.LEFT_ELBOW, m_leftElbowHandle);
        }

        private void AddHandle(PoseBodyPart bodyPart, PoseDataHandle handle)
        {
            //if (!handle.Enabled)
            //    return;

            m_handleDictionary.Add(bodyPart, handle);
        }

        public bool GetBodyPartMap(PoseBodyPart part, out PoseMapping map)
        {
            map = new PoseMapping();

            if (m_handleDictionary == null || m_handleDictionary.Count == 0)
                ResetPoseData();

            if (!m_handleDictionary.ContainsKey(part))
                return false;

            map.Position = m_handleDictionary[part].Position;
            map.Rotation = Quaternion.Euler(m_handleDictionary[part].Rotation);

            return true;
        }

        public PoseDataHandle GetBodyPartHandle(PoseBodyPart part)
        {
            if (m_handleDictionary == null || m_handleDictionary.Count == 0 || !m_handleDictionary.ContainsKey(part))
                ResetPoseData();

            Debug.Log("m_handleDictionary size = " + m_handleDictionary.Count);

            return m_handleDictionary[part];

        }

        public bool HasBodyPart(PoseBodyPart part)
        {
            Initialise();
            return m_handleDictionary.ContainsKey(part);
        }

        public bool SnapToGround(PoseBodyPart part)
        {
            if (!HasBodyPart(part))
                return false;

            return m_handleDictionary[part].SnapToGround;
        }

        #region Updating

        public void UpdatePosition(PoseBodyPart bodyPart, Vector3 pos)
        {
            switch (bodyPart)
            {
                case PoseBodyPart.PELVIS:
                    m_pelvisHandle.Position = pos;
                    break;
                case PoseBodyPart.HEAD:
                    m_headHandle.Position = pos;
                    break;
                case PoseBodyPart.CHEST:
                    m_chestHandle.Position = pos;
                    break;

                case PoseBodyPart.RIGHT_HAND:
                    m_rightHandHandle.Position = pos;
                    break;
                case PoseBodyPart.LEFT_HAND:
                    m_leftHandHandle.Position = pos;
                    break;
                case PoseBodyPart.RIGHT_FOOT:
                    m_rightFootHandle.Position = pos;
                    break;
                case PoseBodyPart.LEFT_FOOT:
                    m_leftFootHandle.Position = pos;
                    break;

                case PoseBodyPart.RIGHT_ELBOW:
                    m_rightElbowHandle.Position = pos;
                    break;
                case PoseBodyPart.LEFT_ELBOW:
                    m_leftElbowHandle.Position = pos;
                    break;
                case PoseBodyPart.RIGHT_KNEE:
                    m_rightKneeHandle.Position = pos;
                    break;
                case PoseBodyPart.LEFT_KNEE:
                    m_leftKneeHandle.Position = pos;
                    break;
            }
        }

        public void UpdateRotation(PoseBodyPart bodyPart, Vector3 rot)
        {
            switch (bodyPart)
            {
                case PoseBodyPart.PELVIS:
                    m_pelvisHandle.Rotation = rot;
                    break;
                case PoseBodyPart.HEAD:
                    m_headHandle.Rotation = rot;
                    break;
                case PoseBodyPart.CHEST:
                    m_chestHandle.Rotation = rot;
                    break;

                case PoseBodyPart.RIGHT_HAND:
                    m_rightHandHandle.Rotation = rot;
                    break;
                case PoseBodyPart.LEFT_HAND:
                    m_leftHandHandle.Rotation = rot;
                    break;
                case PoseBodyPart.RIGHT_FOOT:
                    m_rightFootHandle.Rotation = rot;
                    break;
                case PoseBodyPart.LEFT_FOOT:
                    m_leftFootHandle.Rotation = rot;
                    break;

                case PoseBodyPart.RIGHT_ELBOW:
                    m_rightElbowHandle.Rotation = rot;
                    break;
                case PoseBodyPart.LEFT_ELBOW:
                    m_leftElbowHandle.Rotation = rot;
                    break;
                case PoseBodyPart.RIGHT_KNEE:
                    m_rightKneeHandle.Rotation = rot;
                    break;
                case PoseBodyPart.LEFT_KNEE:
                    m_leftKneeHandle.Rotation = rot;
                    break;
            }
        }

        #endregion
    }
}