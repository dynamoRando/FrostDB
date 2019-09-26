using FrostDB.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class RemoteDataService : IRemoteDataService
    {
        #region Private Fields
        private ICommService _commService;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public RemoteDataService(ICommService service)
        {
            _commService = service;
        }
        #endregion

        #region Public Methods
        public IMessage GetData(IMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<IMessage> GetDataAsync(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
