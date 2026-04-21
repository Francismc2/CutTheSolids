using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using XNodeEditor;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
namespace Engage.AFX.v1
{
    public class AFXNodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private Dictionary<Type, string> nodeInfo = new Dictionary<Type, string>();
        private Vector2 windowPosition;
        private AFXGraph graph;

        public void Setup (Dictionary<Type, string> nodeInfoIn, AFXGraph graphIn,  Vector2 windowPos)
        {
            nodeInfo = nodeInfoIn;
            graph = graphIn;
            windowPosition = windowPos;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
            
            searchList.Add(new SearchTreeGroupEntry(new GUIContent("Add Nodes"), 0) );

            List<string> groups = new List<string>();
            Dictionary<Type, string> nodes = Sort(nodeInfo);
            foreach (KeyValuePair<Type, string> item in nodes)
            {
                string[] entryTitle = item.Value.Split('/');
                
                string groupName = "";
                for (int i = 0; i < entryTitle.Length - 1; i++)
                {
                    groupName += entryTitle[i];
                    if (!groups.Contains(groupName))
                    {
                        SearchTreeGroupEntry group = new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 1);
                        searchList.Add(group);
                        groups.Add(groupName);
                    }
                    groupName += "/";
                }
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last()));
                entry.level = entryTitle.Length;
                entry.userData = item.Key;
                searchList.Add(entry);
            }
            return searchList;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            CreateNode((Type)SearchTreeEntry.userData, windowPosition);
            return true;
        }
        
        public XNode.Node CreateNode(Type type, Vector2 position)
        {
            Undo.RecordObject(graph, "Create Node");
            XNode.Node node = graph.AddNode(type);
            Undo.RegisterCreatedObjectUndo(node, "Create Node");
            node.position = position;
            if (node.name == null || node.name.Trim() == "") node.name = NodeEditorUtilities.NodeDefaultName(type);
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(graph))) AssetDatabase.AddObjectToAsset(node, graph);
            if (NodeEditorPreferences.GetSettings().autoSave) AssetDatabase.SaveAssets();
            NodeEditorWindow.RepaintAll();
            return node;
        }

        private Dictionary<Type, string> Sort(Dictionary<Type, string> dictionary)
        {
            var sortedEntries = dictionary.OrderBy(entry => entry.Value, Comparer<string>.Create((value1, value2) =>
            {
                string[] splits1 = value1.Split('/');
                string[] splits2 = value2.Split('/');

                for (var i = 0; i < splits1.Length; i++)
                {
                    if (i >= splits2.Length)
                        return 1;

                    var result = splits1[i].CompareTo(splits2[i]);

                    if (result != 0)
                    {
                        // Make sure that leaves go after nodes
                        if (splits1.Length != splits2.Length && (i == splits1.Length - 1 || i == splits2.Length - 1))
                            return splits1.Length < splits2.Length ? 1 : -1; // ? -1 : 1; would be groups on bottom instead
                        return result;
                    }
                }
                return 0;
            }));

            var orderedDictionary = new Dictionary<Type, string>(sortedEntries);
            return orderedDictionary;
        }
    }
}
