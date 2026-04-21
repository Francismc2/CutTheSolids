using System;
using System.Collections.Generic;

namespace Engage.BuildTools
{
    public enum SortOption
    {
        None, ID, Name, PrettyName, Project, CreatedOn, LastUpdated, AssetType, AssetPrivacy,
        Description
    }

    public enum SortDirection
    {
        Ascending = 1,
        None = 0,
        Descending = -1
    }

    public static class SortExtensions
    {
        public static int CompareTo(this int? a, int? b)
            => a.HasValue && b.HasValue ? a.Value.CompareTo(b.Value) : a.HasValue.CompareTo(b.HasValue);
    }

    public class CollectionPresenter<T>
    {
        public SortOption currentSortOption;
        public SortDirection currentSortDirection;
        public Comparison<T> currentSort;
        public List<T> collection;

        public void Sort(bool resort = false)
        {
            Sort(collection, resort);
        }

        public void Sort(List<T> list, bool resort = false)
        {
            if (currentSort == null)
                return;

            if (!resort)
            {
                currentSortDirection = currentSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }

            list.Sort(currentSort);

            if (currentSortDirection == SortDirection.Descending)
            {
                list.Reverse();
            }
        }

        public string GetSymbol(SortOption option)
        {
            if (currentSortOption == option)
            {
                return $" {(currentSortDirection > 0 ? CreatorStyle.ASC : CreatorStyle.DESC)}";
            }

            return "";
        }
    }
}
