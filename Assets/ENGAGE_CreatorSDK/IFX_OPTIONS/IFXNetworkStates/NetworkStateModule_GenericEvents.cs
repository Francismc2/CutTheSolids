// /*-------------------------------------------
// ---------------------------------------------
// Creation Date: 14/07/21
// Author: marmstrong
// Description: ENGAGE
// Immersive VR Education
// ---------------------------------------------
// -------------------------------------------*/

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic network states for use with inspector events
/// </summary>
namespace Engage.IFX.NetworkStates
{
    public class NetworkStateModule_GenericEvents : NetworkStateModule
    {
        [SerializeField]
        private List<bool> bools = new List<bool>();
        [SerializeField]
        private List<BoolEvent> onBoolChanged = new List<BoolEvent>();
        [SerializeField]
        private List<int> ints = new List<int>();
        [SerializeField]
        private List<IntEvent> onIntChanged = new List<IntEvent>();
        [SerializeField]
        private List<float> floats = new List<float>();
        [SerializeField]
        private List<FloatEvent> onFloatChanged = new List<FloatEvent>();


        public void SetBool0(bool value) { }
        public void SetBool1(bool value) { }
        public void SetBool2(bool value) { }
        public void SetBool3(bool value) { }
        public void SetBool4(bool value) { }
        public void SetBool5(bool value) { }
        public void SetBool6(bool value) { }
        public void SetBool7(bool value) { }
        public void SetBool8(bool value) { }
        public void SetBool9(bool value) { }
        public void SetBool10(bool value) { }
        
        public void ToggleBool0() { }
        public void ToggleBool1() { }
        public void ToggleBool2() { }
        public void ToggleBool3() { }
        public void ToggleBool4() { }
        public void ToggleBool5() { }
        public void ToggleBool6() { }
        public void ToggleBool7() { }
        public void ToggleBool8() { }
        public void ToggleBool9() { }
        public void ToggleBool10() { }

        public void SetInt0(int value) { }
        public void SetInt1(int value) { }
        public void SetInt2(int value) { }
        public void SetInt3(int value) { }
        public void SetInt4(int value) { }
        public void SetInt5(int value) { }
        public void SetInt6(int value) { }
        public void SetInt7(int value) { }
        public void SetInt8(int value) { }
        public void SetInt9(int value) { }
        public void SetInt10(int value) { }

        public void AddToInt0(int value) { }
        public void AddToInt1(int value) { }
        public void AddToInt2(int value) { }
        public void AddToInt3(int value) { }
        public void AddToInt4(int value) { }
        public void AddToInt5(int value) { }
        public void AddToInt6(int value) { }
        public void AddToInt7(int value) { }
        public void AddToInt8(int value) { }
        public void AddToInt9(int value) { }
        public void AddToInt10(int value) { }

        public void SetFloat0(float value) { }
        public void SetFloat1(float value) { }
        public void SetFloat2(float value) { }
        public void SetFloat3(float value) { }
        public void SetFloat4(float value) { }
        public void SetFloat5(float value) { }
        public void SetFloat6(float value) { }
        public void SetFloat7(float value) { }
        public void SetFloat8(float value) { }
        public void SetFloat9(float value) { }
        public void SetFloat10(float value) { }

        public void AddToFloat0(float value) { }
        public void AddToFloat1(float value) { }
        public void AddToFloat2(float value) { }
        public void AddToFloat3(float value) { }
        public void AddToFloat4(float value) { }
        public void AddToFloat5(float value) { }
        public void AddToFloat6(float value) { }
        public void AddToFloat7(float value) { }
        public void AddToFloat8(float value) { }
        public void AddToFloat9(float value) { }
        public void AddToFloat10(float value) { }
    }
}
