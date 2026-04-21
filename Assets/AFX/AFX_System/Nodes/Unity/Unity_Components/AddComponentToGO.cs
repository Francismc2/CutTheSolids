using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Component + "Add Component")]
    public class AddComponentToGO : AFXActiveNode
    {
        [Header("Obj ref for Type")]
        [SerializeField]
        [Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Object objectReference;
        [SerializeField]
        [Input(ShowBackingValue.Never)] private GameObject gameObject;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private Component component;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            if (GetPort(nameof(objectReference)).IsConnected)
            {
                objectReference = GetInputValue(nameof(objectReference), objectReference);
                gameObject = GetInputValue(nameof(gameObject), gameObject);
                System.Type temp = GetPort(nameof(objectReference)).Connection.ValueType;
                component = gameObject.AddComponent(temp);
            }
            
            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return component;
        }
    }
}