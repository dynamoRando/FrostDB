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
        private DbStorage _storage;
        #endregion

        #region Public Properties
        public string Name => _name;
        public List<Table2> Tables => _tables;
        public int DatabaseId => _databaseId;
        public DbStorage Storage => _storage;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a brand new database with no tables.
        /// </summary>
        /// <param name="process">The FrostDB process.</param>
        /// <param name="name">The name of the database.</param>
        public Database2(Process process, string name)
        {
            _process = process;
            _name = name;
            _databaseId = _process.DatabaseManager.GetNextDatabaseId();
            _storage = new DbStorage(_process, name);
        }

        /// <summary>
        /// Creates a database from a DBFill object. Used when booting up from disk.
        /// </summary>
        /// <param name="process">The FrostDB process.</param>
        /// <param name="fill">A DBFill object (usually loaded from disk.)</param>
        public Database2(Process process, DbFill fill, DbStorage storage)
        {
            _process = process;
            _name = fill.Schema2.DatabaseName;
            _databaseId = fill.Schema2.DatabaseId;
            _storage = storage;
            FillTables(fill);
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines if this database contains the specified tablename.
        /// </summary>
        /// <param name="tableName">The tablename to search for.</param>
        /// <returns>True if database has the table.</returns>
        public bool HasTable(string tableName)
        {
            return Tables.Any(t => t.Name == tableName);
        }

        /// <summary>
        /// Determines if this database contains the specified tableId
        /// </summary>
        /// <param name="tableId">The tableId to search for.</param>
        /// <returns>True if the database has the table.</returns>
        public bool HasTable(int tableId)
        {
            return Tables.Any(t => t.TableId == tableId);
        }

        /// <summary>
        /// Returns the table with the specified name.
        /// </summary>
        /// <param name="tableName">The tableName to get</param>
        /// <returns>The table</returns>
        public Table2 GetTable(string tableName)
        {
            return Tables.Where(t => t.Name == tableName).FirstOrDefault();
        }

        /// <summary>
        /// Returns the table with the specified id
        /// </summary>
        /// <param name="tableId">The tableId to get</param>
        /// <returns>The table</returns>
        public Table2 GetTable(int tableId)
        {
            return Tables.Where(t => t.TableId == tableId).FirstOrDefault();
        }

        /// <summary>
        /// Returns the highest numbered tableId in this database
        /// </summary>
        /// <returns>The max id</returns>
        public int GetMaxTableId()
        {
            return Tables.MaxBy(t => t.TableId).First().TableId;
        }

        /// <summary>
        /// Returns the next table Id in line. Used for adding new tables.
        /// </summary>
        /// <returns>The next table id in line.</returns>
        public int GetNextTableId()
        {
            return GetMaxTableId() + 1;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Adds the tables to the database with the passed in schema
        /// </summary>
        /// <param name="fill">A database fill object.</param>
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
