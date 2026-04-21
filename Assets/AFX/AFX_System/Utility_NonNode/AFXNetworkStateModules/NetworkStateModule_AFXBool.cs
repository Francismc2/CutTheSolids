using UnityEngine;
using System.Collections.Generic;
using Engage.AFX.v1;

namespace Engage.IFX.NetworkStates.AFX.v1
{
    public class NetworkStateModule_AFXBool : NetworkStateModule
    {
        [SerializeField] private BoolComponent[] afxBools;
    }
}