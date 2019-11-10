using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class DataFile : IDataFile
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<BaseTable> Tables { get; set; }
        public BaseStore Store { get; set; }

        public DataFile() { }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DataFileId", Id.Value, typeof(Guid));
            info.AddValue("DataFileName", Name, typeof(string));
            info.AddValue("DataFileTables", Tables, typeof(List<BaseTable>));
            info.AddValue("DataFileStore", Tables, typeof(BaseStore));
        }

        protected DataFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid)serializationInfo.GetValue("DataFileId", typeof(Guid));
            Name = (string)serializationInfo.GetValue("DataFileName", typeof(string));
            Tables = (List<BaseTable>)serializationInfo.GetValue
                ("DataFileTables", typeof(List<BaseTable>));
            Store = (BaseStore)serializationInfo.GetValue("DataFileStore", typeof(BaseStore));
        }

        /*
         * public List<Table> RegularTables {get; set;}
         * public List<CooperativeTable> CoopTables {get; set;}
         */
    }
}
