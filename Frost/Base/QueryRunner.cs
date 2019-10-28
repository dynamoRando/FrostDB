using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class QueryRunner : IQueryRunner
    {
        #region Public Properties
        public List<Row> Execute(List<RowValueQueryParam> parameters, List<Row> rows)
        {
            // how do we handle AND versus OR?

            var results = new List<Row>();

            parameters.ForEach(parameter =>
            {
                if (parameter.QueryType == Enum.RowValueQuery.Equals)
                {
                    // is this right? will this return all rows?

                    var matchingRows = rows.Where(row =>
                       row.Values.All(value => value.Value == parameter.Value
                       && value.Column.Name == parameter.Column.Name));

                    results.AddRange(matchingRows);
                }
            });

            throw new NotImplementedException();
        }
        #endregion
    }
}
