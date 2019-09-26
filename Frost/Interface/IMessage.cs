using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IMessage
    {
        public Guid Id { get; set; }
    }
}
