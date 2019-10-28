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
        // (FirstName = "Randy") AND (Age = "34")
        // (Age > "10") AND (Age < "50") or ...
        // (Age BETWEEN "10" AND "50")

        #region Public Methods
        static public List<IRowValue> GetValues(string condition, Table table)
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
        private static List<IRowValue> EvaluateTerms(List<string> terms, List<IRowValue> values)
        {
            var items = new List<IRowValue>();
            items.AddRange(values);

            terms.ForEach(term => 
            {
                if (term.Contains('='))
                {
                    var value = items.Where(v => term.Contains(v.Column.Name)).First();
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.Column.DataType);   
                }

                // how to handle less than, greater than, and between?
            });

            return items;
        }
        private static List<IRowValue> GetColumnsForTerms(Table table, List<string> terms)
        {
            var results = new List<IRowValue>();

            terms.ForEach(term =>
            {
                table.Columns.ForEach(column =>
                {
                    if (term.Contains(column.Name))
                    {
                        results.Add(new RowValue(column));
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
