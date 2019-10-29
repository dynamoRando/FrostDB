using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace FrostDB.Base
{
    public class QueryRunner : IQueryRunner
    {
        #region Public Functions
        public List<Row> Execute(List<RowValueQueryParam> parameters, List<Row> rows)
        {
            // how do we handle AND versus OR?
            var results = new List<Row>();

            Parallel.ForEach(parameters, (parameter) =>
            {
                results.AddRange(EvaluteEqualOperator(rows, parameter));
                results.AddRange(EvaluateGreaterThanOperator(rows, parameter));
                results.AddRange(EvaluateLessThanOperator(rows, parameter));
                results.AddRange(EvaluateBetweenOperator(rows, parameter));
            });

            return results;
        }

        #endregion

        #region Private Functions
        private static List<Row> EvaluateBetweenOperator(List<Row> rows, RowValueQueryParam parameter)
        {
            var results = new List<Row>();

            if (parameter.QueryType == Enum.RowValueQuery.Between)
            {
                var type = parameter.Column.DataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iMinValue = (int)parameter.MinValue;
                        var iMaxValue = (int)parameter.MaxValue;

                        var matchingRowsInt = rows.Where(row =>
                        row.Values.All(value => Convert.ToInt32(value.Value) >= iMinValue
                        && Convert.ToInt32(value.Value) <= iMaxValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsInt);

                        break;
                    case bool _ when type == typeof(float):
                        var fMinValue = (float)parameter.MinValue;
                        var fMaxValue = (float)parameter.MaxValue;

                        var matchingRowsFloat = rows.Where(row =>
                        row.Values.All(value => Convert.ToDouble(value.Value) >= fMinValue
                        && Convert.ToDouble(value.Value) <= fMaxValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsFloat);

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtMinValue = (DateTime)parameter.MinValue;
                        var dtMaxValue = (DateTime)parameter.MaxValue;

                        var matchingRowsDt = rows.Where(row =>
                        row.Values.All(value => Convert.ToDateTime(value.Value) >= dtMinValue
                        && Convert.ToDateTime(value.Value) <= dtMaxValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsDt);

                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        private static List<Row> EvaluateGreaterThanOperator(List<Row> rows, RowValueQueryParam parameter)
        {
            var results = new List<Row>();

            if (parameter.QueryType == Enum.RowValueQuery.GreaterThan)
            {
                var type = parameter.Column.DataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iValue = (int)parameter.Value;

                        var matchingRowsInt = rows.Where(row =>
                        row.Values.All(value => Convert.ToInt32(value.Value) > iValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsInt);

                        break;
                    case bool _ when type == typeof(float):
                        var fValue = (float)parameter.Value;

                        var matchingRowsFloat = rows.Where(row =>
                        row.Values.All(value => Convert.ToDouble(value.Value) > fValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsFloat);

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtValue = (DateTime)parameter.Value;

                        var matchingRowsDt = rows.Where(row =>
                        row.Values.All(value => Convert.ToDateTime(value.Value) > dtValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsDt);

                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        private static List<Row> EvaluateLessThanOperator(List<Row> rows, RowValueQueryParam parameter)
        {
            var results = new List<Row>();

            if (parameter.QueryType == Enum.RowValueQuery.LessThan)
            {
                var type = parameter.Column.DataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iValue = (int)parameter.Value;

                        var matchingRowsInt = rows.Where(row =>
                        row.Values.All(value => Convert.ToInt32(value.Value) < iValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsInt);

                        break;
                    case bool _ when type == typeof(float):
                        var fValue = (float)parameter.Value;

                        var matchingRowsFloat = rows.Where(row =>
                        row.Values.All(value => Convert.ToDouble(value.Value) < fValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsFloat);

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtValue = (DateTime)parameter.Value;

                        var matchingRowsDt = rows.Where(row =>
                        row.Values.All(value => Convert.ToDateTime(value.Value) < dtValue
                        && value.Column.Name == parameter.Column.Name)).AsParallel();

                        results.AddRange(matchingRowsDt);

                        break;
                    default:
                        break;
                }
            }

            return results;
        }
        private static List<Row> EvaluteEqualOperator(List<Row> rows, RowValueQueryParam parameter)
        {
            var results = new List<Row>();

            if (parameter.QueryType == Enum.RowValueQuery.Equals)
            {
                // is this right? will this return all rows?

                var matchingRows = rows.Where(row =>
                   row.Values.All(value => value.Value == parameter.Value
                   && value.Column.Name == parameter.Column.Name)).AsParallel();

                results.AddRange(matchingRows);
            }

            return results;
        }
        #endregion
    }
}
