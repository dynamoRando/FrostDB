﻿using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon.ConsoleMessages;
using Grpc.Core;

namespace FrostCommon.Net
{
    public class GClient
    {
        #region Private Fields
        Channel _channel;
        FrostGrpcService.FrostGrpcServiceClient _client;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public 
        public Message Send(Message message)
        {
            var channel = GetChannel(message);
            var client = GetClient(channel);
            var reply = client.GetData(message.ToSkeleton());
            
            // do something with reply
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            _channel.ShutdownAsync().Wait();
        }
        #endregion

        #region Private Methods
        private Channel GetChannel(Message message)
        {
            if (_channel is null)
            {
                return _channel = new Channel(message.Destination.ConvertToString(), ChannelCredentials.Insecure);
            }
            else
            {
                return _channel;
            }
        }
        
        private FrostGrpcService.FrostGrpcServiceClient GetClient(Channel channel)
        {
            if (_client is null)
            {
                return new FrostGrpcService.FrostGrpcServiceClient(channel);
            }
            else
            {
                return _client;
            }
        }
        #endregion

    }
}
