using C5;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Class that holds the address of a tree as well as the tree itself and the tree's state (future)
    /// </summary>
    class BTreeContainer
    {
        #region Private Fields
        private readonly object _treeLock = new object();
        private readonly object _stateLock = new object();
        TreeDictionary<int, Page> _tree;
        BTreeContainerState _state;
        BTreeAddress _address;
        #endregion

        #region Public Properties
        public BTreeContainerState State => _state;
        public BTreeAddress Address => _address;
        #endregion

        #region Constructors
        public BTreeContainer() { }
        public BTreeContainer(BTreeAddress address, TreeDictionary<int, Page> tree)
        {
            _tree = tree;
            _address = address;
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

            if (_state == BTreeContainerState.Ready)
            {
                SetContainerState(BTreeContainerState.LockedForUpdate);

                // using the values passed in, update the tree

                SetContainerState(BTreeContainerState.Ready);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all rows from this tree in this container
        /// </summary>
        /// <param name="schema">The table schema to convert the rows to</param>
        /// <returns>A list of rows</returns>
        public List<Row2> GetAllRows(TableSchema2 schema)
        {
            var result = new List<Row2>();

            lock (_treeLock)
            {
                _tree.ForEach(item =>
                {
                    List<Row2> rows = item.Value.GetValues(schema);
                    result.AddRange(rows);
                });
            }

            return result;
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
