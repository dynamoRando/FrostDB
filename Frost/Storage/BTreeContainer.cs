using C5;
using MoreLinq;
using System;
using System.Collections.Generic;
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
        #endregion

        #region Public Properties
        public BTreeContainerState State => GetContainerState();
        public BTreeAddress Address => _address;
        #endregion

        #region Constructors
        public BTreeContainer() { }
        public BTreeContainer(BTreeAddress address, TreeDictionary<int, Page> tree, DbStorage storage)
        {
            _tree = tree;
            _address = address;
            _storage = storage;
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Tries to get the max row id from the tree
        /// </summary>
        /// <returns>The max row id</returns>
        public int GetMaxRowId()
        {
            // get the max page from the tree, and then iterate thru the page backwards to get the max row id
            throw new NotImplementedException();
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
                    if (!_storage.IsOpenXact(row.XactId))
                    {
                        _storage.WriteTransactionForInsert(row);
                    }

                    if (_tree.Count == 0)
                    {
                        var page = new Page(GetNextPageId(), _address.TableId, _address.DatabaseId);
                        page.AddRow(row, GetMaxRowId() + 1);
                    }

                    // need to convert an RowInsert object to a Row2 object (a byte array)



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
        public List<Row2> GetAllRows(TableSchema2 schema, bool dirtyRead)
        {
            var result = new List<Row2>();

            if (dirtyRead)
            {
                TreeDictionary<int, Page> item = _tree.DeepCopy();
                item.ForEach(i =>
                {
                    List<Row2> rows = i.Value.GetRows(schema);
                    result.AddRange(rows);
                });
            }
            else
            {
                lock (_treeLock)
                {
                    _tree.ForEach(item =>
                    {
                        List<Row2> rows = item.Value.GetRows(schema);
                        result.AddRange(rows);
                    });
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
