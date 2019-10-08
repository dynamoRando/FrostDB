using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabase : IDatabase
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public IContract Contract => throw new NotImplementedException();
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        List<ITable<Column, IRow>> IDatabase.Tables => throw new NotImplementedException();
        #endregion

        #region Events
        #endregion

        #region Constructors
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
        #endregion

        #region Private Methods
        #endregion



    }
}
