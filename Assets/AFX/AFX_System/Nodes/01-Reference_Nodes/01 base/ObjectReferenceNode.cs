using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [NodeTint(0.3f, 0.3f, 0.4f)]
    [CreateNodeMenu("", 0)]
    public abstract class ObjectReferenceNode : AFXNode
    {
        public const string ObjectOutPortName = "objectOut";
        public const string ValueOutPortName = "valueOut";

        public abstract System.Type MyType { get; }
        [HideInInspector]
        [SerializeField]
        protected string referenceName;
        public string ReferenceName => referenceName;

        public void SetReferenceName(string newReferenceName)
        {
            referenceName = newReferenceName;
        }
    }

    [CreateNodeMenu("", 0)]
    public class ObjectReferenceNode<T> : ObjectReferenceNode where T : Object
    {
        public override System.Type MyType => typeof(T);

        [SerializeField][Output(ShowBackingValue.Never)] private T objectOut;

        public virtual T ObjectOut
        {
            get
            {
                if (objectOut == null)
                {
                    if (this.Graph.ObjectReferencesGraph.TryGetValue(referenceName, out Object objectRef))
                    {
                        if (objectRef != null)
                        {
                            objectOut = (T)objectRef;
                        }
                    }
                }
                return objectOut;
            }
        }

        public override object GetValue(NodePort port) => ObjectOut;
    }
}