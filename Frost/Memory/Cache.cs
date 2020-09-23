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
    /// Responsible for maintaining in memory structures of databases
    /// </summary>
    public class Cache
    {
        #region Private Fields
        private Process _process;
        private ConcurrentDictionary<BTreeAddress, BTreeContainer> _cache;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Cache(Process process)
        {
            _process = process;
            _cache = new ConcurrentDictionary<BTreeAddress, BTreeContainer>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Attempts to insert a row into a btree
        /// </summary>
        /// <param name="insert">The row to insert</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool InsertRow(RowInsert insert)
        {
            if (insert == null)
            {
                throw new ArgumentNullException(nameof(insert));
            }

            if (_cache.ContainsKey(insert.Table.BTreeAddress))
            {
                BTreeContainer container;
                if (_cache.TryGetValue(insert.Table.BTreeAddress, out container))
                {
                    container.TryInsertRow(insert);
                }


                // try to get the tree and update it
                // the container should update the btree, the data file, and the data directory file only
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all rows for the specified table (via tree address)
        /// </summary>
        /// <param name="treeAddress">The tree's address (dbId, tableId)</param>
        /// <returns>All rows for the table</returns>
        public List<Row2> GetAllRows(BTreeAddress treeAddress)
        {
            var result = new List<Row2>();
            Database2 database = _process.GetDatabase2(treeAddress.DatabaseId);
            TableSchema2 schema = database.GetTable(treeAddress.TableId).Schema;

            if (CacheHasContainer(treeAddress))
            {
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema, false));
            }
            else
            {
                AddContainerToCache(treeAddress, database.Storage);
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema, false));
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds a container to cache from disk
        /// </summary>
        /// <param name="address">The address of this btree</param>
        /// <param name="storage">The DbStorage object where the tree will be loaded from file</param>
        private void AddContainerToCache(BTreeAddress address, DbStorage storage)
        {
            BTreeContainer container = GetContainerFromDisk(address, storage);
            _cache.TryAdd(address, container);
        }

        /// <summary>
        /// Tries to load a container from disk file
        /// </summary>
        /// <param name="address">The address of the container to get</param>
        /// <returns>A container from disk</returns>
        private BTreeContainer GetContainerFromDisk(BTreeAddress address, DbStorage storage)
        {
            var tree = new TreeDictionary<int, Page>();
            TableSchema2 schema = _process.GetDatabase2(address.DatabaseId).GetTable(address.TableId).Schema;

            // need to get the bytes from disk and build a new BTreeContainer for it
            // if the disk file is empty, just return a new btree container
            throw new NotImplementedException();
            return new BTreeContainer(address, tree, storage, schema, _process);
        }

        /// <summary>
        /// Checks the cache for the specified container
        /// </summary>
        /// <param name="address">The address of the container</param>
        /// <returns>True if the cache has the container, otherwise false</returns>
        private bool CacheHasContainer(BTreeAddress address)
        {
            return _cache.ContainsKey(address);
        }

        /// <summary>
        /// Retrives a container from the cachel
        /// </summary>
        /// <param name="treeAddress">The address of the container</param>
        /// <returns>The container</returns>
        private BTreeContainer GetContainerFromCache(BTreeAddress treeAddress)
        {
            BTreeContainer container = null;
            _cache.TryGetValue(treeAddress, out container);
            return container;
        }
        #endregion

    }
}
