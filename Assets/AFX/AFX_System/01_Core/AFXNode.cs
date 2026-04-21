using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu("")]
    public class AFXNode : Node
    {
        protected AFXGraph afxGraph;
        public AFXGraph Graph
        {
            get
            {
                if (afxGraph == null)
                {
                    afxGraph = graph as AFXGraph;
                }
                return afxGraph;
            }
        }

        protected string error;
        public string Error { get => error; set => error = value; }

        protected override void Init()
        {
            base.Init();
            error = null;
        }
    }
}