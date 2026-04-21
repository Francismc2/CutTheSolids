using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow + "Flow Simple Timer")]
    public class SimpleTimer : AFXFlowNode
    {
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool loop = false;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float duration = 1f;

        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow started;
        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow tick;
        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow completed;

        [SerializeField] [Output] private float elapsed;
        [SerializeField] [Output] private float remaining;

        private float time;
        private bool play = true;
        private bool complete = false;

        public override object GetValue(NodePort port)
        {
            duration = GetInputValue(nameof(duration), duration);

            if (port.fieldName == nameof(elapsed))
            {
                return time;
            }

            if (port.fieldName == nameof(remaining))
            {
                return duration - time;
            }

            return null;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            loop = GetInputValue(nameof(loop), loop);
            duration = GetInputValue(nameof(duration), duration);

            if (time < duration)
            {
                if (!play) return;

                if (time == 0f)
                {
                    started.ActivateNextNode(GetPort(nameof(started)));
                }

                time += Time.deltaTime;
                tick.ActivateNextNode(GetPort(nameof(tick)));
            }
            else
            {
                if (complete) return;

                time = duration;
                complete = true;
                completed.ActivateNextNode(GetPort(nameof(completed)));

                if (loop)
                {
                    Restart();
                }
            }
        }

        private void Restart()
        {
            time = 0;
            play = true;
            complete = false;
            started.ActivateNextNode(GetPort(nameof(started)));
        }

        public override object GetField(NodePort port)
        {
            return port.fieldName switch
            {
                nameof(enter) => enter,
                nameof(started) => started,
                nameof(tick) => tick,
                nameof(completed) => completed,
                _ => null
            };
        }
    }
}