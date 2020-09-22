using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace FrostDB
{
    /// <summary>
    /// A FrostDb Database
    /// </summary>
    public class Database2
    {
        /*
         * This database object will return tables and rows and is responsible for holding the
         *  - schmea
         *  - contract
         *  - list of participants
         *  - users and security
         *  
         *  This object does not actually hold any binary data. That information is handled by the Pager object (cache) and 
         *  the backing DbStorage object (to get data from disk). Those in turn, manage table data for the database in a TreeDictionary object,
         *  which is contained in a BTreeContainer used to control read/write access to the tree.
         *  
         *  In essense, the actual binary data for the database (in the tables) is contained in Binary Trees (in a TreeDictionary).
         *  Everything else is contained in this database object.
         *  
         */

        #region Private Fields
        private Process _process;
        private string _name;
        private List<Table2> _tables;
        private int _maxTableId;
        private int _databaseId;
        private DbStorage _storage;
        private DbSchema2 _schema;
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
            _storage = new DbStorage(_process, name, _databaseId);
        }

        /// <summary>
        /// Creates a database from a DBFill object. Used when booting up from disk.
        /// </summary>
        /// <param name="process">The FrostDB process.</param>
        /// <param name="fill">A DBFill object (usually loaded from disk.)</param>
        public Database2(Process process, DbFill fill, DbStorage storage)
        {
            _process = process;
            _name = fill.Schema.DatabaseName;
            _databaseId = fill.Schema.DatabaseId;
            _storage = storage;
            _schema = fill.Schema;
            FillTables(fill);
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a table to the database based on the passed in table schema
        /// </summary>
        /// <param name="schema">The table schema</param>
        public void AddTable(TableSchema2 schema)
        {
            var table = new Table2(_process, schema);
            _tables.Add(table);
            HandleAddTable(table.Name);
        }

        /// <summary>
        /// Adds a table to the database 
        /// </summary>
        /// <param name="table">The table to be added</param>
        public void AddTable(Table2 table)
        {
            if (table != null)
            {
                _tables.Add(table);
                HandleAddTable(table.Name);
            }
            else
            {
                throw new ArgumentNullException(nameof(table));
            }
        }

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
        /// Performs related actions needed when adding a table to the database
        /// </summary>
        private void HandleAddTable(string tableName)
        {
            UpdateSchema();
            _storage.SaveSchema(_schema);
            _process.Log.Debug($"{tableName} was added to database {_name}");
        }

        /// <summary>
        /// Updates the local schema reference based on the current tables and columns in memory
        /// </summary>
        private void UpdateSchema()
        {
            int colIndx = 0;

            var schema = new DbSchema2(_databaseId, _name);

            _tables.ForEach(table =>
            {
                var tableSchema = new TableSchema2(table.TableId, table.Name, _name, _databaseId, table.Columns.Length);
                table.Columns.ForEach(column =>
                {
                    var columnSchema = new ColumnSchema(column.Name, column.DataType);
                    tableSchema.Columns[colIndx] = columnSchema;
                    colIndx++;
                });
                schema.Tables.Add(tableSchema);
            });

            _schema = schema;
        }

        /// <summary>
        /// Adds the tables to the database with the passed in schema
        /// </summary>
        /// <param name="fill">A database fill object.</param>
        private void FillTables(DbFill fill)
        {
            var schema = fill.Schema;

            foreach (var table in schema.Tables)
            {
                this.Tables.Add(new Table2(_process, table));
            }

            throw new NotImplementedException();
        }
        #endregion

    }
}
