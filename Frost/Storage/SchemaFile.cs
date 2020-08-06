using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    public class SchemaFile : IStorageFile
    {
        #region Private Fields
        private string _schemaFileExtension;
        private string _schemaFileFolder;
        private string _databaseName;
        private string _fileText;
        private DbSchema _schema;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public SchemaFile(string schemaFileFolder, string fileExtension, string databaseName)
        {
            _schemaFileExtension = schemaFileFolder;
            _schemaFileExtension = fileExtension;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        public void Load()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void LoadFile()
        {
            var file = Path.Combine(_schemaFileFolder, _databaseName + "." + _schemaFileExtension);
            var lines = File.ReadAllLines(file).ToList();
            lines.ForEach(l => ParseLine(l));
        }

        private void ParseLine(string line)
        {
            
            if (line.StartsWith("table"))
            {
                var tableSchema = GetTableSchema(line);
            }
            
            if (line.StartsWith("column"))
            {
                var column = GetColumn(line);
            }
            throw new NotImplementedException();
        }
        private TableSchema GetTableSchema(string line)
        {
            // table tableId tableName
            var result = new TableSchema();

            var items = line.Split(" ");
            {
                Guid item;
                if (Guid.TryParse(items[0], out item))
                {
                    result.TableId = item;
                }
                result.TableName = items[1];
            }

            return result;
        }

        private Column GetColumn(string line)
        {
            // column columnid columnName columnDataType
            throw new NotImplementedException();
        }
        #endregion


    }
}
