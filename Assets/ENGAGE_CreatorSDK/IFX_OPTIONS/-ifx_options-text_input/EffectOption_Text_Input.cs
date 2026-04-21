using System;
using UnityEngine;
using UnityEngine.Events;

namespace Engage.IFX.Options
{
    public class EffectOption_Text_Input : MonoBehaviour 
    {
        [Serializable]
        public class OnGoLoaded : UnityEvent<string> { }

        [SerializeField] private OnGoLoaded onGoLoaded;
    }
}
