using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostCommon.Net
{
    public class FrostGService : FrostGrpcService.FrostGrpcServiceBase
    {
        private readonly ILogger<FrostGService> _logger;
        private IMessageProcessor _messageProcessor;

        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostGService() { }
        public FrostGService(ILogger<FrostGService> logger)
        {
            _logger = logger;
        }

        public FrostGService(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public FrostGService(ILogger<FrostGService> logger, IMessageProcessor messageProcessor)
        {
            _logger = logger;
            _messageProcessor = messageProcessor;
        }
        #endregion

        #region Public Methods
        public void SetProcessor(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public override Task<MessageSkeleton> GetData(MessageSkeleton request, ServerCallContext context)
        {
            Message result = new Message();
            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Text))
                {
                    string content = request.Text;
                    Message message;

                    if (Json.TryParse(content, out message))
                    {
                        result = (Message)_messageProcessor.Process(message);
                    }
                }
            }
            return Task.FromResult(result.ToSkeleton());
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
