using FrostDB.Storage;
using System;
using System.Collections.Generic;

namespace FrostDB
{
    /// <summary>
    /// Class used to maintain all the needed file structures supporting a database
    /// </summary>
    public class DbStorage
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolderPath = string.Empty;
        private SchemaFile _schema;
        private DbDataFile _data;
        private DbDataDirectoryFile _dataDirectory;
        private ParticipantFile _participants;
        private DbSecurityFile _security;
        private DbContractFile _contractFile;
        private DbDataIndexFile _indexFile;
        // probably need a lock object around the xact file
        private DbXactFile _xactFile;
        private string _databaseName;
        private int _databaseId;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbStorage(Process process, string databaseName, int databaseId)
        {
            _process = process;
            _databaseName = databaseName;
            _databaseId = databaseId;
        }

        public DbStorage(Process process, string databaseName)
        {
            _process = process;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool WriteTransactionForUpdate(List<RowUpdate> rows)
        {
            _xactFile.WriteTransactionForUpdate(rows);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles adding a row to this table for this database. Will write to xact log, btree, and other files/structures as needed. This method is blocking.
        /// </summary>
        /// <param name="row">The row to add</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool WriteTransactionForInsert(RowInsert row)
        {
            bool isSuccessful;

            // note: is this the correct way to handle blocking? each object blocks for itself. should this block as one unit?
            if (_xactFile.WriteTransactionForInsert(row))
            {

                // TO DO: Need to update b-tree, indexes, etc.

                if (UpdateBTreeForInsert(row))
                { 

                }

                if (UpdateIndexesForInsert(row))
                {

                }

                _xactFile.MarkInsertXactAsReconciled(row);
            }

            throw new NotImplementedException();
        }

        public bool WriteTransactionForDelete()
        {
            _xactFile.WriteTransactionForDelete();
            throw new NotImplementedException();
        }

        public List<Row2> GetAllRows(BTreeAddress treeAddress)
        {
            return _process.DatabaseManager.StorageManager.GetAllRows(treeAddress);
        }

        /// <summary>
        /// Updates the schema file on disk
        /// </summary>
        /// <param name="schema">The schema to save to disk</param>
        public void SaveSchema(DbSchema2 schema)
        {
            _schema.Save(schema);
        }

        /// <summary>
        /// Creates the appropriate files on disk for a new database.
        /// </summary>
        public void CreateFiles()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;

            _schema = new SchemaFile(databaseFolder, _process.Configuration.SchemaFileExtension, _databaseName);
            _data = new DbDataFile(_process.Configuration.FrostBinaryDataExtension, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataExtension);
            _dataDirectory = new DbDataDirectoryFile(_data, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataDirectoryExtension);
            _participants = new ParticipantFile(_process.Configuration.ParticipantFileExtension, databaseFolder, _databaseName);
            _security = new DbSecurityFile(_process.Configuration.FrostSecurityFileExtension, databaseFolder, _databaseName);
            _contractFile = new DbContractFile(_process.Configuration.ContractExtension, databaseFolder, _databaseName);
            _indexFile = new DbDataIndexFile(_process.Configuration.FrostDbIndexFileExtension, databaseFolder, _databaseName);
            _xactFile = new DbXactFile(_process.Configuration.FrostDbXactFileExtension, databaseFolder, _databaseName);

            // is this all the files that we need?
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a populated database object by reading all files on disk.
        /// </summary>
        /// <param name="databaseName">The name of the database to get</param>
        /// <returns>A database object</returns>
        public Database2 GetDatabase(string databaseName)
        {
            _databaseName = databaseName;
            // TO DO: Build the DB fill process
            var fill = new DbFill();
            fill.Schema = GetSchema();
            _databaseId = fill.Schema.DatabaseId;
            fill.PendingParticpants = GetPendingParticipants();
            fill.AcceptedParticipants = GetAcceptedParticipants();

            return new Database2(_process, fill, this);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the db indexes if applicable for a row insert
        /// </summary>
        /// <param name="row">The row to b inserted</param>
        /// <returns>True if successful, otherwise false</returns>
        private bool UpdateIndexesForInsert(RowInsert row)
        {

            Table2 table = GetTable(row.Table.BTreeAddress);
            if (table.HasIndexes)
            {
                // need to update indexes if appropriate
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the b-tree for this row for an insert action. This will also update the data file and db data directory file if possible.
        /// </summary>
        /// <param name="insert">The row to be added</param>
        /// <returns>True if successful, otherwise false</returns>
        private bool UpdateBTreeForInsert(RowInsert insert)
        {
            return _process.DatabaseManager.StorageManager.InsertRow(insert);
        }

        /// <summary>
        /// Returns the schema for this database
        /// </summary>
        /// <returns>A Db schema</returns>
        private DbSchema2 GetSchema()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;
            var schemaFileExtension = _process.Configuration.SchemaFileExtension;

            _schema = new SchemaFile(databaseFolder, schemaFileExtension, _databaseName);
            return _schema.GetDbSchema();
        }

        /// <summary>
        /// Returns the list of accepted participants for this db
        /// </summary>
        /// <returns>A list of accepted participants</returns>
        private List<Participant2> GetAcceptedParticipants()
        {
            if (_participants is null)
            {
                LoadParticpantFile();
            }

            return _participants.GetAcceptedParticipants();
        }


        /// <summary>
        /// Returns a list of participants that are pending acceptance of the db contract
        /// </summary>
        /// <returns>A list of pending participants</returns>
        private List<Participant2> GetPendingParticipants()
        {
            if (_participants is null)
            {
                LoadParticpantFile();
            }

            return _participants.GetPendingParticipants();
        }


        /// <summary>
        /// Loads the participant file from disk
        /// </summary>
        private void LoadParticpantFile()
        {
            var extension = _process.Configuration.ParticipantFileExtension;
            _participants = new ParticipantFile(extension, _databaseFolderPath, _databaseName);
        }

        /// <summary>
        /// Gets a database based on the specified id
        /// </summary>
        /// <param name="databaseId">The dbId to get</param>
        /// <returns>The database</returns>
        private Database2 GetDatabase(int databaseId)
        {
            return _process.GetDatabase2(databaseId);
        }

        /// <summary>
        /// Gets a table based on the specified dbId and tableId
        /// </summary>
        /// <param name="databaseId">The db Id to get</param>
        /// <param name="tableId">The table Id to get</param>
        /// <returns>The table</returns>
        private Table2 GetTable(int databaseId, int tableId)
        {
            return _process.GetDatabase2(databaseId).GetTable(tableId);
        }

        /// <summary>
        /// Gets a table based on the specified BTreeAddress
        /// </summary>
        /// <param name="address">The address of the table</param>
        /// <returns>The table</returns>
        private Table2 GetTable(BTreeAddress address)
        {
            return GetTable(address.DatabaseId, address.TableId);
        }
        #endregion

    }
}
