using FrostDB.Interface;
using FrostDB.Interface.IServiceResults;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrostDB
{
    /*
     * 
     * This service should have all the functions for communication, i.e. get row, save row, 
     * save pending contract, accept contract, etc
     * 
     */

    public class RemoteService : IRemoteService
    {
        #region Private Fields
        private IServiceCollection _services;
        private IIpcServiceHost _service;
        private CancellationTokenSource _tokenSource;
        private Task _serviceTask;
        #endregion

        #region Constructors
        public RemoteService() 
        {
            _services = ConfigureServices(new ServiceCollection());
            var host = new IpcServiceHostBuilder(_services.BuildServiceProvider());

            //host.AddNamedPipeEndpoint<IRemoteService>(name: "endpoint1", pipeName: "pipeName")
            //    .AddTcpEndpoint<IRemoteService>(name: "endpoint2", ipEndpoint: IPAddress.Loopback, port: Process.Configuration.ServerPort);

            host.AddTcpEndpoint<IRemoteService>(name: "tcpEndpoint", ipEndpoint: IPAddress.Loopback, port: Process.Configuration.ServerPort);

            _service = host.Build();
        }
        #endregion

        #region Public Methods
        public Row GetRow(Guid? databaseId, Guid? tableId, Guid? rowId)
        {
            Row result = null;

            var process = ProcessReference.Process;
            //TODO: Should check permissions, etc.
            
            if (process.HasDatabase(databaseId))
            {
                var db = process.GetDatabase(databaseId);
                if (db.HasTable(tableId))
                {
                    var table = db.GetTable(tableId);
                    if (table.HasRow(rowId))
                    {
                        result = table.GetRow(rowId);
                    }
                }
            }

            return result;
        }

        public void SaveRow(Guid? databaseId, Guid? tableId, Row row)
        {
            var process = ProcessReference.Process;

            if (process.HasDatabase(databaseId))
            {
                var db = process.GetDatabase(databaseId);
                if (db.HasTable(tableId))
                {
                    var table = db.GetTable(tableId);
                    table.AddRow(row);
                }
            }
        }
        
        public void AddPendingContract(Contract contract)
        {
            ProcessReference.Process.AddPendingContract(contract);
        }

        public void AcceptPendingContract(Participant participant)
        {
            var process = ProcessReference.Process;
            if (process.HasDatabase(participant.Contract.DatabaseId))
            {
                var db = process.GetDatabase(participant.Contract.DatabaseId);
                db.AddParticipant(participant);
            }
        }

        public async void StartService()
        {
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;
            await _service.RunAsync(token); 
        }

        public void StopService()
        {
            _tokenSource.Cancel();
        }
        #endregion

        #region Private Methods
        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services
                .AddIpc(builder =>
                {
                    builder
                        .AddNamedPipe(options =>
                        {
                            options.ThreadCount = 4;
                        })
                        .AddService<IRemoteService, RemoteService>();
                });
        }
        #endregion
    }
}
