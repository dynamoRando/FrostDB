using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FrostCommon.ConsoleMessages;
using System.Threading.Tasks;
using System.Diagnostics;
using FrostCommon.Net;

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
        public List<string> PartialDatabaseNames { get; set; }
        public ConcurrentDictionary<string,DatabaseInfo> DatabaseInfos { get; set; }
        public ConcurrentDictionary<string, TableInfo> TableInfos { get; set; }
        public ConcurrentDictionary<string, ContractInfo> ContractInfos { get; set; }
        public ConcurrentDictionary<string, PendingContractInfo> PendingContractInfos { get; set; }
        public ConcurrentDictionary<string, AcceptedContractInfo> AcceptedContractInfos { get; set; }
        public ConcurrentDictionary<string, List<ContractInfo>> ProcessPendingContracts { get; set; }
        public ContractInfo ContractInfo { get; set; }
        public ConcurrentDictionary<Guid?, FrostPromptResponse> Responses { get; set; }
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
            PartialDatabaseNames = new List<string>();
            DatabaseInfos = new ConcurrentDictionary<string, DatabaseInfo>();
            TableInfos = new ConcurrentDictionary<string, TableInfo>();
            ContractInfos = new ConcurrentDictionary<string, ContractInfo>();
            PendingContractInfos = new ConcurrentDictionary<string, PendingContractInfo>();
            ProcessPendingContracts = new ConcurrentDictionary<string, List<ContractInfo>>();
            AcceptedContractInfos = new ConcurrentDictionary<string, AcceptedContractInfo>();
            Responses = new ConcurrentDictionary<Guid?, FrostPromptResponse>();
        }
        #endregion

        #region Public Methods
        public void AddToQueue(Guid? id)
        {
            _messageIds.Add(id);
        }
        public void RemoveFromQueue(Guid? id)
        {
            _messageIds.TryTake(out id);
        }
        public bool HasMessageId(Guid? id)
        {
            return _messageIds.TryPeek(out id);
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
