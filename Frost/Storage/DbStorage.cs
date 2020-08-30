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
        private Database2 _database;
        private string _databaseFolderPath = string.Empty;
        private SchemaFile _schema;
        private DbDataFile _data;
        private ParticipantFile _participants;
        private DbSecurityFile _security;
        private DbContractFile _contractFile;
        private DbDataIndexFile _indexFile;
        private DbXactFile _xactFile;
        private string _databaseName;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbStorage(Process process, string databaseName)
        {
            _process = process;
            _databaseName = databaseName;
        }
        public DbStorage(Process process, Database2 database, string databaseDirectory)
        {
            _process = process;
            _database = database;
            _databaseFolderPath = databaseDirectory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Searches the Db Data File for the specified Page
        /// </summary>
        /// <param name="address">The page address</param>
        /// <returns>The page with the specified address</returns>
        public Page GetPageFromDisk(PageAddress address)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a page from the disk for this database
        /// </summary>
        /// <returns>A page from the disk</returns>
        public Page GetAPage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a page from disk that is not found in the specified exclusion list
        /// </summary>
        /// <param name="excludeList">A list of pages that are already in cache</param>
        /// <returns>A page</returns>
        public Page GetNextPage(List<PageAddress> excludeList)
        {
            throw new NotImplementedException();
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
