using System.Collections.Generic;
using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateAssetMenu(menuName = "AFX Event")]
    public class AFXEvent : ScriptableObject
    {
        private List<AFXEventComponent> componentEventListeners = new List<AFXEventComponent>();
        private List<OnAFXEventInvoked> nodeEventListeners = new List<OnAFXEventInvoked>();

        public void TriggerEvent()
        {
            for (int i = componentEventListeners.Count - 1; i >= 0; i--)
            {
                componentEventListeners[i].OnEventTriggered();
            }

            for (int i = nodeEventListeners.Count - 1; i >= 0; i--)
            {
                nodeEventListeners[i].ExecuteNode();
            }
        }

        public void AddListener(AFXEventComponent listener)
        {
            if (!Application.isPlaying) return;
            componentEventListeners.Add(listener);
        }

        public void AddListener(OnAFXEventInvoked listener)
        {
            if (!Application.isPlaying) return;
            nodeEventListeners.Add(listener);
        }

        public void RemoveListener(AFXEventComponent listener)
        {
            componentEventListeners.Remove(listener);
        }

        public void RemoveListener(OnAFXEventInvoked listener)
        {
            nodeEventListeners.Remove(listener);
        }

        public void ClearListeners()
        {
            componentEventListeners.Clear();
            nodeEventListeners.Clear();
        }
    }
}