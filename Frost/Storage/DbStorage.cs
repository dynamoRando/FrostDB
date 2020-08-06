﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DbStorage
    {
        #region Private Fields
        private Process _process;
        private Database _database;
        private string _databaseFolderPath = string.Empty;
        private SchemaFile _schema;
        private DbDataFile _data;
        private DbAddressBook _addressBook;
        private ParticipantFile _participants;
        private DbSecurityFile _security;
        private DbContractFile _contractFile;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbStorage(Database database, string databaseDirectory)
        {
            _database = database;
            _databaseFolderPath = databaseDirectory;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
