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
    class BTreeContainer
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

        public bool TryUpdateRows(List<RowUpdate> rows)
        {
            bool result = false;

            if (GetContainerState() == BTreeContainerState.Ready)
            {
                SetContainerState(BTreeContainerState.LockedForUpdate);

                lock (_treeLock)
                {
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
                    List<Row2> rows = i.Value.GetValues(schema);
                    result.AddRange(rows);
                });
            }
            else
            {
                lock (_treeLock)
                {
                    _tree.ForEach(item =>
                    {
                        List<Row2> rows = item.Value.GetValues(schema);
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
        #endregion


    }
}
