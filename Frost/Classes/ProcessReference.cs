using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public static class ProcessReference
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public static Process Process { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static Contract GetContract(Guid? databaseId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).Contract;
        }
        public static IDatabase GetDatabase(Guid? databaseId) 
        {
            return ProcessReference.Process.GetDatabase(databaseId);
        }

        public static Table GetTable(Guid? databaseId, Guid? tableId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).GetTable(tableId);
        }

        public static Row GetRow(Guid? databaseId, Guid? tableId, Guid? rowId)
        {
            return ProcessReference.Process.GetDatabase(databaseId).GetTable(tableId).GetRow(rowId);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
