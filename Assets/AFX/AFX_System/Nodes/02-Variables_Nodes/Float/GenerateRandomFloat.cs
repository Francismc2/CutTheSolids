using UnityEngine;
using XNode;
namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Float + "Generate Random Float")]
    public class GenerateRandomFloat : AFXActiveNode
    {
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float min;
        [SerializeField]
        [Input(ShowBackingValue.Unconnected)] private float max;

        [SerializeField]
        [Output(ShowBackingValue.Never)] private float output;

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