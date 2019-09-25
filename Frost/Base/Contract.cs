using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Contract : IContract
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public IDatabase Database => throw new NotImplementedException();
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IDBObject> StoreObjects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IDBObject> InstanceObjects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
