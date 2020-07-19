using FrostCommon.ConsoleMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DbSchemaMapper
    {

        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static DbSchemaInfo Map(DbSchema schema)
        {
            var info = new DbSchemaInfo();
            MapDatabaseInformation(schema, info);
            MapTableInformation(schema, info);

            return info;
        }
        #endregion

        #region Private Methods
        private static void MapDatabaseInformation(DbSchema schema, DbSchemaInfo info)
        {
            info.DatabaseId = schema.DatabaseId;
            info.DatabaseName = schema.DatabaseName;
        }

        private static void MapTableInformation(DbSchema schema, DbSchemaInfo info)
        {
            foreach (var table in schema.Tables)
            {
                var tSchema = new TableSchemaInfo();
                tSchema.TableName = table.TableName;
                MapColumnInformation(table, tSchema);

                info.Tables.Add(tSchema);
            }
        }

        private static void MapColumnInformation(TableSchema table, TableSchemaInfo tSchema)
        {
            foreach (var c in table.Columns)
            {
                var cSchema = new ColumnSchemaInfo();
                cSchema.ColumnName = c.Name;
                cSchema.DataType = c.DataType.ToString();
                tSchema.Columns.Add(cSchema);
            }
        }
        #endregion


    }
}
