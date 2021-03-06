﻿using System;
using System.Collections.Generic;
using System.Text;
using Grpc;
using Grpc.Core;
using System.Diagnostics;

namespace FrostCommon.Net
{
    public class Server
    {
        #region Private Fields
        Grpc.Core.Server _server;
        #endregion

        #region Public Properties
        public string ServerName { get; set; }
        public int PortNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public void Start(int portNumber, string ipAddress, IMessageProcessor messageProcessor)
        {
            _server = new Grpc.Core.Server
            {
                Services = { FrostGrpcService.BindService(new FrostGService(messageProcessor)) },
                Ports = { new ServerPort(ipAddress, portNumber, ServerCredentials.Insecure) }
            };
            
            _server.Start();
            Debug.WriteLine($"GServer Started for {ipAddress} : {PortNumber.ToString()}");
        }
        public void Stop()
        {
            _server.ShutdownAsync().Wait();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
