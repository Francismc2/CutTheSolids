// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 19/09/21
// Author: marmstrong
// Description: ENGAGE
// Immersive VR Education
// ---------------------------------------------
// -------------------------------------------*/

using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Engage.IFX.NetworkStates
{
    public enum SyncState
    {
        idle = 0,
        active = 1,
        recorded = 2
    }

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    [Serializable]
    public class IntEvent : UnityEvent<int> { }
    [Serializable]
    public class FloatEvent : UnityEvent<float> { }
    [Serializable]
    public class IFXNetworkStateEvent : UnityEvent<IFXNetworkState> { }

    [Serializable]
    public class IFXNetworkState
    {
        public string uniqueID;
        public List<bool> bools = new List<bool>();
        public List<int> ints = new List<int>();
        public List<float> floats = new List<float>();

        public IFXNetworkState() { }
        public IFXNetworkState
            (
                List<bool> bools,
                List<int> ints,
                List<float> floats
            )
        {
            this.bools = bools;
            this.ints = ints;
            this.floats = floats;
        }
    }
}
