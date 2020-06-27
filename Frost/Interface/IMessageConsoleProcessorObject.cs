using FrostCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IMessageConsoleProcessorObject
    {
        IMessage Process(Message message);
    }
}
