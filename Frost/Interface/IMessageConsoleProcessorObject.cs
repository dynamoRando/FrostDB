using FrostCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IMessageConsoleProcessorObject
    {
        void Process(Message message);
    }
}
