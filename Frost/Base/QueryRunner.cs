using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class QueryRunner : IQueryRunner
    {
        #region Public Properties
        public List<Row> Execute(List<RowValueQueryParam> parameters, List<Row> rows)
        {
            // how do we handle AND versus OR?
            throw new NotImplementedException();
        }
        #endregion
    }
}
