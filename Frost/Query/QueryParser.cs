using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using FrostCommon.Net;
using FrostDB;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace FrostDB
{
    /// <summary>
    /// Deprecated. Will be replaced by QueryManager
    /// </summary>
    public class QueryParser : IQueryParser
    {
        private Process _process;
       
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

        #region Constructors
        public QueryParser(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        //static public List<RowValueQueryParam> GetParameters(string condition, Table table)
        //{
        //    var terms = GetTerms(condition);
        //    var results = GetColumnsForTerms(table, terms);
        //    var values = EvaluateTerms(terms, results);

        //    return values;
        //}

        public List<RowValueQueryParam> GetParameters(string condition, Table table)
        {
            var terms = GetTerms(condition);
            var results = GetColumnsForTerms(table, terms);
            var values = EvaluateTerms(terms, results);

            return values;
        }

        public bool IsValidQuery(string condition, Table table)
        {
            bool isValid = false;

            var terms = GetTerms(condition);

            isValid = terms.All(term =>
                 table.Columns.Any(column => term.Contains(column.Name,
                    StringComparison.InvariantCultureIgnoreCase))
            );

            return isValid;
        }

        internal bool IsValidCommand(string command, Process process, out IQuery query)
        {
            var commands = GetCommands(command);

            string database = commands[0];
            string statement = commands[1];

            bool processHasDatabase = false;
            bool canParseStatement = false;

            var item = SetQueryType(statement);

            if (database.Contains(QueryKeywords.Use))
            {
                processHasDatabase = TryParseDatabase(commands[0], item);
            }

            switch (item)
            {
                case SelectQuery s:
                    canParseStatement = s.IsValid(statement);
                    break;
                case InsertQuery i:
                    canParseStatement = i.IsValid(statement);
                    break;
                case UpdateQuery u:
                    canParseStatement = u.IsValid(statement);
                    break;
                case DeleteQuery d:
                    canParseStatement = d.IsValid(statement);
                    break;
            }

            if (processHasDatabase && canParseStatement)
            {
                query = item;
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
      
        private IQuery SetQueryTypeAntlr(string statement)
        {
            throw new NotImplementedException();
        }

        private IQuery SetQueryType(string statement)
        {
            if (statement.Contains(QueryKeywords.Select, StringComparison.OrdinalIgnoreCase))
            {
                return new SelectQuery(_process);
            }

            if (statement.Contains(QueryKeywords.Insert, StringComparison.OrdinalIgnoreCase))
            {
                return new InsertQuery(_process);
            }

            if (statement.Contains(QueryKeywords.Update, StringComparison.OrdinalIgnoreCase))
            {
                return new UpdateQuery(_process);
            }

            if (statement.Contains(QueryKeywords.Delete, StringComparison.OrdinalIgnoreCase))
            {
                return new DeleteQuery(_process);
            }

            return null;

        }
        private bool TryParseDatabase(string useStatement, IQuery query)
        {
            var items = useStatement.Split(" ");
            var dbName = items[1];

            if (_process.HasDatabase(dbName))
            {
                query.DatabaseName = dbName;
                return true;
            }
            else
            {
                return false;
            }
        }
        private string[] GetCommands(string command)
        {
            return command.Split(';');
        }

        private List<RowValueQueryParam> EvaluateTerms(List<string> terms,
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

        private List<RowValueQueryParam> GetColumnsForTerms(Table table, List<string> terms)
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

        private List<string> GetTerms(string query)
        {
            var stringValues = new List<string>();

            stringValues.AddRange(query.Split('(', ')').ToList());
            stringValues.RemoveAll(s => string.IsNullOrEmpty(s));

            return stringValues;
        }

        private List<string> GetQueryValues(string query)
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
