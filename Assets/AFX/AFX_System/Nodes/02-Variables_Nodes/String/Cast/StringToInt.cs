using System;
using UnityEngine;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.StringCast + "Cast String to Int")]
    public class StringToInt : AFXNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private String input;

        [SerializeField][Output] private int output;

        public override object GetValue(NodePort port)
        {
            input = GetInputValue(nameof(input), input);

            if(!int.TryParse(input, out output))
            {
                error = $"[{Graph.name}] [{GetType()}] String {input} not parsable as int.";
                Debug.LogError(error);
            }

            return output;
        }
    }
}