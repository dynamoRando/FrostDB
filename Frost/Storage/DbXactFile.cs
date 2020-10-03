using FrostDB.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FrostDB
{
    /*
     * Note: need to make sure that I don't confuse myself. Beginning and Ending a Transaction is not the same
     * as recording and reconciling a transaction.
     * 
     * Beginning and ending a transaction is an ACID property.
     * 
     * Recording and reconciling a transaction is the process by which you sync in-memory to disk.
     */

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
        private ConcurrentDictionary<Guid, Guid> _unreconciledXacts;
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
            _unreconciledXacts = new ConcurrentDictionary<Guid, Guid>();

            if (!DoesFileExist())
            {
                CreateFile();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Determines if the xact specified has not been reconciled with the data file
        /// </summary>
        /// <param name="id">The id of the xact</param>
        /// <returns>True if the xact has not been reconciled, otherwise false</returns>
        public bool IsPendingReconciliation(Guid id)
        {
            return _unreconciledXacts.ContainsKey(id);
        }

        /// <summary>
        /// This method is a stub.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool WriteTransactionForUpdate(List<RowUpdate> rows)
        {

            foreach (var row in rows)
            {
                if (IsPendingReconciliation(row.XactId))
                {
                    throw new InvalidOperationException($"xact {row.XactId.ToString()} is already in progress");
                }
            }

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

            if (IsPendingReconciliation(row.XactId))
            {
                throw new InvalidOperationException($"xact {row.XactId.ToString()} is already in progress");
            }

            if (DoesFileExist())
            {
                _locker.EnterWriteLock();

                _unreconciledXacts.TryAdd(row.XactId, row.XactId);

                // to do - need to come up with xact file format
                // xact xactId tableId isReconciled <action> { Insert | Update | Delete } <data> { RowValues | RowId, RowValues | RowId }

                var item = new StringBuilder();

                item.Append("xact ");
                item.Append($"{row.XactId.ToString()} ");
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

        /// <summary>
        /// Marks a insert xaction as reconciled in the file
        /// </summary>
        /// <param name="row">The row that was to be inserted</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool MarkInsertXactAsReconciled(RowInsert row)
        {
            bool isSuccessful;
            _locker.EnterWriteLock();

            // TO DO: Need to actually update the file.


            // remove the xaction from the pending open transactions
            Guid value;
            isSuccessful = _unreconciledXacts.TryRemove(row.XactId, out value);

            _locker.ExitWriteLock();
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
