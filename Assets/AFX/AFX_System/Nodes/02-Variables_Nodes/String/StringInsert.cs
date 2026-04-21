using UnityEngine;
using UnityEngine.Windows;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.String + "String Insert")]
    public class StringInsert : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string insert;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private int index;

        [SerializeField][Output] private string output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);
            insert = GetInputValue(nameof(insert), insert);
            index = GetInputValue(nameof(index), index);

            int clampedIndex = Mathf.Clamp(index, 0, input.Length - 1);

            if (index < input.Length && index > -1)
            {
                error = $"[{Graph.name}] [{GetType()}] Index: {index} out of range. Index clamped to {clampedIndex}";
            }

            return input.Insert(clampedIndex, insert);
        }
    }
}