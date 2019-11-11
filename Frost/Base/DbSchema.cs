﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class DbSchema : ISchema, ISerializable
    {
        #region Private Fields
        private Guid? _schemaVersion;
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location Location { get; set; }
        public List<TableSchema> Tables { get; set; }
        public Guid? Version => _schemaVersion;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected DbSchema(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            DatabaseName = (string)serializationInfo.GetValue
             ("SchemaDatabaseName", typeof(string));
            DatabaseId = (Guid?)serializationInfo.GetValue
                ("SchemaDatabaseId", typeof(Guid?));
            Location = (Location)serializationInfo.GetValue
                ("SchemaDatabaseLocation", typeof(Location));
            _schemaVersion = (Guid?)serializationInfo.GetValue
                ("SchemaVersion", typeof(Guid?));
            Tables = (List<TableSchema>)serializationInfo.GetValue
            ("SchemaTables", typeof(List<TableSchema>));
        }
        public DbSchema() 
        {
            _schemaVersion = Guid.NewGuid();
            Tables = new List<TableSchema>();
        }

        public DbSchema(BaseDatabase database)
        {
            Map(database);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void Map(BaseDatabase database)
        {
            DatabaseName = database.Name;
            DatabaseId = database.Id;
            database.Tables.ForEach(table => Tables.Add(table.Schema));
            Location = (Location)Process.GetLocation();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SchemaDatabaseName", DatabaseName, typeof(string));
            info.AddValue("SchemaDatabaseId", DatabaseId, typeof(Guid?));
            info.AddValue("SchemaDatabaseLocation", Location, typeof(Location));
            info.AddValue("SchemaVersion", Version, typeof(Guid?));
            info.AddValue("SchemaTables", Tables,
                typeof(List<TableSchema>));
        }
        #endregion
    }
}