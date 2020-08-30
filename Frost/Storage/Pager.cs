using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Responsible for returning pages from disk
    /// </summary>
    public class Pager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        private Dictionary<PageAddress, Page> _cache;
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
        }
        #endregion

        #region Public Methods
        public Page GetPage(PageAddress address)
        {
            return GetPageFromCacheOrDisk(address);
        }

        public Page GetPage(string databaseName, string tableName, int pageId)
        {
            throw new NotImplementedException();
        }

        public Page GetPage(int databaseId, int tableId, int pageId)
        {
            return GetPageFromCacheOrDisk(new PageAddress { DatabaseId = databaseId, TableId = tableId, PageId = pageId });
        }
        #endregion

        #region Private Methods
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
            }

            return page;
        }

        private Page GetPageFromDisk(PageAddress address)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
