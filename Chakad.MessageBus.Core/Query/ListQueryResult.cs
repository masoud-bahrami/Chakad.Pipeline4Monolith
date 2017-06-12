using System.Collections;
using System.Collections.Generic;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Query
{
    public interface IListQueryResult : IBusinessQueryResult
    {
        int TotalCount { get; set; }
        IEnumerable GetItems();
    }


    public class ListQueryResult<T> : QueryResult, IListQueryResult
    {
        private int _totalCount = -1;
        public ICollection<T> Entities { get; set; }

        public long ElapsedTime { get; set; }

        #region IListQueryResult Members

        public IEnumerable GetItems()
        {
            return Entities;
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        #endregion

    }


}