﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private Dictionary<PageAddress, Page> _cache;
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
            _cache = new Dictionary<PageAddress, Page>();
            _addresses = new List<PageAddress>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a page from cache or disk based on the page address
        /// </summary>
        /// <param name="address">The page address</param>
        /// <returns>A page from cache or disk</returns>
        public Page GetPage(PageAddress address)
        {
            return GetPageFromCacheOrDisk(address);
        }

        /// <summary>
        /// Gets a page from cache or disk based on the specified parameters
        /// </summary>
        /// <param name="databaseName">The database name</param>
        /// <param name="tableName">The table name</param>
        /// <param name="pageId">The page Id</param>
        /// <returns>A page from cache or disk</returns>
        public Page GetPage(string databaseName, string tableName, int pageId)
        {
            var db = _process.GetDatabase2(databaseName);
            var dbId = db.DatabaseId;
            var table = db.GetTable(tableName);
            var tableId = table.TableId;

            return GetPage(dbId, tableId, pageId);
        }

        /// <summary>
        /// Gets a page from cache or disk based on the specified parameters
        /// </summary>
        /// <param name="databaseId">The database id</param>
        /// <param name="tableId">The table id</param>
        /// <param name="pageId">The page id</param>
        /// <returns>A page from cache or disk</returns>
        public Page GetPage(int databaseId, int tableId, int pageId)
        {
            return GetPageFromCacheOrDisk(new PageAddress { DatabaseId = databaseId, TableId = tableId, PageId = pageId });
        }
        #endregion

        #region Private Methods
        private void AddToCache(Page page)
        {
            _cache.Add(page.Address, page);
            _addresses.Add(page.Address);
        }

        private Page GetPageFromCacheOrDisk(PageAddress address)
        {
            Page page = null;
            if (_cache.ContainsKey(address))
            {
                _cache.TryGetValue(address, out page);
            }
            else
            {
                page = GetPageFromDisk(address);
                AddToCache(page);
            }

            return page;
        }

        private Page GetPageFromDisk(PageAddress address)
        {
            var addresses = _addresses.
                Where(address => address.DatabaseId == address.DatabaseId && address.TableId == address.TableId).ToList();

            return _process.GetDatabase2(address.DatabaseId).Storage.GetNextPage(addresses);
        }
        #endregion

    }
}
