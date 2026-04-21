using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public class DataGridColumn<T>
    {
        protected string headerLabel;

        public string Ascending { get; set; } = CreatorStyle.ASC;
        public string Descending { get; set; } = CreatorStyle.DESC;
        public string Flat { get; set; } = "";

        public string ToSymbol(SortDirection direction) => direction == 0 ? Flat : direction > 0 ? Ascending : Descending;

        public string Name { get; set; }
        public string HeaderLabel
        {
            get => headerLabel ?? Name;
            set => headerLabel = value;
        }

        public SortDirection SortDirection { get; set; }

        public GUIStyle HeaderStyle { get; set; } = EditorStyles.boldLabel;
        public Action OnHeaderClick { get; set; }
        public virtual Action<DataGridColumn<T>, T> DrawCell { get; set; }

        public virtual Comparison<T> Sort { get; set; }
        public bool Sortable => Sort != null;

        public List<GUILayoutOption> HeaderLayoutOptions { get; set; } = new List<GUILayoutOption>();
        public List<GUILayoutOption> CellLayoutOptions { get; set; } = new List<GUILayoutOption>();

        public float Width { get; set; } = 100f;

        public GUILayoutOption[] GetHeaderLayoutOptions()
        {
            HeaderLayoutOptions.Add(GUILayout.Width(Width));
            return HeaderLayoutOptions.ToArray();
        }

        public GUILayoutOption[] GetCellLayoutOptions()
        {
            CellLayoutOptions.Add(GUILayout.Width(Width));
            return CellLayoutOptions.ToArray();
        }
    }
}
