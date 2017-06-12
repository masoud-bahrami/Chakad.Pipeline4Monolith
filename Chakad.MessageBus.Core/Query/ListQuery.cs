using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Chakad.Pipeline.Core.Query
{
    public interface IListQuery<TOut> : IBusinessQuery<TOut> where TOut : IListQueryResult
    {
        int Skip { get; set; }
        int Take { get; set; }
        string SearchText { get; set; }
        ICollection<SortInfo> OrderBy { get; set; }
    }
    public class ListQuery<TOut> : IListQuery<TOut> where TOut : IListQueryResult
    {
        public ListQuery()
        {
            Skip = 0;
            Take = int.MaxValue;
            OrderBy = new Collection<SortInfo>();
        }

        #region IListQuery<TOut> Members

        public int Skip { get; set; }
        public int Take { get; set; }
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