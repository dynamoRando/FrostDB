using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class TableSchema : ISchema
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string TableName { get; set; }
        public Guid? TableId { get; set; }
        public List<Column> Columns { get; set; }
        public bool IsCooperative { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public TableSchema()
        {
            Columns = new List<Column>();
        }

        public TableSchema(BaseTable table)
        {
            Map(table);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void Map(BaseTable table)
        {
            TableName = table.Name;
            TableId = table.Id;
            Columns = table.Columns;
            IsCooperative = table.IsCooperative();
        }
        #endregion
    }
}
