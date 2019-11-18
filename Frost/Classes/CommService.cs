using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CommService : ICommService
    {
        #region Private Fields
        private IFrostClientService _clientService;
        private IFrostServerService _serverService;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public CommService()
        {
            _serverService = new FrostServerService();  
        }
        #endregion

        #region Public Methods
        public void StartServer()
        {
           
        }

        public void StopServer()
        {
           
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
