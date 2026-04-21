using System;
using UnityEngine;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.EventsUnity + "Timed Update")]
    public class TimedUpdate : AFXEventNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float duration = 1f;

        private float time = 0f;
        private Action update;

        protected override void Init()
        {
            update = () => ExecuteNode(exit);
            Graph.AFXUpdate += update;
        }


        public override void ExecuteNode(AFXFlow afxFlow)
        {
            time += Time.deltaTime;

            if (time < duration) return;

            time = 0f;
            base.ExecuteNode(exit);
        }

        private void OnDestroy()
        {
            if (update == null) return;
            Graph.AFXUpdate -= update;
        }
    }
}