using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace FrostDB.Base
{
    public class Parser : IParser
    {
        // will work
        // (FirstName = "Randy"), (Age = "34")
        // won't work
        // (Age > "10"), (Age < "50") 
        // will work
        // (Age BETWEEN "10","50")
        // (Age > "50")
        // won't work
        // (FirstName = "Randy") OR (Age = "34")
        // won't work
        // (((FirstName = "Randy") AND (Age = "34")) OR (FirstName = "Megan"))

        #region Public Methods
        static public List<RowValueQueryParam> GetParameters(string condition, Table table)
        {
            var terms = GetTerms(condition);
            var results = GetColumnsForTerms(table, terms);
            var values = EvaluateTerms(terms, results);

            return values;
        }

        static public bool Validate(string condition, Table table)
        {
            bool isValid = false;

            var terms = GetTerms(condition);

            isValid = terms.All(term =>
                 table.Columns.Any(column => term.Contains(column.Name,
                    StringComparison.InvariantCultureIgnoreCase))
            );

            return isValid;
        }
        #endregion

        #region Private Methods
        private static List<RowValueQueryParam> EvaluateTerms(List<string> terms,
            List<RowValueQueryParam> values)
        {
            var items = new List<RowValueQueryParam>();
            items.AddRange(values);

            terms.ForEach(term =>
            {
                if (term.Contains('='))
                {
                    var value = items.Where(v => term.Contains(v.Column.Name)).First();
                    value.QueryType = Enum.RowValueQuery.Equals;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.Column.DataType);
                }

                if (term.Contains('>'))
                {
                    var value = items.Where(v => term.Contains(v.Column.Name)).First();
                    value.QueryType = Enum.RowValueQuery.GreaterThan;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.Column.DataType);
                }

                if (term.Contains('<'))
                {
                    var value = items.Where(v => term.Contains(v.Column.Name)).First();
                    value.QueryType = Enum.RowValueQuery.LessThan;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.Column.DataType);
                }

                if (term.Contains("BETWEEN"))
                {
                    var value = items.Where(v => term.Contains(v.Column.Name)).First();
                    value.QueryType = Enum.RowValueQuery.Between;
                    var ix = GetQueryValues(term);
                    value.MinValue = ix.Min();
                    value.MaxValue = ix.Max();
                }
            });

            return items;
        }
        private static List<RowValueQueryParam> GetColumnsForTerms(Table table, List<string> terms)
        {
            var results = new List<RowValueQueryParam>();

            terms.ForEach(term =>
            {
                table.Columns.ForEach(column =>
                {
                    if (term.Contains(column.Name))
                    {
                        results.Add(new RowValueQueryParam(column));
                    }
                });
            });

            return results;
        }

        static private List<string> GetTerms(string query)
        {
            var stringValues = new List<string>();

            stringValues.AddRange(query.Split('(', ')').ToList());

            return stringValues;
        }

        static private List<string> GetQueryValues(string query)
        {
            var stringValues = new List<string>();

            var reg = new Regex("\".*?\"");
            reg.Matches(query).ToList().ForEach(m => { stringValues.Add(m.ToString()); });

            return stringValues;
        }
        #endregion
    }
}
