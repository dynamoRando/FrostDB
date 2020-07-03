using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using FrostCommon.Net;

namespace FrostDB
{
    public class QueryRunner : IQueryRunner
    {
        #region Public Functions
        internal FrostPromptResponse Execute(IQuery query)
        {
            return query.Execute();
        }

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
                var type = parameter.ColumnDataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iMinValue = Convert.ToInt32(parameter.MinValue);
                        var iMaxValue = Convert.ToInt32(parameter.MaxValue);

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToInt32(value.Value) >= iMinValue &&
                                    (Convert.ToInt32(value.Value) <= iMaxValue))
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(float):
                        var fMinValue = (float)parameter.MinValue;
                        var fMaxValue = (float)parameter.MaxValue;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDouble(value.Value) >= fMinValue &&
                                    (Convert.ToDouble(value.Value) <= fMaxValue))
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtMinValue = (DateTime)parameter.MinValue;
                        var dtMaxValue = (DateTime)parameter.MaxValue;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDateTime(value.Value) >= dtMinValue &&
                                    (Convert.ToDateTime(value.Value) <= dtMaxValue))
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

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
                var type = parameter.ColumnDataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iValue = (int)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToInt32(value.Value) > iValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(float):
                        var fValue = (float)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDouble(value.Value) > fValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtValue = (DateTime)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDateTime(value.Value) > dtValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

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
                var type = parameter.ColumnDataType;

                switch (true)
                {
                    case bool _ when type == typeof(int):
                        var iValue = (int)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToInt32(value.Value) < iValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(float):
                        var fValue = (float)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDouble(value.Value) < fValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

                        break;
                    case bool _ when type == typeof(DateTime):
                        var dtValue = (DateTime)parameter.Value;

                        Parallel.ForEach(rows, (row) =>
                        {
                            Parallel.ForEach(row.Values, (value) =>
                            {
                                if (value.ColumnName == parameter.ColumnName &&
                                value.ColumnType == parameter.ColumnDataType
                                )
                                {
                                    if (Convert.ToDateTime(value.Value) < dtValue)
                                    {
                                        results.Add(row);
                                    }
                                }
                            });
                        });

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
                Parallel.ForEach(rows, (row) =>
                {
                    Parallel.ForEach(row.Values, (value) =>
                    {
                        if (value.ColumnName == parameter.ColumnName &&
                       Convert.ChangeType(value.Value, value.ColumnType).Equals(
                           Convert.ChangeType(parameter.Value, parameter.ColumnDataType)))
                        {
                            results.Add(row);
                        }
                    });
                });
            }

            return results;
        }
        #endregion
    }
}
