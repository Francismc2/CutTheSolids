using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{

    public class DataGrid<T>
    {
        public List<T> SourceList { get; }

        private GUIStyle rowStyleEven;
        private GUIStyle rowStyleOdd;

        public GUIStyle RowStyleEven
        {
            get
            {
                if (rowStyleEven == null)
                {
                    rowStyleEven = CreatorStyle.CreateStyle(background: CreatorStyle.ListEvenTone);
                }

                return rowStyleEven;
            }
            set => rowStyleEven = value;
        }
        public GUIStyle RowStyleOdd
        {
            get
            {
                if (rowStyleOdd == null)
                {
                    rowStyleOdd = CreatorStyle.CreateStyle(background: CreatorStyle.ListOddTone);
                }

                return rowStyleOdd;
            }
            set => rowStyleOdd = value;
        }

        public List<DataGridColumn<T>> Columns { get; } = new List<DataGridColumn<T>>();

        public Func<T, Color> RowBackgroundColorCoding { get; set; }

        public DataGrid(List<T> sourceList)
        {
            SourceList = sourceList;
        }

        public Vector2 DrawGrid(Vector2 scrollPosition)
        {
            using (var bundlePanelContentFrame = new EditorGUILayout.VerticalScope())
            {
                DrawHeaders(SourceList.Count > 0);

                using (var scrollArea = new GuiTools.ScrollArea(ref scrollPosition, EditorStyles.helpBox))
                {
                    DrawData();
                }
            }

            return scrollPosition;
        }

        public void DrawColumnHeader(DataGridColumn<T> column)
        {
            string header = column.SortDirection == SortDirection.None ? column.HeaderLabel : $"{column.HeaderLabel} {column.ToSymbol(column.SortDirection)}";

            if (GUILayout.Button(header, column.HeaderStyle, column.GetHeaderLayoutOptions()))
            {
                if (column.Sortable)
                {
                    var sortDirection = column.SortDirection;

                    Columns.ForEach(col => col.SortDirection = SortDirection.None);
                    SourceList.Sort(column.Sort);

                    if (sortDirection != SortDirection.Descending)
                    {
                        SourceList.Reverse();
                        column.SortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        column.SortDirection = SortDirection.Ascending;
                    }
                }

                column.OnHeaderClick?.Invoke();
            }
        }

        public void DrawHeaders(bool headersEnabled = true)
        {
            using (var columnHeaders = new EditorGUILayout.HorizontalScope())
            {
                using (var enabled = new GuiTools.EnabledScope(headersEnabled))
                {
                    foreach(var header in Columns)
                    {
                        DrawColumnHeader(header);
                    }
                }
            }
        }

        public void DrawData()
        {
            using (var bundleListPanel = new EditorGUILayout.VerticalScope())
            {
                for (int i = 0; i < SourceList.Count; i++)
                {
                    using (var localScope = new GuiTools.ColorScope(backgroundColor: RowBackgroundColorCoding?.Invoke(SourceList[i])))
                    {
                        DrawRow(SourceList[i], i % 2 == 0 ? RowStyleEven : RowStyleOdd);
                    }
                }
            }
        }

        public void DrawRow(T item, GUIStyle style)
        {
            using (var rowScope = new EditorGUILayout.HorizontalScope(style))
            {
                foreach (var column in Columns)
                {
                    column.DrawCell?.Invoke(column, item);
                }
            }
        }
    }
}
