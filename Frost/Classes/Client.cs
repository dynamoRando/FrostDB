using FrostDB.Interface;
using JKang.IpcServiceFramework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB
{
    public class Client : IClient
    {
        public static async Task<Row> GetRow(Location location, Guid? databaseId, Guid? tableId, Guid? rowId)
        {
            IpcServiceClient<IRemoteService> client = new IpcServiceClientBuilder<IRemoteService>()
       .UseTcp(IPAddress.Parse(location.IpAddress), location.PortNumber)
       .Build();

            var result = await client.InvokeAsync(x => x.GetRow(databaseId, tableId, rowId));

            return result;
        }
    }
}
