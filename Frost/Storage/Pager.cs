using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using C5;
using MoreLinq;

namespace FrostDB
{
    /// <summary>
    /// Responsible for returning pages from disk or cache. Also responsible for I/O operations on pages
    /// </summary>
    public class Pager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        private ConcurrentDictionary<BTreeAddress, BTreeContainer> _cache;
        private List<PageAddress> _addresses;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Pager(Process process, string databaseFolder)
        {
            _process = process;
            _databaseFolder = databaseFolder;
            _cache = new ConcurrentDictionary<BTreeAddress, BTreeContainer>();
            _addresses = new List<PageAddress>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns all rows for the specified table (via tree address)
        /// </summary>
        /// <param name="treeAddress">The tree's address (dbId, tableId)</param>
        /// <returns>All rows for the table</returns>
        public List<Row2> GetAllRows(BTreeAddress treeAddress)
        {
            var result = new List<Row2>();
            TableSchema2 schema = _process.GetDatabase2(treeAddress.DatabaseId).GetTable(treeAddress.TableId).Schema;

            BTreeContainer container = GetContainer(treeAddress);
            if (container.State == BTreeContainerState.Ready)
            {
                TreeDictionary<int, Page> tree = container.Tree;
                tree.ForEach(item =>
                {
                    List<Row2> rows = item.Value.GetValues(schema);
                    result.AddRange(rows);
                });
            }

            return result;
        }
        #endregion

        #region Private Methods
        private BTreeContainer GetContainer(BTreeAddress treeAddress)
        {
            BTreeContainer container = null;
            _cache.TryGetValue(treeAddress, out container);
            return container;
        }
        #endregion

    }
}
