using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class DatabaseManager : DataManager<Database>
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DatabaseManager(DatabaseFileMapper mapper, string databaseFolder,
            string databaseExtension) : base(databaseFolder, databaseExtension, mapper)
        {
        }

        public DatabaseManager() : base()
        {
        }
        #endregion

        #region Public Methods
       
        #endregion

        #region Private Methods
        #endregion




    }
}
