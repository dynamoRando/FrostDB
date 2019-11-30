﻿using System;
using FrostDB.Interface;
using FrostCommon;
using FrostDB.Extensions;

namespace FrostDB
{
    public class MessageConsoleProcessor : IMessageProcessor
    {
        public void Process(IMessage message)
        {
            throw new NotImplementedException();
        }

        public static void Parse(Message message)
        {
            if (message.ReferenceMessageId.Value == Guid.Empty)
            {
                if (message.Action.Contains("Row"))
                {
                    // call RowProcessor, or whatever
                }

                if (message.Action.Contains("Contract"))
                {
                    ContractMessageProcessor.Process(message);
                }

                message.SendResponse();
            }
            else
            {
                // do nothing
            }
        }
    }
}
