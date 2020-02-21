using FrostDB.Interface;
using System;
using FrostCommon;

namespace FrostDB
{
    public class DataMessageProcessor : IMessageProcessor
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
        public DataMessageProcessor()
        {
            //_dbManager = new DatabaseManager();
        }
        #endregion

        #region Public Methods
        public void Process(IMessage message)
        {
            // act on the message and send to appropriate database
            //_dbManager.AddToInbox(message);
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
