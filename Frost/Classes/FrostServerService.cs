using FrostDB.Interface;
using FrostDB.Interface.IServiceResults;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FrostDB
{
    public class FrostServerService : IFrostServerService
    {
        #region Public Methods
        public IPendingContractResult AcceptContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void StartService()
        {
            IServiceCollection services = ConfigureServices(new ServiceCollection());

            // build and run service host
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<IFrostServerService>(name: "endpoint1", pipeName: "pipeName")
                .AddTcpEndpoint<IFrostServerService>(name: "endpoint2", ipEndpoint: IPAddress.Loopback, port: Process.Configuration.ServerPort)
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
                        .AddService<IFrostServerService, FrostServerService>();
                });
        }
        #endregion 
    }
}
