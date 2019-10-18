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
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Tables", Tables, typeof(List<ITable<Column, Row>>));
        }

        protected DataFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            Name = (string)serializationInfo.GetValue("Name", typeof(string));
            Tables = (List<ITable<Column, Row>>)serializationInfo.GetValue
                ("Tables", typeof(List<ITable<Column, Row>>));
        }

        /*
         * public List<Table> RegularTables {get; set;}
         * public List<CooperativeTable> CoopTables {get; set;}
         */
    }
}
