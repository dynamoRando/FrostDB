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
        public List<ITable<Column, Row>> Tables { get; set; }

        public DataFile() { }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DataFileId", Id.Value, typeof(Guid));
            info.AddValue("DataFileName", Name, typeof(string));
            info.AddValue("DataFileTables", Tables, typeof(List<ITable<Column, Row>>));
        }

        protected DataFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid)serializationInfo.GetValue("DataFileId", typeof(Guid));
            Name = (string)serializationInfo.GetValue("DataFileName", typeof(string));
            Tables = (List<ITable<Column, Row>>)serializationInfo.GetValue
                ("DataFileTables", typeof(List<ITable<Column, Row>>));
        }

        /*
         * public List<Table> RegularTables {get; set;}
         * public List<CooperativeTable> CoopTables {get; set;}
         */
    }
}
