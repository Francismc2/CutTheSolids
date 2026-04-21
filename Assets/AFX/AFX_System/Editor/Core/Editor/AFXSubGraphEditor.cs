using UnityEditor;
namespace Engage.AFX.v1
{
    [InitializeOnLoad]
    [CustomNodeGraphEditor(typeof(AFXSubGraph))]
    public class AFXSubGraphEditor : AFXNodeGraphEditor
    {
        public override void OnGUI()
        {
            base.OnGUI();
            this.window.titleContent.text = target.name + " - SubGraph";
        }
    }
}
