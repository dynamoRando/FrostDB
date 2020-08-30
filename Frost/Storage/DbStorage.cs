using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Class used to maintain all the needed file structures supporting a database
    /// </summary>
    public class DbStorage
    {
        #region Private Fields
        private Process _process;
        private Database _database;
        private string _databaseFolderPath = string.Empty;
        private SchemaFile _schema;
        private DbDataFile _data;
        private ParticipantFile _participants;
        private DbSecurityFile _security;
        private DbContractFile _contractFile;
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
        public DbStorage(Process process, Database database, string databaseDirectory)
        {
            _process = process;
            _database = database;
            _databaseFolderPath = databaseDirectory;
        }
        #endregion

        #region Public Methods
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

        private List<Participant> GetAcceptedParticipants()
        {
            if (_schema is null)
            {
                LoadParticpantFile();
            }

            return _participants.GetAcceptedParticipants();
        }

        private List<Participant> GetPendingParticipants()
        {
            if (_schema is null)
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
