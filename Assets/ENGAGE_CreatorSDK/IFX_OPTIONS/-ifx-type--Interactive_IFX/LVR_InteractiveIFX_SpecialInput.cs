using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVR_InteractiveIFX_SpecialInput : MonoBehaviour
{
    public enum InputType
    {
        startState,
        pickup,
        pickedUp_oppositeHand_Grip_Push,
        pickedUp_oppositeHand_Grip_Release,
        pickedUp_currentHand_Grip_Push,
        pickedUp_currentHand_Grip_Release,
        release
    }

    public enum AnimatorStateType
    {
        Float,
        Int,
        Bool,
        Trigger
    }

    [System.Serializable]
    public class AnimatorStateChange
    {
        public Animator animator;
        public AnimatorStateType animatorStateType;
        public string parameterName;
        public int intValue;
        public float floatValue;
        public bool boolValue;

        public AnimatorStateChange
            (
                Animator animator,
                AnimatorStateType animatorStateType,
                string parameterName,
                int intValue,
                float floatValue,
                bool boolValue
            )
        {
            this.animator = animator;
            this.animatorStateType = animatorStateType;
            this.parameterName = parameterName;
            this.intValue = intValue;
            this.floatValue = floatValue;
            this.boolValue = boolValue;
        }
    }

    [System.Serializable]
    public class InteractiveInput
    {
        public string name;
        public InputType inputType;
        public GameObject[] activateObjects;
        public GameObject[] deactivateObjects;
        public AnimatorStateChange[] animatorStateChange;
        public bool onlyUseAfterAnotherInputFires;
        public int inputInListToCheckFired;
        [System.NonSerialized]
        public bool thisInputFired = false;

        public InteractiveInput
            (
                string name,
                InputType inputType,
                GameObject[] activateObjects,
                GameObject[] deactivateObjects,
                AnimatorStateChange[] animatorStateChange,
                bool onlyUseAfterAnotherInputFires,
                int inputInListToCheckFired,
                bool thisInputFired
            )
        {
            this.name = name;
            this.inputType = inputType;
            this.activateObjects = activateObjects;
            this.deactivateObjects = deactivateObjects;
            this.animatorStateChange = animatorStateChange;
            this.onlyUseAfterAnotherInputFires = onlyUseAfterAnotherInputFires;
            this.inputInListToCheckFired = inputInListToCheckFired;
            thisInputFired = true;
        }
    }

    public List<InteractiveInput> interactiveInput = new List<InteractiveInput>();

}
