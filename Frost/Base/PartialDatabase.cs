using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    /*
    [Serializable]
    public class PartialDatabase : IBaseDatabase
    {
        #region Private Fields
        private DataManager<PartialDatabase> _manager;
        private string _name;
        #endregion

        #region Public Properties
        public IContract Contract => throw new NotImplementedException();
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        public List<ITable<Column, Row>> Tables => throw new NotImplementedException();
        public DataManager<PartialDatabase> Manager { get { return _manager; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabase(string databaseName, DataManager<PartialDatabase> manager)
        {
            _name = databaseName;
            _manager = manager;
        }

        protected PartialDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            //myProperty_value = (string) info.GetValue("props", typeof(string));
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public void AddTable(ITable<Column, Row> table)
        {
            throw new NotImplementedException();
        }

        public void AddTable(ITable<Column, IRow> table)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //  info.AddValue("props", myProperty_value, typeof(string));
            throw new NotImplementedException();
        }

        public bool HasTable(string tableName)
        {
            return this.Tables.Any(t => t.Name == tableName);
        }

        public ITable<Column, Row> GetTable(string tableName)
        {
            return this.Tables.
                Where(t => t.Name == tableName).First();
        }
        #endregion

        #region Private Methods
        #endregion



    }
    */
}
