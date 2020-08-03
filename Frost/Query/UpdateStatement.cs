using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FrostDB
{
    public class UpdateStatement : IStatement
    {

        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public List<string> Tables { get; set; }
        public bool HasWhereClause => CheckIfHasWhereClause();
        public string RawStatement { get; set; }
        public WhereClause WhereClause { get; set; }
        public List<UpdateStatementElement> Elements { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public string DatabaseName { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public UpdateStatement()
        {
            IsValid = true;
            Tables = new List<string>();
            WhereClause = new WhereClause();
            Elements = new List<UpdateStatementElement>();
            ErrorMessage = string.Empty;
        }

        public UpdateStatement(Process process) : this()
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void SetProcess(Process process)
        {
            _process = process;
        }

        public void ParseElements()
        {
            foreach (var element in Elements)
            {
                var items = element.RawStringWithWhitespace.Split('=');
                if (items.Length == 2)
                {
                    element.ColumnName = items[0].Trim();
                    element.Operator = "=";
                    element.Value = items[1].Trim().Replace("'", string.Empty);
                    element.DatabaseName = DatabaseName;
                    element.TableName = Tables.First();
                    SetupElement(element);
                }
            }
        }
        #endregion

        #region Private Methods
        private void SetupElement(UpdateStatementElement element)
        {
            if (element != null)
            {
                if (!string.IsNullOrEmpty(element.DatabaseName))
                {
                    if (_process.HasDatabase(element.DatabaseName))
                    {
                        var db = _process.GetDatabase(element.DatabaseName);
                        element.Database = db as Database;
                        if (db.HasTable(element.TableName))
                        {
                            element.Table = db.GetTable(element.TableName);
                            if (element.Table.HasColumn(element.ColumnName))
                            {
                                element.Column = element.Table.GetColumn(element.ColumnName);
                            }
                            else
                            {
                                IsValid = false;
                                ErrorMessage = $"Column {element.ColumnName} not found";
                            }
                        }
                        else
                        {
                            IsValid = false;
                            ErrorMessage = $"Table {element.TableName} not found";
                        }
                    }
                }
                else
                {
                    IsValid = false;
                    ErrorMessage = "Database Name not supplied";
                }
            }
        }

        private bool CheckIfHasWhereClause()
        {
            if (WhereClause.WhereClauseWithWhiteSpace.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
