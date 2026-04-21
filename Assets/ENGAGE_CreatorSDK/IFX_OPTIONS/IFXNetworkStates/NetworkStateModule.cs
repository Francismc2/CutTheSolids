
using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base class for IFX Network States
/// </summary>
namespace Engage.IFX.NetworkStates
{
    public class NetworkStateModule : MonoBehaviour
    {
        [Header("If using outside IFX (in location), this ID must be a completely unique string (up to 8 characters)", order = 0)]
        [SerializeField]
        private string uniqueID;

        [Header("Should this module be recorded?", order = 1)]
        [SerializeField]
        private bool recordable;

        /// <summary>
        /// Reliable Fixed ownership. This is optional and not part of normal usage (dynamic)
        /// </summary>
        public void SetFixedOwnerMe()
        {
        }

        /// <summary>
        /// Reliable Fixed ownership. This is optional and not part of normal usage (dynamic)
        /// </summary>
        public void SetFixedOwnership(int playerNumber)
        {
        }

        /// <summary>
        /// Release all fixed ownership and return to dynamic ownership (default)
        /// </summary>
        public void ReleaseFixedOwnership()
        {
        }

        public void Start()
        {
        }
    }
}
