using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace FrostDB
{
    public class Database2
    {
        #region Private Fields
        private Process _process;
        private string _name;
        private List<Table2> _tables;
        private int _maxTableId;
        private int _databaseId;
        #endregion

        #region Public Properties
        public string Name => _name;
        public List<Table2> Tables => _tables;
        public int DatabaseId => _databaseId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database2(Process process, string name)
        {
            _process = process;
            _name = name;
            _databaseId = _process.DatabaseManager.GetNextDatabaseId();
        }

        public Database2(Process process, DbFill fill)
        {
            _process = process;
            _name = fill.Schema2.DatabaseName;
            _databaseId = fill.Schema2.DatabaseId;
            FillTables(fill);
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public bool HasTable(string tableName)
        {
            return Tables.Any(t => t.Name == tableName);
        }

        public bool HasTable(int tableId)
        {
            return Tables.Any(t => t.TableId == tableId);
        }

        public Table2 GetTable(string tableName)
        {
            return Tables.Where(t => t.Name == tableName).FirstOrDefault();
        }

        public Table2 GetTable(int tableId)
        {
            return Tables.Where(t => t.TableId == tableId).FirstOrDefault();
        }

        public int GetMaxTableId()
        {
            return Tables.MaxBy(t => t.TableId).First().TableId;
        }

        public int GetNextTableId()
        {
            return GetMaxTableId() + 1;
        }

        #endregion

        #region Private Methods
        private void FillTables(DbFill fill)
        {
            var schema = fill.Schema2;

            foreach (var table in schema.Tables)
            {
                this.Tables.Add(new Table2(_process, table));
            }

            throw new NotImplementedException();
        }
        #endregion

    }
}
