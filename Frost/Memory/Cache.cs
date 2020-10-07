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

            bool isSuccessful = false;

            BTreeContainer container;
            BTreeAddress address = insert.Table.BTreeAddress;

            if (_cache.ContainsKey(insert.Table.BTreeAddress))
            {
               
                if (_cache.TryGetValue(address, out container))
                {
                    isSuccessful = container.TryInsertRow(insert);
                }
            }
            else
            {
                AddContainerToCache(address);
                if (_cache.TryGetValue(address, out container))
                {
                    isSuccessful = container.TryInsertRow(insert);
                }
            }

            return isSuccessful;
        }

        /// <summary>
        /// Attempts to save the specified B-tree to disk
        /// </summary>
        /// <param name="address">The specific b-tree to save</param>
        /// <returns>True if successful, otherwise false.</returns>
        public bool SyncTreeToDisk(BTreeAddress address)
        {
            bool isSuccessful = false;
            BTreeContainer container;
            if (_cache.TryGetValue(address, out container))
            {
                container.SetContainerState(BTreeContainerState.LockedForStorageSync);
                isSuccessful = container.SyncToDisk();
                container.SetContainerState(BTreeContainerState.Ready);
            }
            return isSuccessful;
        }

        public BTreeContainer GetTree(BTreeAddress address)
        {
            BTreeContainer container;
            _cache.TryGetValue(address, out container);
            return container;
        }

        /// <summary>
        /// Returns all rows for the specified table (via tree address)
        /// </summary>
        /// <param name="treeAddress">The tree's address (dbId, tableId)</param>
        /// <returns>All rows for the table</returns>
        public List<RowStruct> GetAllRows(BTreeAddress treeAddress)
        {
            var result = new List<RowStruct>();
            Database2 database = _process.GetDatabase2(treeAddress.DatabaseId);
            TableSchema2 schema = database.GetTable(treeAddress.TableId).Schema;

            if (CacheHasContainer(treeAddress))
            {
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema, false));
            }
            else
            {
                AddContainerToCache(treeAddress);
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema, false));
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds a container to cache from disk. If this is a new table, will add a new container to the cache.
        /// </summary>
        /// <param name="address">The address of this btree</param>
        private void AddContainerToCache(BTreeAddress address)
        {
            BTreeContainer container = GetContainerFromDisk(address);
            _cache.TryAdd(address, container);
        }

        /// <summary>
        /// Tries to load a container from disk file. If this is a fresh database, will return a new container.
        /// </summary>
        /// <param name="address">The address of the container to get</param>
        /// <returns>A container from disk</returns>
        private BTreeContainer GetContainerFromDisk(BTreeAddress address)
        {
            Database2 db = _process.GetDatabase2(address.DatabaseId);
            DbStorage storage = db.Storage;
            var tree = new TreeDictionary<int, Page>();
            TableSchema2 schema = db.GetTable(address.TableId).Schema;

            // get the first page
            Page page = storage.GetPage(1, address);

            // if this is a brand new table
            if (page == null)
            {
                page = new Page(1, address.TableId, address.DatabaseId, schema, _process);
            }

            tree.Add(page.Id, page);

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
