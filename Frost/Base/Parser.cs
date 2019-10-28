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

        #region Public Methods
        static public List<IRowValue> GetValues(string condition)
        {
            throw new NotImplementedException();
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
