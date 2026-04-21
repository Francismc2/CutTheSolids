using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Flow + "Flow Timer")]
    public class Timer : AFXFlowNode
    {
        [SerializeField] [Input(connectionType: ConnectionType.Multiple)] private AFXFlow restart;
        [SerializeField] [Input(connectionType: ConnectionType.Multiple)] private AFXFlow pause;
        [SerializeField] [Input(connectionType: ConnectionType.Multiple)] private AFXFlow resume;
        [SerializeField] [Input(connectionType: ConnectionType.Multiple)] private AFXFlow toggle;

        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow started;
        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow tick;
        [SerializeField] [Output(connectionType: ConnectionType.Multiple)] private AFXFlow completed;

        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool loop = false;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private float duration = 1f;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private bool unscaledTime = false;

        [SerializeField] [Output] private float elapsed;
        [SerializeField] [Output] private float remaining;

        private float time;
        private bool play = true;
        private bool complete = false;

        public override object GetValue(NodePort port)
        {
            duration = GetInputValue(nameof(duration), duration);
            unscaledTime = GetInputValue(nameof(unscaledTime), unscaledTime);

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

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            PortSpecificActions(afxFlow);

            loop = GetInputValue(nameof(loop), loop);
            duration = GetInputValue(nameof(duration), duration);
            unscaledTime = GetInputValue(nameof(unscaledTime), unscaledTime);

            if (time < duration)
            {
                if (!play) return;

                if (time == 0f)
                {
                    started.ActivateNextNode(GetPort(nameof(started)));
                }

                if (unscaledTime)
                {
                    time += Time.unscaledDeltaTime;
                }
                else
                {
                    time += Time.deltaTime;
                }

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

        private void PortSpecificActions(AFXFlow afxFlow)
        {
            if (afxFlow.Port == GetPort(nameof(restart)).Connection)
            {
                Restart();
                return;
            }

            if (afxFlow.Port == GetPort(nameof(resume)).Connection)
            {
                play = true;
                return;
            }

            if (afxFlow.Port == GetPort(nameof(pause)).Connection)
            {
                play = false;
                return;
            }

            if (afxFlow.Port == GetPort(nameof(toggle)).Connection)
            {
                play = !play;
                return;
            }
        }
    }
}