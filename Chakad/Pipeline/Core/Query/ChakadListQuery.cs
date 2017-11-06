using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chakad.Pipeline.Core.Query
{
    public interface IChakadListQuery<TOut> : IBusinessQuery<TOut> where TOut : IChakadListQueryResult
    {
        int Page { get; set; }
        int Count { get; set; }
        string SearchText { get; set; }
        ICollection<SortInfo> OrderBy { get; set; }
    }
    public class ChakadListQuery<TOut> : IChakadListQuery<TOut> where TOut : IChakadListQueryResult
    {
        public ChakadListQuery()
        {
            Page = 1;
            Count = int.MaxValue;
            OrderBy = new Collection<SortInfo>();
        }

        #region IListQuery<TOut> Members

        public int Page { get; set; }
        public int Count { get; set; }
        public string SearchText { get; set; }
        public ICollection<SortInfo> OrderBy { get; set; }
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
}