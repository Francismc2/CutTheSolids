using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Int + "Generate Random Int")]
    public class GenerateRandomInt : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int min;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private int max;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private int output;

        public override object GetValue(NodePort port)
        {
            return output;
        }

        void GenerateRandomValue()
        {
            min = GetInputValue(nameof(min), min);
            max = GetInputValue(nameof(max), max);
            output = Random.Range(min, max);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            GenerateRandomValue();
            base.ExecuteNode(exit);
        }
    }
}