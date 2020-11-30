using FrostDB.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


// TO DO: Make the xaction file and the log file seperate.
// As transactions are reconciled, append them to the log file.
namespace FrostDB
{
    enum XactLineAction
    {
        Unknown,
        Insert,
        Update,
        Delete
    }

    // xact xactId tableId isReconciled <action> { Insert | Update | Delete } <data> { RowValues | RowId, RowValues | RowId }
    struct XactLine
    {
        internal Guid XactId { get; set; }
        internal int TableId { get; set; }
        internal bool IsReconciled { get; set; }
        // to do : should this be an enum?
        internal XactLineAction Action { get; set; }
        List<RowValue2> Data { get; set; }
        internal int LineNumber { get; set; }
        internal int Offset { get; set; }

        public XactLine(RowInsert row)
        {
            XactId = row.XactId;
            TableId = row.Table.TableId;
            IsReconciled = false;
            Action = XactLineAction.Insert;
            Data = row.Values;
            LineNumber = 0;
            Offset = 0;
        }

        public XactLine(string line)
        {
            var items = line.Split(" ");
            XactId = Guid.Parse(items[1]);
            TableId = Convert.ToInt32(items[2]);
            IsReconciled = Convert.ToBoolean(items[3]);
            Action = XactLineAction.Unknown;
            Data = new List<RowValue2>();

            if (items[4].Trim().Equals("Insert"))
            {
                Action = XactLineAction.Insert;
            }

            if (items[4].Trim().Equals("Update"))
            {
                Action = XactLineAction.Update;
            }

            if (items[4].Trim().Equals("Delete"))
            {
                Action = XactLineAction.Delete;
            }

            int dataStart = line.IndexOf("<data>") + 6;
            int dataEnd = line.IndexOf("</data>");

            var dataItemsLine = line.Substring(dataStart, dataEnd - dataStart).Trim();

            string[] dataItems = dataItemsLine.Split(" ");
            int x;

            for (x = 0; x < dataItems.Length; x += 2)
            {
                var dataItem = new RowValue2();
                dataItem.Column = new ColumnSchema(dataItems[x], string.Empty);
                dataItem.Value = dataItems[x + 1];
                Data.Add(dataItem);
            }

            LineNumber = 0;
            Offset = 0;
        }

        public override string ToString()
        {
            var item = new StringBuilder();

            item.Append("xact ");
            item.Append($"{XactId.ToString()} ");
            item.Append(TableId.ToString());
            item.Append($" {IsReconciled.ToString()} ");
            item.Append($"{Action.ToString("F")} ");
            item.Append("<data> ");

            Data.ForEach(value =>
            {
                item.Append($"{value.Column.Name} ");
                item.Append($"{value.Value} ");
            });

            item.Append("</data>");

            return item.ToString();
        }
    }

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

                var item = new XactLine(row);

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
            return isSuccessful;
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

            // this sucks

            string[] lines = File.ReadAllLines(FileName());

            var xacts = new List<XactLine>(lines.Length);

            foreach (var line in lines)
            {
                if (line.StartsWith("version"))
                {
                    continue;
                }

                if (line.Length == 0)
                {
                    continue;
                }

                var xact = new XactLine(line);
                if (xact.XactId == row.XactId)
                {
                    xact.IsReconciled = true;
                }

                xacts.Add(xact);
            }

            string[] linesToWrite = new string[lines.Length];

            int i = 0;
            foreach (var x in xacts)
            {
                linesToWrite[i] = x.ToString();
                i++;
            }

            File.WriteAllLines(FileName(), linesToWrite);

            // remove the xaction from the pending open transactions
            Guid value;
            isSuccessful = _unreconciledXacts.TryRemove(row.XactId, out value);

            _locker.ExitWriteLock();

            return isSuccessful;
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
