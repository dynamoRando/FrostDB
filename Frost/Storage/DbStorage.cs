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
        /// This method is a stub
        /// </summary>
        /// <returns></returns>
        public bool WriteTransactionForInsert()
        {
            _xactFile.WriteTransactionForInsert();
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

        public void SaveSchema(DbSchema2 schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the appropriate files on disk for a new database.
        /// </summary>
        public void CreateFiles()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;
            var schemaFileExtension = _process.Configuration.SchemaFileExtension;
            _schema = new SchemaFile(databaseFolder, schemaFileExtension, _databaseName);
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
        private DbSchema2 GetSchema()
        {
            var databaseFolder = _process.Configuration.DatabaseFolder;
            var schemaFileExtension = _process.Configuration.SchemaFileExtension;

            _schema = new SchemaFile(databaseFolder, schemaFileExtension, _databaseName);
            return _schema.GetDbSchema();
        }

        private List<Participant2> GetAcceptedParticipants()
        {
            if (_participants is null)
            {
                LoadParticpantFile();
            }

            return _participants.GetAcceptedParticipants();
        }

        private List<Participant2> GetPendingParticipants()
        {
            if (_participants is null)
            {
                LoadParticpantFile();
            }

            return _participants.GetPendingParticipants();
        }

        private void LoadParticpantFile()
        {
            var extension = _process.Configuration.ParticipantFileExtension;
            _participants = new ParticipantFile(extension, _databaseFolderPath, _databaseName);
        }
        #endregion

    }
}
