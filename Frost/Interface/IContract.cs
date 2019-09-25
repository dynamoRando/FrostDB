using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IContract : IFrostObjectGet
    {
        public IDatabase Database { get; }
        public string Description { get; set; }
        public List<IDBObject> StoreObjects { get; set; }
        public List<IDBObject> InstanceObjects { get; set; }
    }
}
