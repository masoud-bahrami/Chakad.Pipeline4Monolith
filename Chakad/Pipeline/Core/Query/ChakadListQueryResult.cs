using Chakad.Pipeline.Core.Internal;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Chakad.Pipeline.Core.Query
{
    public class ChakadQueryResult<T> : QueryResult, IBusinessQueryResult
    {
        public ChakadQueryResult()
        {
            CorrelationId = CorrelationIdConstants.Query;
        }
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