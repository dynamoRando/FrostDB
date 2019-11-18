using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class DataFile : IDataFile
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public DbSchema Schema { get; set; }
        public DataFile() { }
        public List<Participant> Participants { get; set; }
        public Contract Contract { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DataFileId", Id.Value, typeof(Guid));
            info.AddValue("DataFileName", Name, typeof(string));
            info.AddValue("DataFileTables", Tables, typeof(List<Table>));
            info.AddValue("DataFileSchema", Schema, typeof(DbSchema));
            info.AddValue("DataFileParticipants", Schema, typeof(List<Participant>));
            info.AddValue("DataFileContract", Contract, typeof(Contract));
        }

        protected DataFile(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid)serializationInfo.GetValue("DataFileId", typeof(Guid));
            Name = (string)serializationInfo.GetValue("DataFileName", typeof(string));
            Tables = (List<Table>)serializationInfo.GetValue
                ("DataFileTables", typeof(List<Table>));
            Schema = (DbSchema)serializationInfo.GetValue
                ("DataFileSchema", typeof(DbSchema));
            Participants = (List<Participant>)serializationInfo.GetValue
               ("DataFileParticipants", typeof(List<Participant>));
            Contract = (Contract)serializationInfo.GetValue
               ("DataFileContract", typeof(Contract));
        }

        /*
         * public List<Table> RegularTables {get; set;}
         * public List<CooperativeTable> CoopTables {get; set;}
         */
    }
}
