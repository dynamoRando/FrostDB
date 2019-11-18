using FrostDB.Interface;
using FrostDB.Interface.IServiceResults;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
        public IRegisterNewPartialDatabaseResult RegisterNewPartialDatabase(Contract contract)
        {
            throw new NotImplementedException();
        }

        public IAddRowToPartialDatabaseResult AddRowToPartialDatabase(Participant sourceParticipant, Guid? DatabaseId, Row row)
        {
            throw new NotImplementedException();
        }
        public IPendingContractResult AcceptContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void StartService()
        {
            IServiceCollection services = ConfigureServices(new ServiceCollection());

            // build and run service host
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<IRemoteService>(name: "endpoint1", pipeName: "pipeName")
                .AddTcpEndpoint<IRemoteService>(name: "endpoint2", ipEndpoint: IPAddress.Loopback, port: Process.Configuration.ServerPort)
                .Build()
                .Run();
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
