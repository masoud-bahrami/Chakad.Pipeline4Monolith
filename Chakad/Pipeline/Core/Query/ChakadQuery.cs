using Chakad.Pipeline.Core.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chakad.Pipeline.Core.Query
{
    public interface IChakadQuery<TOut> : IBusinessQuery<TOut>
    {
        string SearchText { get; set; }
        ICollection<SortInfo> OrderBy { get; set; }
        Page Page { get; set; }
        Filter[] Filters { get; set; }
        Sort[] Sorts { get; set; }
        string[] Groups { get; set; }
    }

    public class ChakadQuery<TOut> : IChakadQuery<TOut>
    {
        public ChakadQuery()
        {
            CorrelationId = CorrelationIdConstants.Query;
            Page = new Page
            {
                Skip = 0,
                Take = ushort.MaxValue
            };
            OrderBy = new Collection<SortInfo>();
        }

        #region IListQuery<TOut> Members
        public string SearchText { get; set; }
        public ICollection<SortInfo> OrderBy { get; set; }
        public Filter[] Filters { get; set; }
        public Sort[] Sorts { get; set; }
        public string[] Groups { get; set; }
        public Page Page { get; set; }
        public string CorrelationId { get; set; }
        #endregion
    }

    #region ~ Sort Info ~
    public class SortInfo
    {
        public string Field { get; set; }
        public bool Descending { get; set; }
        public int Order { get; set; }
    }
    #endregion

    public class Page
    {
        public ushort Skip { get; set; }
        public ushort Take { get; set; }
    }
    public class Sort
    {
        public string Dir { get; set; }
        public string Field { get; set; }
    }
    public class Filter
    {
        public class FilterVM
        {
            public string Field { get; set; }
            public string Operator { get; set; }
            public string Value { get; set; }
        }
        public FilterVM[] Filters { get; set; }
        public string Logic { get; set; }
    }
}