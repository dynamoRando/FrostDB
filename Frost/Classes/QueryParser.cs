using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using FrostCommon.Net;

namespace FrostDB
{
    public class QueryParser : IQueryParser
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
        //static public List<RowValueQueryParam> GetParameters(string condition, Table table)
        //{
        //    var terms = GetTerms(condition);
        //    var results = GetColumnsForTerms(table, terms);
        //    var values = EvaluateTerms(terms, results);

        //    return values;
        //}

        static public List<RowValueQueryParam> GetParameters(string condition, Table table)
        {
            var terms = GetTerms(condition);
            var results = GetColumnsForTerms(table, terms);
            var values = EvaluateTerms(terms, results);

            return values;
        }

        static public bool IsValidQuery(string condition, Table table)
        {
            bool isValid = false;

            var terms = GetTerms(condition);

            isValid = terms.All(term =>
                 table.Columns.Any(column => term.Contains(column.Name,
                    StringComparison.InvariantCultureIgnoreCase))
            );

            return isValid;
        }

        static public bool IsValidCommand(string command, Process process, out Query query)
        {
            query = new Query(process);

            var commands = GetCommands(command);

            string database = commands[0];
            string statement = commands[1];

            bool hasDatabase = false;
            bool parseStatement = false;

            if (database.Contains(QueryKeywords.Use))
            {
                hasDatabase = query.TryParseDatabase(commands[0]);
            }

            query.SetQueryType(statement);

            switch (query.QueryType)
            {
                case Enum.QueryType.Select:
                    parseStatement = query.TryParseSelect(statement, query);
                    break;
                case Enum.QueryType.Insert:
                    parseStatement = query.TryParseInsert(statement, query);
                    break;
                case Enum.QueryType.Update:
                    parseStatement = query.TryParseUpdate(statement, query);
                    break;
                case Enum.QueryType.Delete:
                    parseStatement = query.TryParseDelete(statement, query);
                    break;
            }

            if (hasDatabase && parseStatement)
            {
                return true;
            }
            else
            {
                query = null;
                return false;
            }
        }

        #endregion

        #region Private Methods
        private static string[] GetCommands(string command)
        {
            return command.Split(';');
        }

        private static List<RowValueQueryParam> EvaluateTerms(List<string> terms,
            List<RowValueQueryParam> values)
        {
            var items = new List<RowValueQueryParam>();
            items.AddRange(values);

            Parallel.ForEach(terms, (term) =>
            {
                if (term.Contains('='))
                {
                    var value = items.Where(v => term.Contains(v.ColumnName))
                    .AsParallel().First();

                    value.QueryType = Enum.RowValueQuery.Equals;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.ColumnDataType);
                }

                if (term.Contains('>'))
                {
                    var value = items.Where(v => term.Contains(v.ColumnName))
                    .AsParallel().First();

                    value.QueryType = Enum.RowValueQuery.GreaterThan;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.ColumnDataType);
                }

                if (term.Contains('<'))
                {
                    var value = items.Where(v => term.Contains(v.ColumnName))
                    .AsParallel().First();

                    value.QueryType = Enum.RowValueQuery.LessThan;
                    value.Value = Convert.ChangeType
                    (GetQueryValues(term).First(), value.ColumnDataType);
                }

                if (term.Contains("BETWEEN"))
                {
                    var value = items.Where(v => term.Contains(v.ColumnName))
                    .AsParallel().First();

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

            Parallel.ForEach(terms, (term) =>
            {
                Parallel.ForEach(table.Columns, (column) =>
                {
                    if (term.Contains(column.Name))
                    {
                        results.Add(new RowValueQueryParam(column.Name, column.DataType));
                    }
                });
            });

            return results;
        }

        //private static List<RowValueQueryParam> GetColumnsForTerms(Table table, List<string> terms)
        //{
        //    var results = new List<RowValueQueryParam>();

        //    Parallel.ForEach(terms, (term) =>
        //    {
        //        Parallel.ForEach(table.Columns, (column) =>
        //        {
        //            if (term.Contains(column.Name))
        //            {
        //                results.Add(new RowValueQueryParam(column.Name, column.DataType));
        //            }
        //        });
        //    });

        //    return results;
        //}

        static private List<string> GetTerms(string query)
        {
            var stringValues = new List<string>();

            stringValues.AddRange(query.Split('(', ')').ToList());
            stringValues.RemoveAll(s => string.IsNullOrEmpty(s));

            return stringValues;
        }

        static private List<string> GetQueryValues(string query)
        {
            var stringValues = new List<string>();
            var returnStrings = new List<string>();

            var reg = new Regex("\".*?\"");
            reg.Matches(query).ToList().ForEach(m => { stringValues.Add(m.ToString().Trim()); });

            stringValues.ForEach(v => 
            {
                returnStrings.Add(v.Replace("\"", ""));
            });

            return returnStrings;
        }
        #endregion
    }
}
