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
            Database2 database = _process.GetDatabase2(treeAddress.DatabaseId);
            TableSchema2 schema = database.GetTable(treeAddress.TableId).Schema;

            if (CacheHasContainer(treeAddress))
            {
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema));
            }
            else
            {
                AddContainerToCache(treeAddress, database.Storage);
                result.AddRange(GetContainerFromCache(treeAddress).GetAllRows(schema));
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
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
            return new BTreeContainer(address, tree, storage);
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
