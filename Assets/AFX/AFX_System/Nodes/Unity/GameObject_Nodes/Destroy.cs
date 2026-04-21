using UnityEngine;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.GameObject+ "GameObject Destroy")]
    public class Destroy : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Never, typeConstraint = TypeConstraint.None)] private Object input;

        void DestroyObject()
        {
            input = GetInputValue(nameof(input), input);
            if (input!=null)
            {
                Destroy(input);
            }
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            DestroyObject();
            base.ExecuteNode(exit);
        }
    }
}