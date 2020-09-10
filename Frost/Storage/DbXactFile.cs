using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FrostDB
{
    /// <summary>
    /// The database transaction file
    /// </summary>
    class DbXactFile : IStorageFile
    {
        #region Private Fields
        private string _xactFileExtension;
        private string _xactFileFileFolder;
        private string _databaseName;
        private ReaderWriterLockSlim _locker;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a transaction log file for the specified db
        /// </summary>
        /// <param name="fileFolder">The folder in which the xact file is held</param>
        /// <param name="fileExtension">The file extension for the xact file</param>
        /// <param name="databaseName">The name of the database</param>
        public DbXactFile(string fileFolder, string fileExtension, string databaseName)
        {
            _xactFileFileFolder = fileFolder;
            _xactFileExtension = fileExtension;
            _databaseName = databaseName;
            _locker = new ReaderWriterLockSlim();

            if (!DoesFileExist())
            {
                CreateFile();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is a stub.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool WriteTransactionForUpdate(List<RowUpdate> rows)
        {
            _locker.EnterWriteLock();
            //write to file
            _locker.ExitWriteLock();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to write to the xact log for this database the row to be inserted. Will insert the xact as un-reconciled. This method is blocking.
        /// </summary>
        /// <param name="row">The row to be inserted.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool WriteTransactionForInsert(RowInsert row)
        {
            bool isSuccessful;

            if (DoesFileExist())
            {
                _locker.EnterWriteLock();

                // to do - need to come up with xact file format
                // xact tableId isReconciled <action> { Insert | Update | Delete } <data> { RowValues | RowId, RowValues | RowId }

                var item = new StringBuilder();

                item.Append("xact ");
                item.Append(row.Table.TableId.ToString());
                item.Append(" false ");
                item.Append("Insert ");
                item.Append("<data> ");

                row.Values.ForEach(value =>
                {
                    item.Append($"{value.Column.Name} ");
                    item.Append($"{value.Value} ");
                });

                item.Append(" <data>");

                using (var file = File.AppendText(FileName()))
                {
                    file.WriteLine(item.ToString());
                    file.Flush();
                }

                _locker.ExitWriteLock();

                isSuccessful = true;
            }
            else
            {
                isSuccessful = false;
            }

            // to do - is this right?
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is a stub
        /// </summary>
        /// <returns></returns>
        public bool WriteTransactionForDelete()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Checks the version number of this file. If it is not set, it will default to Version 1
        /// </summary>
        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.DATA_XACT_FILE_VERSION_1;
            }
        }

        /// <summary>
        /// Creates a new Schema file for this database
        /// </summary>
        private void CreateFile()
        {
            SetVersionNumberIfBlank();

            using (var file = new StreamWriter(FileName()))
            {
                file.WriteLine("version " + VersionNumber.ToString());
            }
        }

        /// <summary>
        /// Checks to see if the Schema file exists for this database.
        /// </summary>
        /// <returns>True if the schema file exists, otherwise false.</returns>
        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        /// <summary>
        /// Returns the schema filename for this database.
        /// </summary>
        /// <returns>Returns the schema filename for this database.</returns>
        private string FileName()
        {
            return Path.Combine(_xactFileFileFolder, _databaseName + _xactFileExtension);
        }

        #endregion

    }
}
