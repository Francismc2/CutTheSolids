using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu("Register Object")]
    public class RegisterObject : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] private Object input;

        [HideInInspector]
        [SerializeField] private string key = "";
        public string Key { get => key; set => key = value; }

        private string registeredKey;
        private NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(input));
            Register();
        }

        public void Register()
        {
            if (string.IsNullOrEmpty(Key) || !inputPort.IsConnected)
            {
                CleanUp();
                return;
            }

            if (Graph.RegisteredObjects.TryAdd(Key, inputPort))
            {
                registeredKey = Key;
                Graph.RegisteredObjectAdded?.Invoke();
            }
            else
            {
                Error = "Key must be unique";
                CleanUp();
            }
        }

        /// <summary>
        /// Remove old objects if the register name gets changed.
        /// </summary>
        public void CleanUp()
        {
            if (string.IsNullOrEmpty(registeredKey)) return;
            if (inputPort.IsConnected) return;

            Graph.RegisteredObjects.Remove(registeredKey);
            registeredKey = null;
            ClearConnections();
            Graph.RegisteredObjectRemoved?.Invoke();
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            Error = null;
            if (string.IsNullOrEmpty(Key))
            {
                Error = "You must enter a key before connecting a Port";
                ClearConnections() ;
                return;
            }

            base.OnCreateConnection(from, to);
            Register();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            CleanUp();
        }

        private void OnDestroy()
        {
            CleanUp();
        }
    }
}
