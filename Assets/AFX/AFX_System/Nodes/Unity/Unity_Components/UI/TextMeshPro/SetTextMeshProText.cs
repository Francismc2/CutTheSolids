using UnityEngine;
using TMPro;

namespace Engage.AFX.v1
{
    [NodeTitle("Set Text")]
    [CreateNodeMenu(AFXMenuTree.ComponentUITextMeshPro + "Set TextMeshPro Text")]
    public class SetTextMeshProText : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Never)] private TextMeshPro textMeshPro;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string input;

        void SetText()
        {
            textMeshPro = GetInputValue(nameof(textMeshPro), textMeshPro);
            input = GetInputValue(nameof(input), input);
            textMeshPro.SetText(input);
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            SetText();
            base.ExecuteNode(exit);
        }
    }
}