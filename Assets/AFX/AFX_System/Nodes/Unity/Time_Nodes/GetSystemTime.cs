using UnityEngine;
using System;
using XNode;

namespace Engage.AFX.v1
{
    [CreateNodeMenu(AFXMenuTree.Time + "Get SystemTime")]
    public class GetSystemTime : AFXNode
    {
        [SerializeField][Output] private string date;
        [SerializeField][Output] private int day;
        [SerializeField][Output] private int month;
        [SerializeField][Output] private int year;
        [SerializeField][Output] private int hour;
        [SerializeField][Output] private int minute;
        [SerializeField][Output] private int second;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(date))
            {
                return DateTime.Now.ToString();
            }

            if (port.fieldName == nameof(day))
            {
                return DateTime.Now.Day;
            }

            if (port.fieldName == nameof(month))
            {
                return DateTime.Now.Month;
            }

            if (port.fieldName == nameof(year))
            {
                return DateTime.Now.Year;
            }

            if (port.fieldName == nameof(hour))
            {
                return DateTime.Now.Hour;
            }

            if (port.fieldName == nameof(minute))
            {
                return DateTime.Now.Minute;
            }

            if (port.fieldName == nameof(second))
            {
                return DateTime.Now.Second;
            }

            return null;
        }
    }
}