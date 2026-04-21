using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Text + "Set text")]
    public class SetText : AFXActiveNode
    {
        [SerializeField] [Input(ShowBackingValue.Never)] private Text textComponent;
        [SerializeField] [Input(ShowBackingValue.Unconnected)] private string text;
        [SerializeField] [Output(ShowBackingValue.Never)] private string output;

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            textComponent = GetInputValue(nameof(textComponent), textComponent);
            text = GetInputValue(nameof(text), text);

            output = textComponent.text;
            textComponent.text = text;

            base.ExecuteNode(exit);
        }

        public override object GetValue(NodePort port)
        {
            return output;
        }
    }
}