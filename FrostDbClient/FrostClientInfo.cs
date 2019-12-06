﻿using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FrostCommon.ConsoleMessages;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FrostDbClient
{
    public class FrostClientInfo
    {
        #region Private Fields
        private ConcurrentBag<Guid?> _messageIds; // should probably create a new class called Message Queue
        #endregion

        #region Public Properties
        public Guid? ProcessId { get; set; }
        public List<string> DatabaseNames { get; set; }
        public ConcurrentDictionary<string,DatabaseInfo> DatabaseInfos { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostClientInfo()
        {
            _messageIds = new ConcurrentBag<Guid?>();
            ProcessId = Guid.NewGuid();
            DatabaseNames = new List<string>();
            DatabaseInfos = new ConcurrentDictionary<string, DatabaseInfo>();
        }
        #endregion

        #region Public Methods
        public void AddToQueue(Guid? id)
        {
            _messageIds.Add(id);
        }
        public void RemoveFromQueue(Guid? id)
        {
            Task.Run(() => _messageIds.TryTake(out id));
        }
        public bool HasMessageId(Guid? id)
        {
            return _messageIds.Any(m => m == id);
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
