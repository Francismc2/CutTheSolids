using UnityEngine;
using System;

namespace Engage.IFX.NetworkStates
{
    public class NetworkStateModule_Transform : NetworkStateModule
    {

        [SerializeField]
        private NetworkState_Transforms_Settings[] transforms;
    }

    [Serializable]
    public class NetworkState_Transforms_Settings
    {
        [SerializeField]
        public bool interpolate;
        [SerializeField]
        public float networkSensitivity = 0.01f;
        [SerializeField]
        public bool networkPosition;
        [SerializeField]
        public bool networkRotation;
        [SerializeField]
        public bool networkScale;
        [SerializeField]
        public Transform transform;
    }
}