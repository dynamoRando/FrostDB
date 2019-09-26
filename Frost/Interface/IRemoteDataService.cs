using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Interface
{
    public interface IRemoteDataService
    {
        void SendMessage(IMessage message);
        IMessage GetData(IMessage message);
        Task<IMessage> GetDataAsync(IMessage message);
    }
}
