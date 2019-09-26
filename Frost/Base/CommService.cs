using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class CommService : ICommService
    {
        #region Private Fields
        private IDataServer _dataServer;
        private IDataClient _dataClient;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public CommService()
        {
            _dataClient = new DataClient();
            _dataServer = new DataServer();
        }
        #endregion

        #region Public Methods
        public void StartServer()
        {
            _dataServer.Start();
        }

        public void StopServer()
        {
            _dataServer.Stop();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
