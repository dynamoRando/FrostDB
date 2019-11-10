using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabaseManager : BaseDataManager<BasePartialDatabase>
    {
        #region Private Fields
       
        #endregion

        #region Public Properties
   
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabaseManager(PartialDatabaseFileMapper mapper, 
            string databaseFolder, string databaseExtension)
            : base(databaseFolder, databaseExtension, mapper)
        {
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion





    }
}
