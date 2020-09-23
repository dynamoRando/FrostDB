using C5;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Holds the address of a tree, the tree itself, and the tree's state (future)
    /// </summary>
    public class BTreeContainer
    {
        #region Private Fields
        private readonly object _treeLock = new object();
        private readonly object _stateLock = new object();
        TreeDictionary<int, Page> _tree;
        BTreeContainerState _state;
        BTreeAddress _address;
        DbStorage _storage;
        TableSchema2 _schema;
        Process _process;
        #endregion

        #region Public Properties
        public BTreeContainerState State => GetContainerState();
        public BTreeAddress Address => _address;
        #endregion

        #region Constructors
        public BTreeContainer() { }
        public BTreeContainer(BTreeAddress address, TreeDictionary<int, Page> tree, DbStorage storage, TableSchema2 schema, Process process)
        {
            _tree = tree;
            _address = address;
            _storage = storage;
            _schema = schema;
            _process = process;
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Tries to get the max row id from the tree
        /// </summary>
        /// <returns>The max row id</returns>
        public int GetMaxRowId()
        {
            if (_tree.Count == 0)
            {
                return 0;
            }
            else
            {
                int maxPageId = _tree.FindMax().Key;

                Page maxPage;
                _tree.Find(ref maxPageId, out maxPage);
                return maxPage.GetMaxRowId();
            }
        }

        /// <summary>
        /// Sets the state of the container
        /// </summary>
        /// <param name="state">The state to set</param>
        public void SetContainerState(BTreeContainerState state)
        {
            lock (_stateLock)
            {
                _state = state;
            }
        }

        /// <summary>
        /// Tries to add the row data to the btree and also update the data file and db directory data file on disk
        /// </summary>
        /// <param name="row">The row to be added</param>
        /// <returns>True if the operation was successful, otherwise false</returns>
        public bool TryInsertRow(RowInsert row)
        {

            if (row.Table.TableId != _address.TableId || row.Table.DatabaseId != _address.DatabaseId)
            {
                throw new InvalidOperationException("attempted to add row to incorrect btree");
            }

            bool result = false;

            if (GetContainerState() == BTreeContainerState.Ready)
            {
                SetContainerState(BTreeContainerState.LockedForInsert);

                lock (_treeLock)
                {
                    // assume that we've already checked for this in Table2.cs
                    // this is a design change where we will let the Structure (Database / Table) orchestrate the 
                    // reconciliation between cache and disk

                    if (_tree.Count == 0)
                    {
                        var page = new Page(GetNextPageId(), _address.TableId, _address.DatabaseId, _schema, _process);
                        if (page.AddRow(row, GetMaxRowId() + 1))
                        {
                            _tree.Add(page.Id, page);
                        };
                    }
                    else
                    {

                        // need to find the next available Page in the tree that has room for the row
                        // then add the row to that page
                    }

                    // need to go ahead and update the tree and also the data file and db directory file
                }

                SetContainerState(BTreeContainerState.Ready);
            }

            return result;
        }

        /// <summary>
        /// This method is a stub.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public bool TryUpdateRows(List<RowUpdate> rows)
        {
            bool result = false;

            if (GetContainerState() == BTreeContainerState.Ready)
            {
                SetContainerState(BTreeContainerState.LockedForUpdate);

                lock (_treeLock)
                {
                    // note: is this pattern below correct? In Table2 we already updated the xact log for insert.
                    // should the container not worry about the xact log? and only care about the data file and data db directory?

                    // write to the transaction log first
                    _storage.WriteTransactionForUpdate(rows);

                    // using the values passed in, update the tree

                }

                SetContainerState(BTreeContainerState.Ready);
            }

            return result;
        }

        /// <summary>
        /// Returns all rows from this tree in this container
        /// </summary>
        /// <param name="schema">The table schema to convert the rows to</param>
        /// <param name="dirtyRead">true if unprotected read, otherwise false</param>
        /// <returns>A list of rows</returns>
        public RowStruct[] GetAllRows(TableSchema2 schema, bool dirtyRead)
        {
            RowStruct[] result;
            int totalRows = 0;

            lock (_treeLock)
            {
                _tree.ForEach(item =>
                {
                    totalRows += item.Value.TotalRows;
                });
            }

            result = new RowStruct[totalRows];
            var resultSpan = new Span<RowStruct>(result);

            int i = 0;
            if (dirtyRead)
            {
                TreeDictionary<int, Page> item = _tree.DeepCopy();

                foreach (var x in item)
                {
                    RowStruct[] rows = x.Value.GetRows(schema);
                    i += rows.Length;
                    Array.Copy(rows, 0, result, i, rows.Length);
                }
            }
            else
            {
                lock (_treeLock)
                {
                    foreach (var item in _tree)
                    {
                        RowStruct[] rows = item.Value.GetRows(schema);
                        i += rows.Length;
                        Array.Copy(rows, 0, result, i, rows.Length);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Private Methods
        private BTreeContainerState GetContainerState()
        {
            lock (_stateLock)
            {
                return _state;
            }
        }
        private Page GetFirstPageFromDisk()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the next page id in the tree
        /// </summary>
        /// <returns>The next page id in the tree</returns>
        private int GetNextPageId()
        {
            lock (_treeLock)
            {
                if (_tree.Count == 0)
                {
                    return 1;
                }
                else
                {
                    int maxValue = 0;
                    foreach (var page in _tree.Values)
                    {
                        if (page.Id > maxValue)
                        {
                            maxValue = page.Id;
                        }
                    }
                    return maxValue + 1;
                }
            }
        }
        #endregion


    }
}
