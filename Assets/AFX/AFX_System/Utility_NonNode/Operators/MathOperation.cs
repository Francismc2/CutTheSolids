using XNode;

namespace Engage.AFX.v1.Math.Operation
{
    [CreateNodeMenu("")]
    public class MathOperation<OperationProvider> : AFXNode where OperationProvider : IOperationProvider, new()
    {
        private const string PortNameIn1 = "A";
        private const string PortNameIn2 = "B";
        private const string PortNameOut = "Output";
        private NodePort port1;
        private NodePort port2;

        private IOperation operation;
        private IOperationProvider OpProvider { get; } = new OperationProvider();

        protected override void Init()
        {
            base.Init();
            port1 = this.InitPort<object>(PortNameIn1);
            port2 = this.InitPort<object>(PortNameIn2);
        }

        public override object GetValue(NodePort port)
        {
            object a;
            object b;
            if (!port1.TryGetInputValue(out a) || !port2.TryGetInputValue(out b)) return null;
            if (operation == null) { operation = this.OpProvider.GetOperation(a, b); }
            return operation.DoOperation(a, b);
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            Error = null;
            base.OnCreateConnection(from, to);
            if (to.fieldName is PortNameIn1 or PortNameIn2)
            {
                SetupPorts();
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            Error = null;
            base.OnRemoveConnection(port);
        }

        private void SetupPorts()
        {
            if (!port1.IsConnected || !port2.IsConnected) return;

            Error = null;
            System.Type outputType = this.OpProvider.GetOutputType(port1.Connection.ValueType, port2.Connection.ValueType);

            if (outputType == null)
            {
                Error = "Unsupported Types";
                return;
            }

            this.SwapDynamicOutputPortWithNewType(PortNameOut, outputType);
        }
    }
}