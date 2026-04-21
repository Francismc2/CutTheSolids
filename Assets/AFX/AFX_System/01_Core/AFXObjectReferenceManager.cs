using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Engage.AFX.v1
{
    public class AFXObjectReferenceManager : MonoBehaviour
    {
        [SerializeField]
        private List<ObjectReferenceSlot> objectRefsEngine = new();
        public List<ObjectReferenceSlot> ObjectRefsEngine { get => objectRefsEngine; set => objectRefsEngine = value;}

        private AFXEngine afxEngine;
        public AFXEngine AfxEngine
        {
            get
            {
                if (afxEngine == null)
                {
                    afxEngine = GetComponent<AFXEngine>();
                }

                return afxEngine;
            }
        }

        public string AddReferenceSlot(string refName, UnityEngine.Object value, Type slotType)
        {
            
            foreach (var item in ObjectRefsEngine)
            {
                //there is reference slot of that name already added
                if (item.ReferenceName == refName)
                {
                    //but it's value is empty so fill it with the new ref node's value. Only if it's the same Type
                    if (item.ReferenceValue == null && item.SlotType == slotType)
                    {
                        item.ReferenceValue = value;
                        Debug.Log($"[{this.name}]: Added {refName} Slot to Manager");
                        return item.ReferenceName;
                    }
                    else
                    {
                        // We already have this object added. skip.
                        Debug.Log($"[{this.name}]: {refName} Slot already found in Manager");
                        string newName = refName;
#if UNITY_EDITOR
                        newName = ObjectNames.GetUniqueName(ReferenceNamesToArray(ObjectRefsEngine), refName);
                        ObjectReferenceSlot temp = new ObjectReferenceSlot(newName, value, value.GetType());
                        ObjectRefsEngine.Add(temp);
#endif
                        return newName;
                    }
                }
            }
            // The name is unique, add the slot.
            ObjectRefsEngine.Add(new ObjectReferenceSlot(refName, value, value.GetType()));
            Debug.Log($"[{this.name}]: Added {refName} Slot to Manager");

            return refName;
        }

        private string[] ReferenceNamesToArray(List<ObjectReferenceSlot> listOfSlots)
        {
            List<string> names = new List<string>();

            foreach (var item in listOfSlots)
            {
                names.Add(item.ReferenceName);
            }

            return names.ToArray();
        }

        public void RefreshReferenceSlots()
        {
            //get main graph obj ref nodes
            if (AfxEngine.AFXNodeGraph == null) return;

            var objRefNodes = GetObjectReferenceNodes();
            ObjectRefsEngine = CleanAndSortReferences(objRefNodes);
        }

        public void ResetReferenceSlots()
        {
            ObjectRefsEngine.Clear();
            RefreshReferenceSlots();
        }

        private List<ObjectReferenceNode> GetObjectReferenceNodes()
        {
            List<ObjectReferenceNode> objRefNodes = new List<ObjectReferenceNode>();

            foreach (ObjectReferenceNode graphRef in AfxEngine.AFXNodeGraph.nodes.Where(node => node is ObjectReferenceNode))
            {
                objRefNodes.Add(graphRef);
            }

            return objRefNodes;
        }

        private List<ObjectReferenceSlot> CleanAndSortReferences(List<ObjectReferenceNode> objRefNodesIn)
        {
            List<ObjectReferenceSlot> tempSlotList = new List<ObjectReferenceSlot>(ObjectRefsEngine);
            // List to store already filled slots.
            List<ObjectReferenceSlot> oldlist = new List<ObjectReferenceSlot>();
            foreach (ObjectReferenceSlot managerRef in tempSlotList)
            {
                foreach (ObjectReferenceNode graphRef in objRefNodesIn)
                {
                    if (DuplicateNameInListCheck(graphRef, oldlist) || string.IsNullOrEmpty(graphRef.ReferenceName))
                    {
                        continue;
                    }

                    if ((managerRef.ReferenceName == graphRef.ReferenceName) && managerRef.ReferenceValue != null)
                    {
                        oldlist.Add(managerRef);
                    }
                }
            }

            tempSlotList.Clear();
            tempSlotList.AddRange(oldlist);
            
            foreach (ObjectReferenceNode refNode in objRefNodesIn)
            {
                if (DuplicateNameInListCheck(refNode, tempSlotList) || string.IsNullOrEmpty(refNode.ReferenceName))
                {
                    continue;
                }

                tempSlotList.Add(new ObjectReferenceSlot(refNode.ReferenceName, null, refNode.MyType));
            }

            tempSlotList.Sort();
            return tempSlotList;
        }

        bool DuplicateNameInListCheck(ObjectReferenceNode refNode, List<ObjectReferenceSlot> slotList)
        {
            foreach (ObjectReferenceSlot slot in slotList)
            {
                if (refNode.ReferenceName == slot.ReferenceName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}