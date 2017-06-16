using System.Collections;
using System.Collections.Generic;
using Chakad.Pipeline.Core.Message;

namespace Chakad.Pipeline.Core.Query
{
    public interface IChakadListQueryResult : IBusinessQueryResult
    {
        int TotalCount { get; set; }
        IEnumerable GetItems();
    }


    public class ChakadListQueryResult<T> : QueryResult, IChakadListQueryResult
    {
        private int _totalCount = -1;
        public ICollection<T> Entities { get; set; }

        public long ElapsedTime { get; set; }

        #region IChakadListQueryResult Members

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