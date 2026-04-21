using UnityEngine;
using XNode;

namespace Engage.AFX.v1.Instantiate
{
    [NodeTitle("GameObject Instantiate")]
    [CreateNodeMenu(AFXMenuTree.Instantiate + "GameObject Instantiate")]
    public class GameObjectInstantiate : AFXActiveNode
    {
        [SerializeField][Input(ShowBackingValue.Unconnected)] private GameObject prefab;
        [SerializeField][Input(ShowBackingValue.Never)] private Transform parent;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 spawnPosition;
        [SerializeField][Input(ShowBackingValue.Never)] private Quaternion spawnRotation;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private Vector3 spawnScale = Vector3.one;
        [SerializeField][Input(ShowBackingValue.Unconnected)] private string newObjName;

        [SerializeField][Output] private GameObject obj;

        public override object GetValue(NodePort port)
        {
            return obj;
        }

        public override void ExecuteNode(AFXFlow afxFlow)
        {
            prefab = GetInputValue(nameof(prefab), prefab);
            parent = GetInputValue(nameof(parent), parent);
            spawnPosition = GetInputValue(nameof(spawnPosition), spawnPosition);
            spawnRotation = GetInputValue(nameof(spawnRotation), spawnRotation);
            spawnScale = GetInputValue(nameof(spawnScale), spawnScale);
            newObjName = GetInputValue(nameof(newObjName), newObjName);

            obj = Instantiate<GameObject>(prefab, spawnPosition, spawnRotation);

            if (!System.String.IsNullOrEmpty(newObjName))
            {
                obj.name = newObjName;
            }

            if (parent != null) obj.transform.SetParent(parent);

            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;
            obj.transform.localScale = spawnScale;

            base.ExecuteNode(exit);
        }
    }
}