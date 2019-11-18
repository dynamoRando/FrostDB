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

        public static async void SaveRow(Location location, Guid? databaseId, Guid? tableId, Row row)
        {
            IpcServiceClient<IRemoteService> client = new IpcServiceClientBuilder<IRemoteService>()
            .UseTcp(IPAddress.Parse(location.IpAddress), location.PortNumber)
            .Build();

            await client.InvokeAsync(x => x.SaveRow(databaseId, tableId, row));

        }

        public static async void AddPendingContract(Participant participant)
        {
            IpcServiceClient<IRemoteService> client = new IpcServiceClientBuilder<IRemoteService>()
            .UseTcp(IPAddress.Parse(participant.Location.IpAddress), participant.Location.PortNumber)
            .Build();

            await client.InvokeAsync(x => x.AddPendingContract(participant.Contract));
        }
    }
}
