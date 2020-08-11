using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Specifies for each row in this database if the information is local or if it is stored at a participant
    /// </summary>
    public class DbAddressBook : IStorageFile
    {
        #region Private Fields
        private string _addressBookExtension;
        private string _addressBookFolder;
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
        public DbAddressBook(string extension, string folder, string databaseName)
        {
            _addressBookExtension = extension;
            _addressBookFolder = folder;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void ParseLines()
        {
            // table tableId
            // rowId (local)
            // rowId participantId

            throw new NotImplementedException();
        }

        private List<RowReference> GetRowsForTable(string tableName)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
