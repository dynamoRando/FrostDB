using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class ParticipantFile : IStorageFile
    {
        #region Private Fields
        private string _participantFileExtension;
        private string _participantFileFolder;
        private string _databaseName;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ParticipantFile(string extension, string folder, string databaseName)
        {
            _participantFileExtension = extension;
            _participantFileFolder = folder;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        public void Load()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
