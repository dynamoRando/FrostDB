﻿using System;
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
        public DbStorage(Process process)
        {
            _process = process;
        }
        public DbStorage(Process process, Database database, string databaseDirectory)
        {
            _process = process;
            _database = database;
            _databaseFolderPath = databaseDirectory;
        }
        #endregion

        #region Public Methods
        public Database GetDatabase(string databaseName)
        {
            _databaseName = databaseName;
            // TO DO: Build the DB fill process
            var fill = new DbFill();
            fill.Schema = GetSchema();
            fill.PendingParticpants = GetPendingParticipants();
            fill.AcceptedParticipants = GetAcceptedParticipants();
            
            return new Database(_process, fill, this);
        }
        #endregion

        #region Private Methods
        private DbSchema GetSchema()
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
