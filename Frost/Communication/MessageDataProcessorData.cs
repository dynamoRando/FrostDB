﻿using FrostDB.Interface;
using System;
using FrostCommon;

namespace FrostDB
{
    public class MessageDataProcessorData : IMessageProcessor
    {
        #region Private Fields
        private DatabaseManager _dbManager;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessorData()
        {
            //_dbManager = new DatabaseManager();
        }
        #endregion

        #region Public Methods
        public IMessage Process(IMessage message)
        {
            // act on the message and send to appropriate database
            //_dbManager.AddToInbox(message);
            throw new NotImplementedException();
        }

        public IMessage ProcessWithResult(IMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
