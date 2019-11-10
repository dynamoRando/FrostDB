using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DbSchema : ISchema
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location Location { get; set; }
        public List<TableSchema> Tables { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbSchema() 
        {
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
            database.Tables.ForEach(table => Tables.Add(table.GetSchema()));
            Location = (Location)Process.GetLocation();
        }
        #endregion
    }
}
