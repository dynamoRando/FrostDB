using FrostDB.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private BTreeAddress _address;
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
        /// Initalizes all the dependent storage files on disk into memory
        /// </summary>
        public void Initialize()
        {
            Init();
        }

        /// <summary>
        /// This method is a stub.
        /// </summary>
        /// <returns>An array of pages.</returns>
        public Page[] GetPages()
        {
            Page[] pages = _data.GetAllPages();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a page from disk
        /// </summary>
        /// <param name="id">The id of the page to get</param>
        /// <param name="address">The btree address of the page</param>
        /// <returns>The page specified</returns>
        public Page GetPage(int id, BTreeAddress address)
        {
            return _data.GetPage(id, address);
        }

        /// <summary>
        /// Sets the databaseId for this storage value
        /// </summary>
        /// <param name="id">The id of the database</param>
        public void SetDatabaseId(int id)
        {
            _databaseId = id;
        }

        /// <summary>
        /// Returns the max page Id from the data directory file
        /// </summary>
        /// <returns>The max page id from the data directory file</returns>
        public int GetMaxPageIdFromFile()
        {
            return _dataDirectory.GetMaxPageIdInFile();
        }

        /// <summary>
        /// Returns the page ids that are still on disk. Uses the Except (opposite of Intersect) of the list provided.
        /// </summary>
        /// <param name="pagesInMemory">The page ids in memory to compare what's still on disk.</param>
        /// <returns>The page ids still on disk.</returns>
        public List<int> GetPagesLeftOnDisk(List<int> pagesInMemory)
        {
            // note: doing the UNION of the Except function of both lists means that you are possibly
            // expecting there to be pages in memory that haven't been yet saved to disk.
            // this is a situation you should make sure never happens.
            var pagesOnDisk = _dataDirectory.Lines.Select(line => line.PageNumber);
            return pagesInMemory.Except(pagesOnDisk).Union(pagesOnDisk.Except(pagesInMemory)).ToList();
        }

        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <returns>The total number of pages that are on file. Used to determine if the btree container needs to pull more pages from disk.</returns>
        public int GetTotalNumberOfDataPages()
        {
            return _dataDirectory.TotalPages;
        }

        /// <summary>
        /// Determines if the xact id is currently unreconciled
        /// </summary>
        /// <param name="id">The id of the transaction</param>
        /// <returns>True if the xact is unreconciled, otherwise false</returns>
        public bool IsPendingReconciliation(Guid id)
        {
            return _xactFile.IsPendingReconciliation(id);
        }

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

        public bool UpdateIndexes(RowInsert row)
        {
            return UpdateIndexesForInsert(row);
        }

        /// <summary>
        /// Updates the data file with the specified pages. Will overwrite the entire file with the pages provided.
        /// </summary>
        /// <param name="pages">The pages to write to the data file.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool UpdateDataFile(Page[] pages)
        {
            return _data.WritePages(pages);
        }

        /// <summary>
        /// Records this insert action in the xact log. Will record it as unreconciled against cache.
        /// </summary>
        /// <param name="row">The row to record</param>
        /// <returns>True if successful in writing to log, otherwise false</returns>
        public bool RecordTransactionInLog(RowInsert row)
        {
            return _xactFile.WriteTransactionForInsert(row);
        }

        /// <summary>
        /// Updates this insert action in the xact log that it has been reconciled against cache.
        /// </summary>
        /// <param name="row">The row that was reconciled</param>
        /// <returns>True if successful in writing to log, otherwise false</returns>
        public bool MarkTransactionAsReconciledInLog(RowInsert row)
        {
            return _xactFile.MarkInsertXactAsReconciled(row);
        }

        public bool WriteTransactionForDelete()
        {
            _xactFile.WriteTransactionForDelete();
            throw new NotImplementedException();
        }

        public List<RowStruct> GetAllRows(BTreeAddress treeAddress)
        {
            // to do: we need to make sure we also account for getting remote rows
            throw new NotImplementedException();
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
        public void CreateFiles(int databaseId)
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;

            _schema = new SchemaFile(databaseFolder, _process.Configuration.SchemaFileExtension, _databaseName, databaseId);
            _data = new DbDataFile(_process.Configuration.FrostBinaryDataExtension, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataExtension);
            _dataDirectory = new DbDataDirectoryFile(_data, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataDirectoryExtension);
            _participants = new ParticipantFile(_process.Configuration.ParticipantFileExtension, databaseFolder, _databaseName);
            _security = new DbSecurityFile(_process.Configuration.FrostSecurityFileExtension, databaseFolder, _databaseName);
            _contractFile = new DbContractFile(_process.Configuration.ContractExtension, databaseFolder, _databaseName);
            _indexFile = new DbDataIndexFile(_process.Configuration.FrostDbIndexFileExtension, databaseFolder, _databaseName);
            _xactFile = new DbXactFile(databaseFolder, _process.Configuration.FrostDbXactFileExtension, _databaseName);
        }

        /// <summary>
        /// Returns a populated database object by reading all files on disk.
        /// </summary>
        /// <param name="databaseName">The name of the database to get</param>
        /// <returns>A database object</returns>
        public Database2 GetDatabase(string databaseName)
        {
            _databaseName = databaseName;
            var fill = new DbFill();
            fill.Schema = GetSchema();
            _databaseId = fill.Schema.DatabaseId;
            fill.PendingParticpants = GetPendingParticipants();
            fill.AcceptedParticipants = GetAcceptedParticipants();

            return new Database2(_process, fill, this);
        }

        /// <summary>
        /// Adds the page to the data directory file and data file
        /// </summary>
        /// <param name="page">The page to add</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool AddPage(Page page)
        {
            return _data.AddPage(page);
        }

        /// <summary>
        /// Attempts to reconcile the page against disk. (This may be throwaway).
        /// </summary>
        /// <param name="page">The page to reconcile.</param>
        public void UpdatePageOnDisk(Page page)
        {
            // to do: Update the page on disk; and if the line number changes update the data directory.
            throw new NotImplementedException();
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

            //throw new NotImplementedException();
            return true;
        }

        /// <summary>
        /// Returns the schema for this database
        /// </summary>
        /// <returns>A Db schema</returns>
        private DbSchema2 GetSchema()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;
            var schemaFileExtension = _process.Configuration.SchemaFileExtension;

            _schema = new SchemaFile(databaseFolder, schemaFileExtension, _databaseName, _databaseId);
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

        private void Init()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;

            _schema = new SchemaFile(databaseFolder, _process.Configuration.SchemaFileExtension, _databaseName, _databaseId);
            _data = new DbDataFile(_process.Configuration.FrostBinaryDataExtension, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataExtension);
            _dataDirectory = new DbDataDirectoryFile(_data, databaseFolder, _databaseName, _process.Configuration.FrostBinaryDataDirectoryExtension);
            _participants = new ParticipantFile(_process.Configuration.ParticipantFileExtension, databaseFolder, _databaseName);
            _security = new DbSecurityFile(_process.Configuration.FrostSecurityFileExtension, databaseFolder, _databaseName);
            _contractFile = new DbContractFile(_process.Configuration.ContractExtension, databaseFolder, _databaseName);
            _indexFile = new DbDataIndexFile(_process.Configuration.FrostDbIndexFileExtension, databaseFolder, _databaseName);
            _xactFile = new DbXactFile(databaseFolder, _process.Configuration.FrostDbXactFileExtension, _databaseName);
        }
        #endregion

    }
}
