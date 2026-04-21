using System;
using UnityEngine;

namespace Engage.IFX.NetworkStates
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ENGAGE/Networked Animator")]
    public class NetworkStateModule_NetworkedAnimatorAdvanced : NetworkStateModule
    {
        [Header("Use this script to make Animator changes safely over the network.\n\n" +
            "Typical usage: Button OnClick > BecomeOwner(), SelectParameter(), then Apply Value.\n\n" +
            "Do not modify the Animator component directly - use this interface for synced changes.")]

        [SerializeField] protected Animator animator;

        [Space(8)]
        [Header("Ownership control - Unnecessary if 'BecomeOwner' is called first on individual changes")]
        [SerializeField] private bool autoTakeOwnership;

        [Space(8)]
        [Header("Animator Parameter Control - Call 'SelectParameter' before setting parameter values.")]
        [SerializeField] private string selectedParameterName;

        [Space(8)]
        [Header("Animator Layer Weight Control - Call 'SelectLayer' or 'SelectLayerByName' before setting layer values.")]
        [SerializeField] private int selectedLayerIndex = 0;

        public void BecomeOwner()
        {
        }

        public void SelectParameter(string paramName)
        {
        }

        public void SetFloatValue(float value)
        {
        }

        public void AddToFloatValue(float value)
        {
        }

        public void SetIntValue(int value)
        {
        }

        public void AddToIntValue(int value)
        {
        }

        public void SetBoolValue(bool value)
        {
        }

        public void ToggleBoolValue()
        {
        }

        public void Trigger(string triggerName)
        {
        }

        public void PlayState(string stateName)
        {
        }

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void SetTime(float normalizedTime)
        {
        }

        public void SelectLayer(int layerIndex)
        {
        }

        public void SelectLayerByName(string layerName)
        {
        }

        public void SetSelectedLayerWeight01(float weight01)
        {
        }

        public void IncreaseSelectedLayerWeight(float delta)
        {
        }
    }
}
