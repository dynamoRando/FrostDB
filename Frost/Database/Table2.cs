using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Table2
    {
        #region Private Fields
        private Process _process;
        private BTreeDictionary<int, Page> _btree;
        private int _maxPageId;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table2()
        {
            _btree = new BTreeDictionary<int, Page>();
        }

        public Table2(Process process, TableSchema2 schema) : this()
        {
            // need to figure out what the new b-tree structure will look like
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
