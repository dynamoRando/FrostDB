﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseDatabase : 
        IBaseDatabase, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private List<BaseTable> _tables;
        private string _name;
        private Guid? _id;
        #endregion

        #region Public Properties
        public string Name => _name;
        public List<BaseTable> Tables => _tables;
        public Guid? Id => _id;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseDatabase()
        {
            _id = Guid.NewGuid();
            _tables = new List<BaseTable>();
        }
        public BaseDatabase(string name, BaseDataManager<IBaseDatabase> manager, Guid id,
            List<BaseTable> tables) : this(name)
        {
            _id = id;
            _tables = tables;
        }

        public BaseDatabase(string name)
        {
            _name = name;
        }

        protected BaseDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public IBaseTable GetTable(Guid? tableId)
        {
            return _tables.Where(t => t.Id == tableId).First();
        }

        public BaseTable GetTable(string tableName)
        {
            return _tables.Where(t => t.Name == tableName).First();
        }

        public bool HasTable(string tableName)
        {
            return _tables.Any(t => t.Name == tableName);
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
