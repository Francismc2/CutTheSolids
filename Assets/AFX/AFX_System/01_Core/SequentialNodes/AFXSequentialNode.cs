namespace Engage.AFX.v1
{
    public abstract class AFXSequentialNode : AFXNode, IExecutableNode
    {
        public abstract void ExecuteNode(AFXFlow afxFlow);
    }

    public interface IExecutableNode
    {
        void ExecuteNode(AFXFlow afxFlow);
    }
}